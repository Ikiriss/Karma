using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIsFollowingEntity : MonoBehaviour {
    [SerializeField]
    private GameObject focus;

    [SerializeField]
    private bool cameraFollowOnY = false;
    public bool CameraFollowOnY
    {
        get
        {
            return cameraFollowOnY;
        }
        set
        {
            CameraFollowOnY = value;
        }
    }
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {        
        Vector2 velocity = focus.GetComponent<Rigidbody2D>().velocity;
        if (!cameraFollowOnY)
        {
            velocity = new Vector2(velocity.x, 0);
        }
        else
        {
            
        }
        
        transform.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
