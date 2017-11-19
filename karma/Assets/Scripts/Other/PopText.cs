using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopText : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>())
        {
            Debug.Log("Je suis passé ici !");
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
}
