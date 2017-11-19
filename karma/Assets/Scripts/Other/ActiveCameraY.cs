using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCameraY : MonoBehaviour {
    [SerializeField]
    private GameObject camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            camera.GetComponent<CameraIsFollowingEntity>().CameraFollowOnY = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            camera.GetComponent<CameraIsFollowingEntity>().CameraFollowOnY = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
