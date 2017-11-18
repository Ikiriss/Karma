using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordureScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Je suis passé ici !");
    }
    
    
}
