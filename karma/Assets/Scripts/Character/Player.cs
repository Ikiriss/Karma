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
    private Vector2 speed = new Vector2(50, 50);
    public Vector2 Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // Stockage du mouvement
    private Vector2 movement;
    public Vector2 Movement
    {
        get { return movement; }
        set { movement = value; }
    }


    void Start () {
        maxHp = hp;
        myAnimator = GetComponent<Animator>();
        InitInventory();
    }


    void Update()
    { 
       

    }

	void FixedUpdate()
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
        GameObject.Find("Menu_death").GetComponent<Menu_death>().PopDeathMenu();
    }
}
