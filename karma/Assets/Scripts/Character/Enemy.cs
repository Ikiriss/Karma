using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    private Rigidbody2D rigidbody;

    private Player player;

    private bool isHit = false;
    public bool IsHit
    {
        get { return isHit; }
        set { isHit = value; }
    }


    private float frameCount = 0;
    private float maxCount = 20;

    [SerializeField]
    protected EnemyFactory.MobType enemyName;
    public EnemyFactory.MobType EnemyName
    {
        get { return enemyName; }
    }
        

    protected bool hasSpawn;
    public bool HasSpawn
    {
        get { return hasSpawn; }
        set { hasSpawn = value; }
    }

    protected MoveScript moveScript;
	protected WeaponScript[] weapons;

	protected bool alive = true;
    public bool Alive
    {
        get { return alive; }
        set { alive = value; }
    }


    void Awake()
	{
		// Récupération de toutes les armes de l'ennemi
		weapons = GetComponentsInChildren<WeaponScript>();

		// Récupération du script de mouvement lié
		moveScript = GetComponent<MoveScript>();
	}

	// 1 - Disable everything
	void Start()
	{
        player = GameObject.FindObjectOfType<Player>();
        hasSpawn = false;

		// On désactive tout
		// -- collider
		GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        myAnimator = GetComponent<Animator>();
		// -- Mouvement
		moveScript.enabled = false;
		// -- Tir
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}
        attackCooldown = 0f;
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
	void Update()
	{
		// 2 - On vérifie si l'ennemi est apparu à l'écran
		if (hasSpawn == false)
		{
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
			{
				Spawn();
                
			}
		}
		else
		{
            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }
            if (attackSoundCooldown > 0)
            {
                attackSoundCooldown -= Time.deltaTime;
            }
            if (walkSoundCooldown > 0)
            {
                walkSoundCooldown -= Time.deltaTime;
            }
            // On fait tirer toutes les armes automatiquement si il est vivant
            HandleShootWithWeapons();

            if (isHit)
            {
                if(frameCount == maxCount)
                {
                    frameCount = 0;
                    moveScript.enabled = true;
                    isHit = false;
                }
                else
                {
                    frameCount++;
                    moveScript.enabled = false;
                }
            }
                // Si L'ennemi n'a pas été détruit, il faut faire le ménage
                //if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
                //{
                //	Destroy(gameObject);
                //}
            }
	}

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        ShotScript shot = collision.collider.GetComponent<ShotScript>();
        if (shot && !(shot.IsEnemyShot))
        {
            hp -= shot.Damage;
        }

        SwordWeapon sword = collision.collider.GetComponent<SwordWeapon>();

        if (sword)
        {
            hp -= player.Damage;
            isHit = true;
            rigidbody.velocity = new Vector2(10, 0);
            Debug.Log("je me fais taper");
        }

        if (hp <= 0)
        {
            GiveMobBack(transform);
        }
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        
    }
    

    // 3 - Activation
    private void Spawn()
	{
		hasSpawn = true;

		// On active tout
		// -- Collider
		GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
		// -- Mouvement
		moveScript.enabled = true;
        // -- Tir
        //Si c'est une tête chercheuse, on l'anime, sinon on met les armes en place
        if (moveScript.CharacterLockInit)
        {
            if(GetComponent<Animator>())
                GetComponent<Animator>().SetTrigger("attack");
            moveScript.AnimateHeadHunter();

        }
        else
        {
            foreach (WeaponScript weapon in weapons)
            {
                weapon.enabled = true;
            }
        }
	}

	public void setDead(){
		alive = false;
	}

    private void HandleShootWithWeapons()
    {
        if (alive)
        {
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    if (myAnimator)
                        myAnimator.SetTrigger(weapon.WeaponAnimationParameter);
                    weapon.Attack(true);
                    //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                }
            }
        }
    }
    

    public void UnSpawn()
    {
        hasSpawn = false;
        alive = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        // -- Mouvement
        moveScript.enabled = false;
        // -- Tir
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
        hp = maxHp;
        
    }

    public void flipDirection()
    {
        if (transform.eulerAngles.y == 180)
        {
            if (transform.position.x < player.transform.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
        else
        {
            if (transform.position.x > player.transform.position.x)
            {
                transform.Rotate(0, -180, 0);
            }
        }
    }


}
