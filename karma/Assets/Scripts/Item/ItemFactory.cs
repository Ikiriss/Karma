using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour {

    public Transform FLEUR;
    public Transform EPEE_EVENT2;
    public Transform OEUF_CORBEAU;
    public Transform HACHE;
    public Transform ALLUMETTES;
    public Transform ARC;
    public Transform PLANTE_MAGIQUE;

    private Transform[] FLEURPool;
    private Transform[] EPEE_EVENT2Pool;
    private Transform[] OEUF_CORBEAUPool;
    private Transform[] HACHEPool;
    private Transform[] ALLUMETTESPool;
    private Transform[] ARCPool;
    private Transform[] PLANTE_MAGIQUEPool;

    public int FLEURPoolLength;
    public int bouleEtoilePoolLength;
    public int capsuleEnergyPoolLength;
    // Use this for initialization
    void Start()
    {

        //initialisation de la pool

        FLEURPool = new Transform[FLEURPoolLength];
        EPEE_EVENT2Pool = new Transform[bouleEtoilePoolLength];
        /*OEUF_CORBEAUPool = new Transform[bouleEtoilePoolLength];0
        HACHEPool = new Transform[bouleEtoilePoolLength];
        ALLUMETTESPool = new Transform[bouleEtoilePoolLength];
        ARCPool = new Transform[bouleEtoilePoolLength];
        PLANTE_MAGIQUEPool = new Transform[bouleEtoilePoolLength];
        boule7EtoilePool = new Transform[bouleEtoilePoolLength];*/



        initList(FLEURPool,FLEURPoolLength,Item.Name.FLEUR);
        initList(EPEE_EVENT2Pool, bouleEtoilePoolLength, Item.Name.EPEE_EVENT2);
        /*initList(OEUF_CORBEAUPool, bouleEtoilePoolLength, Item.Name.OEUF_CORBEAU);
        initList(HACHEPool, bouleEtoilePoolLength, Item.Name.HACHE);
        initList(ALLUMETTESPool, bouleEtoilePoolLength, Item.Name.ALLUMETTES);
        initList(ARCPool, bouleEtoilePoolLength, Item.Name.ARC);
        initList(PLANTE_MAGIQUEPool, bouleEtoilePoolLength, Item.Name.PLANTE_MAGIQUE);
        initList(boule7EtoilePool, bouleEtoilePoolLength, Item.Name.boule7Etoile);*/

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initList(Transform[] pool, int poolLength, Item.Name itemType)
    {
        for (int i = 0; i < poolLength; i++)
        {
            pool[i] = SpawnItem(itemType);
        }
    }

    //spawn un bullet dans la pool
    public Transform SpawnItem(Item.Name itemType)
    {
        Transform bullet = null;
        switch (itemType)
        {
            case Item.Name.FLEUR:
                bullet = Instantiate(FLEUR) as Transform;
                break;
            case Item.Name.EPEE_EVENT2:
                bullet = Instantiate(EPEE_EVENT2) as Transform;
                break;
            case Item.Name.OEUF_CORBEAU:
                bullet = Instantiate(OEUF_CORBEAU) as Transform;
                break;
            case Item.Name.HACHE:
                bullet = Instantiate(HACHE) as Transform;
                break;
            case Item.Name.ALLUMETTES:
                bullet = Instantiate(ALLUMETTES) as Transform;
                break;
            case Item.Name.ARC:
                bullet = Instantiate(ARC) as Transform;
                break;
            case Item.Name.PLANTE_MAGIQUE:
                bullet = Instantiate(PLANTE_MAGIQUE) as Transform;
                break;
        }
        bullet.position = new Vector3(-100, -100, -10);

        return bullet;
    }

    //permet d'obtenir un bullet de la pool
    public Transform GetItem(Item.Name itemType)
    {
        Transform bullet = null;
        switch (itemType)
        {

            
            case Item.Name.FLEUR:
                bullet = GetABulletFromAPool(FLEURPool,FLEURPoolLength);
                break;
            case Item.Name.EPEE_EVENT2:
                bullet = GetABulletFromAPool(EPEE_EVENT2Pool,bouleEtoilePoolLength);
                break;
            case Item.Name.OEUF_CORBEAU:
                bullet = GetABulletFromAPool(OEUF_CORBEAUPool, bouleEtoilePoolLength);
                break;
            case Item.Name.HACHE:
                bullet = GetABulletFromAPool(HACHEPool, bouleEtoilePoolLength);
                break;
            case Item.Name.ALLUMETTES:
                bullet = GetABulletFromAPool(ALLUMETTESPool, bouleEtoilePoolLength);
                break;
            case Item.Name.ARC:
                bullet = GetABulletFromAPool(ARCPool, bouleEtoilePoolLength);
                break;
            case Item.Name.PLANTE_MAGIQUE:
                bullet = GetABulletFromAPool(PLANTE_MAGIQUEPool, bouleEtoilePoolLength);
                break;
        }
        if (bullet == null)
        {
            bullet = SpawnItem(itemType);
        }
        return bullet;
    }

    //permet de rendre un bullet dans la pool
    public void GiveBackItem(Item.Name mobType, Transform bullet)
    {
        bullet.position = new Vector3(-100, -100, -10);
        
        switch (mobType)
        {

            case Item.Name.FLEUR:
                PutBulletBackInAPool(bullet,FLEURPool, FLEURPoolLength);
                break;
            case Item.Name.EPEE_EVENT2:
                PutBulletBackInAPool(bullet,EPEE_EVENT2Pool, bouleEtoilePoolLength);
                break;
            case Item.Name.OEUF_CORBEAU:
                PutBulletBackInAPool(bullet,OEUF_CORBEAUPool, bouleEtoilePoolLength);
                break;
            case Item.Name.HACHE:
                PutBulletBackInAPool(bullet, HACHEPool, bouleEtoilePoolLength);
                break;
            case Item.Name.ALLUMETTES:
                PutBulletBackInAPool(bullet, ALLUMETTESPool, bouleEtoilePoolLength);
                break;
            case Item.Name.ARC:
                PutBulletBackInAPool(bullet, ARCPool, bouleEtoilePoolLength);
                break;
            case Item.Name.PLANTE_MAGIQUE:
                PutBulletBackInAPool(bullet, PLANTE_MAGIQUEPool, bouleEtoilePoolLength);
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
