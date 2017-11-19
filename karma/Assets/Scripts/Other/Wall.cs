using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    [SerializeField]
    private bool bulletThrough = false;
    public bool BulletThrough
    {
        get { return bulletThrough; }
    }    
    
    [SerializeField]
    private List<BulletFactory.BulletType> bulletTypeList = new List<BulletFactory.BulletType>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsInList(BulletFactory.BulletType bulletType)
    {
        for(int i=0; i<bulletTypeList.Count+1; i++)
        {
            if(bulletType == bulletTypeList[i])
            {
                return true;
            }
        }
        return false;
    }
}
