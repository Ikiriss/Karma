using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidbody;
    private Player player;
    private bool moveRight = false;
    private bool moveLeft = false;
    private bool jump = false;
    private bool attack1 = false;
    private bool attack2 = false;
    private bool attack3 = false;
    // Use this for initialization
    void Start () {

        rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetAxis("Horizontal")>0 || Input.GetAxis("HorizontalStick")>0 || Input.GetAxis("HorizontalCroix") > 0)
        {
            moveRight = true;
            moveLeft = false;
        }else if(Input.GetAxis("Horizontal") < 0 || Input.GetAxis("HorizontalStick") < 0 || Input.GetAxis("HorizontalCroix") < 0)
        {
            moveLeft = true;
            moveRight = false;
        }
        else
        {
            moveLeft = false;
            moveRight = false;
        }
        if(Input.GetButton("Attack1") || Input.GetButton("ButtonB")){ attack1 = true; }
        else{ attack1 = false;}
        if (Input.GetButton("Attack2") || Input.GetButton("ButtonA")){ attack2 = true;}
        else { attack2 = false; }
        if(Input.GetButton("Attack3") || Input.GetButton("ButtonX")) { attack3 = true; }
        else { attack3 = false; }
        if(Input.GetButtonDown("Jump") || Input.GetButtonDown("ButtonY")) { jump = true; }
        else { jump = false; }
    }

    private void FixedUpdate()
    {
        
    }

    public void HandleMovement(float horizontal, float vertical)
    {
        
        // Calcul du mouvement
        player.Movement = new Vector2(
          player.Speed.x * horizontal,
          player.Speed.y * vertical);


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

        rigidbody.velocity = player.Movement;
    }
}
