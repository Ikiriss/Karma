using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIsFollowingEntity : MonoBehaviour {
    [SerializeField]
    private GameObject focus;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetComponent<Rigidbody2D>().velocity = focus.GetComponent<Rigidbody2D>().velocity;
	}
}
