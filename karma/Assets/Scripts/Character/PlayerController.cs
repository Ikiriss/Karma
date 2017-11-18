using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float translateActivationMarge = 0.1f;
    private bool grounded = false;
    private Rigidbody2D rigidbody;
    private Player player;
    private Vector2 movement = new Vector2(0, 0);
    private bool moveRight = false;
    private bool moveLeft = false;
    private bool jump = false;
    private bool attack1 = false;
    private bool moveHorizontalBlocked = false;
    public bool Attack1
    {
        get {
            if (player.CanAttack)
            {
                player.Attack();
                return attack1;
            }
            else
            {
                return false;
            }
             }
    }
    private bool attack2 = false;
    private bool attack3 = false;

    private float previousVelocityY =0;
    // Use this for initialization
    void Start () {

        rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (/*previousVelocityY == 0 && */rigidbody.velocity.y == 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        
        if (Input.GetButtonUp("Horizontal"))
        {
            //moveHorizontalBlocked = true;
        }
        if(Input.GetButton("Attack1") || Input.GetButton("ButtonB")){ attack1 = true; }
        else{ attack1 = false;}
        if (Input.GetButton("Attack2") || Input.GetButton("ButtonA")){ attack2 = true;}
        else { attack2 = false; }
        if(Input.GetButton("Attack3") || Input.GetButton("ButtonX")) { attack3 = true; }
        else { attack3 = false; }
        if((Input.GetButton("Jump") || Input.GetButton("ButtonY")) && grounded) { jump = true; }
        else { jump = false; }
        
    }

    private void FixedUpdate()
    {
        float horizontalTranslation = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalStick") + Input.GetAxis("HorizontalCroix");
        //if(horizontalTranslation == 0)
        //{
        //    moveHorizontalBlocked = false;
        //}

        if (Mathf.Abs(horizontalTranslation) > translateActivationMarge && horizontalTranslation > 0 && !moveHorizontalBlocked)
        {
            moveRight = true;
            moveLeft = false;
        }
        else if (Mathf.Abs(horizontalTranslation) > translateActivationMarge && horizontalTranslation < 0 && !moveHorizontalBlocked)
        {
            moveLeft = true;
            moveRight = false;
        }
        else
        {
            moveLeft = false;
            moveRight = false;
        }
        HandleMovement();
    }

    
    

    private void HandleMovement()
    {
        
        if (moveRight)
        {
            movement.x = player.Speed.x;
        }else if (moveLeft)
        {
            movement.x = -player.Speed.x;
        }
        else
        {
            movement.x = 0;
        }        
        if (jump)
        {
            movement.y = player.Speed.y;
        }
        else
        {
            movement.y = 0;
        }
        
        
        


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

        previousVelocityY = rigidbody.velocity.y;
        rigidbody.velocity += movement;
        if (rigidbody.velocity.x > player.MaxSpeed.x)
        {
            rigidbody.velocity = new Vector2(player.MaxSpeed.x,rigidbody.velocity.y);
        }else if(rigidbody.velocity.x < -player.MaxSpeed.x)
        {
            rigidbody.velocity = new Vector2(-player.MaxSpeed.x, rigidbody.velocity.y);
        }else if(grounded && movement.x == 0)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        }
        if(rigidbody.velocity.y > player.MaxSpeed.y)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, player.MaxSpeed.y);
        }
        

    }
}
