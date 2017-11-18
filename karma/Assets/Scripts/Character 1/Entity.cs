using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	/// <summary>
	/// Points de vies
	/// </summary>
    [SerializeField]
    protected int hp;
	public int Hp {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField]
    protected int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField]
    protected int maxHp;
    public int MaxHp
    {
        get { return maxHp; }
    }

    [SerializeField]
    protected float attackRate = 0.25f;

    protected float attackCooldown;



    [SerializeField]
    protected EnemyFactory.MobType mobType;

    protected Animator myAnimator;
	//protected Enemy enemyScript;

	void OnTriggerEnter2D(Collider2D collider)
	{
	    
	}

    private void OnDestroy()
    {
        
    }


    protected IEnumerator GiveBulletBackAfterT(float t, Transform bullet)
    {
        bullet.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSecondsRealtime(t);
        GameObject.Find("Scripts").GetComponent<BulletFactory>().GiveBackBullet(bullet.GetComponent<ShotScript>().getBulletType(), bullet);
    }

    protected IEnumerator GiveMobBackAfterT(float t, Transform mob)
    {
        mob.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSecondsRealtime(t);
        GameObject.Find("Scripts").GetComponent<EnemyFactory>().GiveBackMob(mobType, mob);
        bool isBossOnStoryMode = true;
        if (isBossOnStoryMode)
        {
            GameObject.Find("Menu_win").GetComponent<Menu_death>().PopDeathMenu();
        }
    }

    public virtual bool CanAttack
    {
        get
        {
            return attackCooldown <= 0f;
        }
    }

    public virtual void Attack()
    {
        attackCooldown = attackRate;
        //trigger l'animation
    }
}
