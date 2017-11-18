using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contrôleur du joueur
/// </summary>
public class Player : Entity {
	// Use this for initialization
	private Rigidbody2D rigidbody;
    private Entity entity;
    private WeaponScript weapon;
	private Animator animator;

    [SerializeField]
    private Vector2 speed = new Vector2(50, 50);
    public Vector2 Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // Stockage du mouvement
    private Vector2 movement;


    void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator>();
	}


    void Update()
    { 
       

    }

	void FixedUpdate()
	{

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

    }

    public void HandleMovement(float horizontal, float vertical)
    {
        // Calcul du mouvement
        movement = new Vector2(
          speed.x * horizontal,
          speed.y * vertical);


        // Déplacement limité au cadre de la caméra
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );

        GetComponent<Rigidbody2D>().velocity = movement;
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
