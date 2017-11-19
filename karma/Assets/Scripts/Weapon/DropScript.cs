using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour {
    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Prefab du projectile
    /// </summary>
    //public Transform shotPrefab;    
    [SerializeField]
    protected Item.Name itemName;
    public Item.Name ItemName
    {
        get
        {
            return itemName;
        }
    }


    /// <summary>
    /// Temps de rechargement entre deux tirs
    /// </summary>
    

    //--------------------------------
    // 2 - Rechargement
    //--------------------------------    

    [SerializeField]
    protected string animatorParameter = ""; //animation à faire
    public string AnimatorParameter
    {
        get { return animatorParameter; }
    }
    [SerializeField]
    protected AudioClip itemSound = null; //son à faire
    public AudioClip ItemSound
    {
        get { return itemSound; }
    }
    [SerializeField]
    protected float itemSoundRate;
    protected float itemSoundCooldown;
    [SerializeField]
    protected float itemVolume = 1f;

   



    void Start()
    {
       
    }

    void Update()
    {
        

    }

    //--------------------------------
    // 3 - Tirer depuis un autre script
    //--------------------------------

    /// <summary>
    /// Création d'un projectile si possible
    /// </summary>
    public virtual void SpawnItem()
    {
        
        //Debug.Log("pew");

        //nouvelle version, pop bullet from factory
        Transform itemTransform = popItem(itemName);
        //Debug.Log("bullet poped");        
        Physics2D.IgnoreCollision(GetComponentInParent<Collider2D>(), itemTransform.GetComponent<Collider2D>());

        // Propriétés du script
        ShotScript shot = itemTransform.gameObject.GetComponent<ShotScript>();

        //make the sound
        if (CanMakeSound)
        {
            MakeSound();
        }

            
        
    }
    protected bool CanMakeSound
    {
        get { return itemSoundCooldown <= 0; }
    }

    protected void MakeSound()
    {
        itemSoundCooldown = itemSoundRate;
        if (itemSound)
            AudioSource.PlayClipAtPoint(itemSound, transform.position, itemVolume);
    }

    /// <summary>
    /// L'arme est chargée ?
    /// </summary>    

    protected Transform popItem(Item.Name itemName)
    {
        Transform itemTransform = null;
        itemTransform = GameObject.Find("Scripts").GetComponent<ItemFactory>().GetItem(itemName);
        // Position
        itemTransform.position = transform.position;
        //Debug.Log(itemTransform.position);
        itemTransform.rotation = transform.rotation;

        itemTransform.GetComponent<Rigidbody2D>().simulated = true;

        
        
        //itemTransform.gameObject.GetComponent<Entity>().enabled = true;
        if (itemTransform.gameObject.GetComponent<Animator>())
            itemTransform.gameObject.GetComponent<Animator>().SetBool("pool", false);
        //Si on doit viser, on calcule les coordonées et on attend l'animation si on est un projectile


        return itemTransform;
    }

    
}
