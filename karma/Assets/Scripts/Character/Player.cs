using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contrôleur du joueur
/// </summary>
public class Player : Entity {
    // Use this for initialization	

    private SpriteRenderer[] itemSprites;

    private Rigidbody2D rigidbody;

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
    private int numberOfItem = 5;
	

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

    [SerializeField]
    protected string jumpAnimationParameter = "jump";
    protected string JumpAnimationParameter
    {
        get { return jumpAnimationParameter; }
    }

    // Stockage du mouvement
    //private Vector2 movement;
    //public Vector2 Movement
    //{
    //    get { return movement; }
    //    set { movement = value; }
    //}

    private bool isHit = false;
    public bool IsHit
    {
        get { return isHit; }
        set { isHit = value; }
    }


    void Start () {
        maxHp = hp;
        myAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        InitInventory();
        itemSprites = new SpriteRenderer[numberOfItem];
        InitrenderInventory();
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
        Pnj pnj = collision.collider.GetComponent<Pnj>();
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        ShotScript shot = collision.collider.GetComponent<ShotScript>();
        Item item = collision.collider.GetComponent<Item>();
        if (shot && shot.IsEnemyShot)
        {
            hp -= shot.Damage;
            shot.ReturnToTheFactory();
            if(transform.eulerAngles.y == 0)
            {
                rigidbody.velocity = new Vector2(-5, 0);
            }
            else
            {
                rigidbody.velocity = new Vector2(5, 0);
            }

            isHit = true;

            //Debug.Log("je prend des dégats");
        }
        if (pnj && pnj.CanAttack && pnj.AttackPattern)
        {
            if(pnj.PnjName == Pnj.Name.CORBEAU && KarmaScript.karma != KarmaScript.KarmaState.NEGATIVE_KARMA)
            {
                PlayerController playerController = GetComponent<PlayerController>();
                if(playerController.PreviousGravityScale == -1)
                {
                    playerController.CorbeauMode = true;
                }
                
            }
            else
            {
                if (pnj.CanAttackSound)
                {
                    pnj.MakeAttackSound();
                }
                pnj.Attack();
                hp -= pnj.Damage;
                pnj.MakeAttackAnimation();
                //Recule in collision
                if (transform.eulerAngles.y == 0)
                {
                    rigidbody.velocity = new Vector2(-10, 0);
                    pnj.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
                    pnj.flipDirection();
                }
                else
                {
                    rigidbody.velocity = new Vector2(10, 0);
                    pnj.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                    pnj.flipDirection();
                }

                Debug.Log(hp);
                isHit = true;

            }

            
        }
        if (enemy && enemy.CanAttack && !enemy.IsHit)
        {
            if (enemy.CanAttackSound)
            {
                enemy.MakeAttackSound();
            }
            enemy.Attack();
            hp -= enemy.Damage;
            enemy.MakeAttackAnimation();

            //Recule in collision
            if (transform.eulerAngles.y == 0)
            {
                rigidbody.velocity = new Vector2(-10, 0);
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
                enemy.flipDirection();
            }
            else
            {
                rigidbody.velocity = new Vector2(10, 0);
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
                enemy.flipDirection();
            }


            Debug.Log(hp);
            isHit = true;
        }
        if(item)
        {
            item.IsPicked = true;
            switch(item.ItemName)
            {
                case Item.Name.PLANTE_MAGIQUE:
                    AddItemToInventory(item, 0);
                    break;

                case Item.Name.OEUF_CORBEAU:
                    AddItemToInventory(item, 1);
                    break;

                case Item.Name.HACHE:
                    AddItemToInventory(item, 2);
                    break;

                case Item.Name.ALLUMETTES:
                    AddItemToInventory(item, 3);
                    break;

                case Item.Name.BAGUETTE_MAGIQUE:
                    AddItemToInventory(item, 4);
                    break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       
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
        item1.ItemName = Item.Name.BAGUETTE_MAGIQUE;
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

    public virtual void MakeJumpAnimation()
    {
        if (myAnimator)
        {
            myAnimator.SetTrigger(jumpAnimationParameter);
        }
    }

    void InitrenderInventory()
    {
        Camera camera = GameObject.FindObjectOfType<Camera>();
        int childNumber = camera.transform.childCount;
        for(int i = 0; i < childNumber; i++ )
        {
            Transform t = camera.transform.GetChild(i);
            SpriteRenderer s = t.GetComponentInChildren<SpriteRenderer>();
            itemSprites[i] = s;
        }
    }

    void RenderInventory()
    {
        int i = 0;
        foreach(Item item in inventory)
        {
            i++;
            if(item.IsPicked)
            {
                itemSprites[i].enabled = true;
            }
        }
    }
}
