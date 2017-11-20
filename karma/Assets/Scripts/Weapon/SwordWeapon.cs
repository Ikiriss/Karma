using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : MonoBehaviour {

    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    private int frameCount = 0;
    [SerializeField]
    private int maxCount = 40;
    [SerializeField]
    private int waitCount = 20; 
    [SerializeField]
    private float maxRotate = 150f; 

    /// <summary>
    /// Temps de rechargement entre deux tirs
    /// </summary>
    [SerializeField]
    protected float attackRate = 0.25f;
    protected float attackCooldown;
    protected bool onAttack = false;

    private BoxCollider2D swordCollider;

    //--------------------------------
    // 2 - Rechargement
    //--------------------------------
    protected bool isEnemy = true;
    [SerializeField]
    protected string weaponAnimationParameter = "attack"; //animation à faire
    public string WeaponAnimationParameter
    {
        get { return weaponAnimationParameter; }
    }


    [SerializeField]
    protected AudioClip weaponSound = null; //son à faire
    public AudioClip WeaponSound
    {
        get { return weaponSound; }
    }
    [SerializeField]
    protected float weaponSoundRate;
    protected float weaponSoundCooldown;
    [SerializeField]
    protected float weaponVolume = 1f;



    // Multiplicateur de dommage permettant de gérer des modes super sayen ou autre
    [SerializeField]
    protected int damageMultiplicator = 1;



    void Start()
    {
        attackCooldown = 0f;
        swordCollider = GetComponent<BoxCollider2D>();
        swordCollider.enabled = false;
    }

    void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            //Debug.Log(shootCooldown);
        }
        if (weaponSoundCooldown > 0)
        {
            weaponSoundCooldown -= Time.deltaTime;
        }

        if(onAttack)
        {
            UpdateSwordColliderMovementOnAttack();
        }


    }

    public virtual bool CanAttack
    {
        get
        {
            return attackCooldown <= 0f;
        }
    }

    //--------------------------------
    // 3 - Tirer depuis un autre script
    //--------------------------------

    /// <summary>
    /// Création d'un projectile si possible
    /// </summary>
    public virtual void Attack()
    {
        if (CanAttack)
        {
            attackCooldown = attackRate;
            //Debug.Log("pew");         
            swordCollider.enabled = true;

            onAttack = true;






        }
    }

    void UpdateSwordColliderMovementOnAttack()
    {
        if(frameCount == maxCount)
        {
            frameCount = 0;
            onAttack = false;
            swordCollider.enabled = false;
            swordCollider.transform.Rotate(new Vector3(0, 0, maxRotate));
        }
        else
        {
            frameCount++;
            if(frameCount > waitCount)
                swordCollider.transform.Rotate(new Vector3(0, 0, -maxRotate / (maxCount - waitCount)));
        }
    }
}
