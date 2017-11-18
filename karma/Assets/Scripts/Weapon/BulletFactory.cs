using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour {

    //public static BulletFactory Instance;
    public Transform archerBulletPrefab;
    public Transform archerAutoBulletPrefab;
    public Transform sorcierBulletPrefab;
    
    
    private Transform[] archerBulletPool;
    private Transform[] archerAutoBulletPool;
    private Transform[] sorcierBulletPool;
    

    public int archerBulletPoolLength = 10;
    public int archerAutoBulletPoolLength = 10;
    public int sorcierBulletPoolLength = 10;
    

    public enum BulletType
    {
        NONE,
        ARCHERBULLET,
        ARCHERAUTOBULLET,
        SORCIERBULLET
    }
    
	// Use this for initialization
	void Start () {

        //initialisation de la pool
        archerBulletPool = new Transform[archerBulletPoolLength];
        archerAutoBulletPool = new Transform[archerAutoBulletPoolLength];
        sorcierBulletPool = new Transform[sorcierBulletPoolLength];

        initBulletList(archerBulletPool, archerBulletPoolLength, BulletType.ARCHERBULLET);
        initBulletList(archerAutoBulletPool, archerAutoBulletPoolLength, BulletType.ARCHERAUTOBULLET);
        initBulletList(sorcierBulletPool, sorcierBulletPoolLength, BulletType.SORCIERBULLET);
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initBulletList(Transform[] pool, int poolLength, BulletType bulletType)
    {
        for(int i=0; i < poolLength; i++)
        {
            pool[i] = SpawnBullet(bulletType);
        }
    }

    //spawn un bullet dans la pool
    public Transform SpawnBullet(BulletType bulletType)
    {
        Transform bullet = null;
        switch (bulletType)
        {
            case BulletType.ARCHERBULLET:
                bullet = Instantiate(archerBulletPrefab) as Transform;
                break;
            case BulletType.ARCHERAUTOBULLET:
                bullet = Instantiate(archerAutoBulletPrefab) as Transform;
                break;
            case BulletType.SORCIERBULLET:
                bullet = Instantiate(sorcierBulletPrefab) as Transform;
                break;
                
        }
        bullet.position = new Vector3(-100, -100, -10);
        bullet.gameObject.GetComponent<Renderer>().enabled = false;
        bullet.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        bullet.gameObject.GetComponent<MoveScript>().enabled = false;
        bullet.gameObject.GetComponent<ShotScript>().enabled = false;
        //bullet.gameObject.GetComponent<Entity>().enabled = false;

        return bullet;
    } 

    //permet d'obtenir un bullet de la pool
    public Transform GetBullet(BulletType bulletType)
    {
        Transform bullet = null;
        switch (bulletType)
        {
            case BulletType.ARCHERBULLET:
                bullet = GetABulletFromAPool(archerBulletPool, archerBulletPoolLength);
                break;
            case BulletType.ARCHERAUTOBULLET:
                bullet = GetABulletFromAPool(archerAutoBulletPool, archerAutoBulletPoolLength);
                break;
            case BulletType.SORCIERBULLET:
                bullet = GetABulletFromAPool(sorcierBulletPool, sorcierBulletPoolLength);
                break;
        }
        if (bullet == null)
        {
            bullet = SpawnBullet(bulletType);
        }
        return bullet;
    }

    //permet de rendre un bullet dans la pool
    public void GiveBackBullet(BulletType bulletType, Transform bullet)
    {
        bullet.position = new Vector3(-100, -100, -10);
        bullet.gameObject.GetComponent<Renderer>().enabled = false;
        bullet.gameObject.GetComponent<PolygonCollider2D>().enabled = false;        
        bullet.gameObject.GetComponent<MoveScript>().enabled = false;
        bullet.gameObject.GetComponent<ShotScript>().enabled = false;
        bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        //Entity hpScript = bullet.gameObject.GetComponent<Entity>();
        //hpScript.Hp = hpScript.MaxHp;
        if (bullet.gameObject.GetComponent<Animator>())
            bullet.gameObject.GetComponent<Animator>().SetBool("pool",true);
        switch (bulletType)
        {
            case BulletType.ARCHERBULLET:
                PutBulletBackInAPool(bullet, archerBulletPool, archerBulletPoolLength);
                break;
            case BulletType.ARCHERAUTOBULLET:
                PutBulletBackInAPool(bullet, archerAutoBulletPool, archerAutoBulletPoolLength);
                break;
            case BulletType.SORCIERBULLET:
                PutBulletBackInAPool(bullet, sorcierBulletPool, sorcierBulletPoolLength);
                break;
        }

    }

    //sort un bullet d'une pool
    private Transform GetABulletFromAPool(Transform[] bulletPool, int poolLength)
    {
        Transform bullet = null;
        for (int i = 0; i < poolLength; i++)
        {
            if (bulletPool[i] != null)
            {
                bullet = bulletPool[i];
                bulletPool[i] = null;
                break;
            }
        }
        return bullet;
    }

    //met un bullet dans une pool
    private void PutBulletBackInAPool(Transform bullet, Transform[] bulletPool, int poolLength )
    {
        for(int i=0; i < poolLength; i++)
        {

            if (bulletPool[i] == null)
            {
                //Debug.Log(bulletPool[i]);
                bulletPool[i] = bullet;
                return;
            }
        }
        //Si on est ici, il n'y a plus de place
        Destroy(bullet.gameObject);
    }
    
}
