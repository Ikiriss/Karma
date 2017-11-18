using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contrôleur du joueur
/// </summary>
public class Player : Entity {
    // Use this for initialization	
    
    private Entity entity;
    private WeaponScript weapon;

    [SerializeField]
    private Item[] inventory;

    public Item[] Inventory
    {
        get { return inventory; }
    }
	

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



    void OnDestroy()
    {
        // Game Over
        Debug.Log("Vous êtes mort !");
        GameObject.Find("Menu_death").GetComponent<Menu_death>().PopDeathMenu();
    }
}
