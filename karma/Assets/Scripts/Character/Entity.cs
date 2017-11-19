using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    [SerializeField]
    protected EnemyFactory.MobType mobType;
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

    [SerializeField]
    protected float attackCooldown;



    

    protected Animator myAnimator;

    [SerializeField]
    protected AudioClip attackSound = null;
    //cd en sec
    [SerializeField]
    protected float attackSoundRate = 1;
    protected float attackSoundCooldown;
    [SerializeField]
    protected float attackSoundVolume = 1.0f;

    [SerializeField]
    protected string attackAnimationParameter = "attack";
    protected string AttackAnimationParameter
    {
        get { return attackAnimationParameter; }
    }

    [SerializeField]
    protected AudioClip walkSound = null;
    //protected Enemy enemyScript;
    [SerializeField]
    protected float walkSoundRate = 1;
    protected float walkSoundCooldown;
    [SerializeField]
    protected float walkSoundVolume = 1.0f;

    [SerializeField]
    protected string walkAnimationParameter = "walk";
    protected string WalkAnimationParameter
    {
        get { return walkAnimationParameter; }
    }

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

    protected void GiveMobBack(Transform mob)
    {
        mob.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        GameObject.Find("Scripts").GetComponent<EnemyFactory>().GiveBackMob(mobType, mob);
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

    public virtual bool CanWalkSound
    {
        get
        {
            return walkSoundCooldown <= 0f;
        }
    }

    public virtual void MakeWalkSound()
    {
        walkSoundCooldown = walkSoundRate;
        if(walkSound)
            AudioSource.PlayClipAtPoint(walkSound, transform.position,walkSoundVolume);
    }

    public virtual bool CanAttackSound
    {
        get
        {
            return attackSoundCooldown <= 0;
        }
    }

    public virtual void MakeAttackSound()
    {
        attackSoundCooldown = attackSoundRate;
        if(attackSound)
            AudioSource.PlayClipAtPoint(attackSound, transform.position, attackSoundVolume);
    }

    public virtual void MakeWalkAnimation()
    {
        
        if (myAnimator)
        {
            myAnimator.SetTrigger(walkAnimationParameter);
        }
    }

    public virtual void MakeAttackAnimation()
    {
        if (myAnimator)
        {
            myAnimator.SetTrigger(attackAnimationParameter);
        }
    }
}
