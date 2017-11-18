using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contrôleur du joueur
/// </summary>
public class Player : Entity {
    // Use this for initialization	

    static public int karma = 0;

    private Entity entity;
    private WeaponScript weapon;

    [SerializeField]
    private Item[] inventory;
    public Item[] Inventory
    {
        get { return inventory; }
    }
    [SerializeField]
    private int numberOfItem = 6;
	

    [SerializeField]
    private Vector2 speed = new Vector2(5, 20);
    public Vector2 Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    [SerializeField]
    private Vector2 maxSpeed = new Vector2(10, 20);
    public Vector2 MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }

    [SerializeField]
    protected AudioClip jumpSound = null;
    //protected Enemy enemyScript;
    [SerializeField]
    protected float jumpSoundRate = 1;
    protected float jumpSoundCooldown;
    [SerializeField]
    protected float jumpSoundVolume = 1.0f;

    // Stockage du mouvement
    //private Vector2 movement;
    //public Vector2 Movement
    //{
    //    get { return movement; }
    //    set { movement = value; }
    //}


    void Start () {
        maxHp = hp;
        myAnimator = GetComponent<Animator>();
        InitInventory();
    }


    void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        if(attackSoundCooldown > 0)
        {
            attackSoundCooldown -= Time.deltaTime;
        }
        if(walkSoundCooldown > 0)
        {
            walkSoundCooldown -= Time.deltaTime;
        }
        if(jumpSoundCooldown > 0)
        {
            jumpSoundCooldown -= Time.deltaTime;
        }

    }

	void FixedUpdate()
	{

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ShotScript shot = collision.collider.GetComponent<ShotScript>();
        if (shot && shot.IsEnemyShot)
        {
            hp -= shot.Damage;
            shot.ReturnToTheFactory();
            //Debug.Log("je prend des dégatzaes");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        Pnj pnj = collision.collider.GetComponent<Pnj>();
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        ShotScript shot = collision.collider.GetComponent<ShotScript>();
        if (pnj)
        {
            //do sth
        }
        else if (enemy && enemy.CanAttack)
        {
            if (enemy.CanAttackSound)
            {
                enemy.MakeAttackSound();
            }
            enemy.Attack();
            hp -= enemy.Damage;
            Debug.Log(hp);
        }
        else if (shot && shot.IsEnemyShot)
        {
            hp -= shot.Damage;
            shot.ReturnToTheFactory();
            Debug.Log("je prend des dégats");
        }
    }
    

    void AddItemToInventory(Item item, int position)
    {
        inventory[position] = item;
    }
    



    public void HandleShoot(bool shoot)
    {
        
    }


    public void UpdateBar()
    {
        float hpPercent = (float)GetComponent<Entity>().Hp / GetComponent<Entity>().MaxHp;
        GetComponent<BarScript>().MoveHealthBar(hpPercent);
    }

    private void InitInventory()
    {
        inventory = new Item[numberOfItem];
        Item item1 = new Item();
        item1.ItemName = Item.Name.FLEUR;
        inventory[0] = item1;
        Item item2 = new Item();
        item1.ItemName = Item.Name.OEUF_CORBEAU;
        inventory[1] = item2;
        Item item3 = new Item();
        item1.ItemName = Item.Name.ALLUMETTES;
        inventory[2] = item3;
        Item item4 = new Item();
        item1.ItemName = Item.Name.ARC;
        inventory[3] = item4;
        Item item5 = new Item();
        item1.ItemName = Item.Name.PLANTE_MAGIQUE;
        inventory[4] = item5;
        Item item6 = new Item();
        item1.ItemName = Item.Name.HACHE;
        inventory[5] = item6;
    }

    public void SetItemPicked(int itemPosition)
    {
        inventory[itemPosition].IsPicked = true;
    }

    public void SetItemUnPicked(int itemPosition)
    {
        inventory[itemPosition].IsPicked = false;
    }

    void OnDestroy()
    {
        // Game Over
        Debug.Log("Vous êtes mort !");
        //GameObject.Find("Menu_death").GetComponent<Menu_death>().PopDeathMenu();
    }

    public virtual bool CanJumpSound
    {
        get
        {
            return jumpSoundCooldown <= 0;
        }
    }

    public virtual void MakeJumpSound()
    {
        jumpSoundCooldown = jumpSoundRate;
        AudioSource.PlayClipAtPoint(jumpSound, transform.position, jumpSoundVolume);
    }
}
