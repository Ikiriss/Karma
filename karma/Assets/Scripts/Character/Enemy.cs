using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {


    

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
		hasSpawn = false;

		// On désactive tout
		// -- collider
		GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
		// -- Mouvement
		moveScript.enabled = false;
		// -- Tir
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}
        attackCooldown = 0f;
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


			// Si L'ennemi n'a pas été détruit, il faut faire le ménage
			if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
	}

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        ShotScript shot = collision.collider.GetComponent<ShotScript>();
        if (shot && !(shot.IsEnemyShot))
        {
            hp -= shot.Damage;
        }
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        
        Player player = collision.collider.GetComponent<Player>();
        
        if (player && collision.collider.GetComponent<PlayerController>().Attack1)
        {
            hp -= player.Damage;
            Debug.Log("je me fais taper");
        }

        if (hp <= 0)
        {            
            GiveMobBack(transform);
        }
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
                    if (GetComponent<Animator>())
                        GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
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

    
}
