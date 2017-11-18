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
    private EnemyFactory.MobType mobType;

    private Animator myAnimator;
	private Enemy enemyScript;



    void Start()
    {
        maxHp = hp;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
	
	}

    private void OnDestroy()
    {
        
    }


    private IEnumerator GiveBulletBackAfterT(float t, Transform bullet)
    {
        bullet.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSecondsRealtime(t);
        GameObject.Find("Scripts").GetComponent<BulletFactory>().GiveBackBullet(bullet.GetComponent<ShotScript>().getBulletType(), bullet);
    }

    private IEnumerator GiveMobBackAfterT(float t, Transform mob)
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
}
