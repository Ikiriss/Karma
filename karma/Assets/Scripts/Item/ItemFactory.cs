using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour {

    public Transform FLEUR;
    public Transform EPEE_EVENT2;
    public Transform OEUF_CORBEAU;
    public Transform HACHE;
    public Transform ALLUMETTES;
    public Transform BAGUETTE_MAGIQUE;
    public Transform PLANTE_MAGIQUE;

    private Transform[] FLEURPool;
    private Transform[] EPEE_EVENT2Pool;
    private Transform[] OEUF_CORBEAUPool;
    private Transform[] HACHEPool;
    private Transform[] ALLUMETTESPool;
    private Transform[] BAGUETTE_MAGIQUEPool;
    private Transform[] PLANTE_MAGIQUEPool;

    public int FleurPoolLength = 1;
    public int OeufCorbeauPoolLength = 1;
    public int HachePoolLength = 1;
    public int AllumettesPoolLength = 1;
    public int BaguetteMagiquePoolLength = 1;
    public int PlanteMagiquePoolLength = 1;

    // Use this for initialization
    void Start()
    {

        //initialisation de la pool

        FLEURPool = new Transform[FleurPoolLength];      
        OEUF_CORBEAUPool = new Transform[OeufCorbeauPoolLength]; 
        HACHEPool = new Transform[HachePoolLength];
        ALLUMETTESPool = new Transform[AllumettesPoolLength];
        BAGUETTE_MAGIQUEPool = new Transform[BaguetteMagiquePoolLength];
        PLANTE_MAGIQUEPool = new Transform[PlanteMagiquePoolLength];




        initList(FLEURPool, FleurPoolLength, Item.Name.FLEUR);
        initList(OEUF_CORBEAUPool, OeufCorbeauPoolLength, Item.Name.OEUF_CORBEAU);
        initList(HACHEPool, HachePoolLength, Item.Name.HACHE);
        initList(ALLUMETTESPool, AllumettesPoolLength, Item.Name.ALLUMETTES);
        initList(BAGUETTE_MAGIQUEPool, BaguetteMagiquePoolLength, Item.Name.BAGUETTE_MAGIQUE);
        initList(PLANTE_MAGIQUEPool, PlanteMagiquePoolLength, Item.Name.PLANTE_MAGIQUE);

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

    //spawn un item dans la pool
    public Transform SpawnItem(Item.Name itemType)
    {
        Transform item = null;
        switch (itemType)
        {
            case Item.Name.FLEUR:
                item = Instantiate(FLEUR) as Transform;
                break;
            case Item.Name.EPEE_EVENT2:
                item = Instantiate(EPEE_EVENT2) as Transform;
                break;
            case Item.Name.OEUF_CORBEAU:
                item = Instantiate(OEUF_CORBEAU) as Transform;
                break;
            case Item.Name.HACHE:
                item = Instantiate(HACHE) as Transform;
                break;
            case Item.Name.ALLUMETTES:
                item = Instantiate(ALLUMETTES) as Transform;
                break;
            case Item.Name.BAGUETTE_MAGIQUE:
                item = Instantiate(BAGUETTE_MAGIQUE) as Transform;
                break;
            case Item.Name.PLANTE_MAGIQUE:
                item = Instantiate(PLANTE_MAGIQUE) as Transform;
                break;
        }
        item.position = new Vector3(-100, -100, -10);

        return item;
    }

    //permet d'obtenir un item de la pool
    public Transform GetItem(Item.Name itemType)
    {
        Transform item = null;
        switch (itemType)
        {

            
            case Item.Name.FLEUR:
                item = GetAitemFromAPool(FLEURPool, FleurPoolLength);
                break;
            case Item.Name.OEUF_CORBEAU:
                item = GetAitemFromAPool(OEUF_CORBEAUPool, OeufCorbeauPoolLength);
                break;
            case Item.Name.HACHE:
                item = GetAitemFromAPool(HACHEPool, HachePoolLength);
                break;
            case Item.Name.ALLUMETTES:
                item = GetAitemFromAPool(ALLUMETTESPool, AllumettesPoolLength);
                break;
            case Item.Name.BAGUETTE_MAGIQUE:
                item = GetAitemFromAPool(BAGUETTE_MAGIQUEPool, BaguetteMagiquePoolLength);
                break;
            case Item.Name.PLANTE_MAGIQUE:
                item = GetAitemFromAPool(PLANTE_MAGIQUEPool, PlanteMagiquePoolLength);
                break;
        }
        if (item == null)
        {
            item = SpawnItem(itemType);
        }
        return item;
    }

    //permet de rendre un item dans la pool
    public void GiveBackItem(Item.Name mobType, Transform item)
    {
        item.position = new Vector3(-100, -100, -10);
        if (item.GetComponent<Rigidbody2D>())
        {
            item.GetComponent<Rigidbody2D>().simulated = false;
        }
        
        switch (mobType)
        {

            case Item.Name.FLEUR:
                PutitemBackInAPool(item,FLEURPool, FleurPoolLength);
                break;
            case Item.Name.OEUF_CORBEAU:
                PutitemBackInAPool(item,OEUF_CORBEAUPool, OeufCorbeauPoolLength);
                break;
            case Item.Name.HACHE:
                PutitemBackInAPool(item, HACHEPool, HachePoolLength);
                break;
            case Item.Name.ALLUMETTES:
                PutitemBackInAPool(item, ALLUMETTESPool, AllumettesPoolLength);
                break;
            case Item.Name.BAGUETTE_MAGIQUE:
                PutitemBackInAPool(item, BAGUETTE_MAGIQUEPool, BaguetteMagiquePoolLength);
                break;
            case Item.Name.PLANTE_MAGIQUE:
                PutitemBackInAPool(item, PLANTE_MAGIQUEPool, PlanteMagiquePoolLength);
                break;
        }

    }

    //sort un item d'une pool
    private Transform GetAitemFromAPool(Transform[] itemPool, int poolLength)
    {
        Transform item = null;
        for (int i = 0; i < poolLength; i++)
        {
            if (itemPool[i] != null)
            {
                item = itemPool[i];
                itemPool[i] = null;
                break;
            }
        }
        return item;
    }

    //met un item dans une pool
    private void PutitemBackInAPool(Transform item, Transform[] itemPool, int poolLength)
    {
        for (int i = 0; i < poolLength; i++)
        {

            if (itemPool[i] == null)
            {
                //Debug.Log(itemPool[i]);
                itemPool[i] = item;
                return;
            }
        }
        //Si on est ici, il n'y a plus de place
        Destroy(item.gameObject);
    }


}
