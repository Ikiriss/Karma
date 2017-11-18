﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Sert à faire des dégats
/// </summary>
public class ShotScript : MonoBehaviour {

	// 1 - Designer variables

	/// <summary>
	/// Points de dégâts infligés
	/// </summary>
    [SerializeField]
	private int damage = 1;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    [SerializeField]
    private BulletFactory.BulletType bulletType;

	/// <summary>
	/// Projectile ami ou ennemi ?
	/// </summary>
    [SerializeField]
	private bool isEnemyShot = false;
    public bool IsEnemyShot
    {
        get { return isEnemyShot; }
        set { isEnemyShot = value; }
    }
    /// <summary>
    /// Est-ce vraiment un projectile ?
    /// </summary>
	//public bool isNotShot = false;

	void Start()
	{
        
    }

    private void Update()
    {
        //on retourne le bullet si il n'est pas visible
        if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false /*&& !isNotShot*/ && gameObject.GetComponent<MoveScript>().enabled)
        {
            GameObject.Find("Scripts").GetComponent<BulletFactory>().GiveBackBullet(bulletType, GetComponent<Transform>());
        }
    }

    public BulletFactory.BulletType getBulletType()
    {
        return bulletType;
    }
}
