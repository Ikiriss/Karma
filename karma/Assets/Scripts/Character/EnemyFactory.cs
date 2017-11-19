using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {

    
    public Transform squelettePrefab;
    public Transform blobPrefab;
    public Transform chauve_sourisPrefab;


    private Transform[] squelettePool;
    private Transform[] blobPool;
    private Transform[] chauve_sourisPool;
    

    public int squelettePoolLength = 5;
    public int blobPoolLength = 3;
    public int chauve_sourisPoolLength = 1;


    public enum MobType
    {
        NONE,
        SQUELETTE,
        BLOB,
        CHAUVE_SOURIS
    }

    // Use this for initialization
    void Start()
    {

        //initialisation de la pool
        
        squelettePool = new Transform[squelettePoolLength];
        blobPool = new Transform[blobPoolLength];
        chauve_sourisPool = new Transform[chauve_sourisPoolLength];
        

        
        initList(squelettePool, squelettePoolLength, MobType.SQUELETTE);
        initList(blobPool, blobPoolLength, MobType.BLOB);
        initList(chauve_sourisPool, chauve_sourisPoolLength, MobType.CHAUVE_SOURIS);
        


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initList(Transform[] pool, int poolLength, MobType mobType)
    {
        for (int i = 0; i < poolLength; i++)
        {
            pool[i] = SpawnMob(mobType);
        }
    }

    //spawn un bullet dans la pool
    public Transform SpawnMob(MobType mobType)
    {
        Transform bullet = null;
        switch (mobType)
        {
            case MobType.SQUELETTE:
                bullet = Instantiate(squelettePrefab) as Transform;
                break;
            case MobType.BLOB:
                bullet = Instantiate(blobPrefab) as Transform;
                break;
            case MobType.CHAUVE_SOURIS:
                bullet = Instantiate(chauve_sourisPrefab) as Transform;
                break;

        }
        bullet.position = new Vector3(-100, -100, -10);
        /*bullet.gameObject.GetComponent<Renderer>().enabled = false;
        bullet.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        bullet.gameObject.GetComponent<MoveScript>().enabled = false;
        bullet.gameObject.GetComponent<ShotScript>().enabled = false;
        bullet.gameObject.GetComponent<HealthScript>().enabled = false;*/

        return bullet;
    }

    //permet d'obtenir un bullet de la pool
    public Transform GetMob(MobType mobType)
    {
        Transform bullet = null;
        switch (mobType)
        {

            case MobType.SQUELETTE:
                bullet = GetABulletFromAPool(squelettePool, squelettePoolLength);
                break;
            case MobType.BLOB:
                bullet = GetABulletFromAPool(blobPool, blobPoolLength);
                break;
            case MobType.CHAUVE_SOURIS:
                bullet = GetABulletFromAPool(chauve_sourisPool, chauve_sourisPoolLength);
                break;

        }
        if (bullet == null)
        {
            bullet = SpawnMob(mobType);
        }
        return bullet;
    }

    //permet de rendre un bullet dans la pool
    public void GiveBackMob(MobType mobType, Transform bullet)
    {
        bullet.position = new Vector3(-100, -100, -10);
        /*bullet.gameObject.GetComponent<Renderer>().enabled = false;
        bullet.gameObject.GetComponent<PolygonCollider2D>().enabled = false;*/
        bullet.gameObject.GetComponent<MoveScript>().enabled = false;
        //bullet.gameObject.GetComponent<ShotScript>().enabled = false;
        Enemy enemy = bullet.gameObject.GetComponent<Enemy>();
        enemy.UnSpawn();
        bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        //Entity hpScript = bullet.gameObject.GetComponent<Entity>();
        Animator anim = bullet.gameObject.GetComponent<Animator>();
        anim.SetBool("pool", true);
        switch (mobType)
        {           
            case MobType.SQUELETTE:
                PutBulletBackInAPool(bullet, squelettePool, squelettePoolLength);
                break;
            case MobType.BLOB:
                PutBulletBackInAPool(bullet, blobPool, blobPoolLength);
                break;
            case MobType.CHAUVE_SOURIS:
                PutBulletBackInAPool(bullet, chauve_sourisPool, chauve_sourisPoolLength);
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
    private void PutBulletBackInAPool(Transform bullet, Transform[] bulletPool, int poolLength)
    {
        for (int i = 0; i < poolLength; i++)
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
