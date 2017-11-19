using System.Collections;
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
    private Vector3 previousPos;
    public Vector3 PreviousPos
    {
        set { previousPos = value; }
    }
    

	void Start()
	{
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlatformEffector2D>())
        {
            Wall wall = collision.collider.GetComponent<Wall>();
            if(wall && wall.BulletThrough && wall.IsInList(bulletType))
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
            }
            else
            {
                ReturnToTheFactory();
            }
            
        }
    }

    private void Update()
    {
        if (previousPos != null)
        {
            Vector3 direction = transform.position - previousPos;

            if (direction != new Vector3(0, 0, 0))
                /*transform.eulerAngles= new Vector3(0, 0, -Vector3.Dot(direction, new Vector3(1, 0, 0) / Vector3.Distance(direction, new Vector3(0, 0, 0))));*/
                transform.Rotate(0, 0, -Vector3.Dot(direction, new Vector3(1, 0, 0) / Vector3.Distance(direction, new Vector3(0, 0, 0))));
        }
        

        //on retourne le bullet si il n'est pas visible
        if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false /*&& !isNotShot*/ && gameObject.GetComponent<MoveScript>().enabled)
        {
            GameObject.Find("Scripts").GetComponent<BulletFactory>().GiveBackBullet(bulletType, GetComponent<Transform>());
        }
        previousPos = transform.position;
    }

    public BulletFactory.BulletType getBulletType()
    {
        return bulletType;
    }

    public void ReturnToTheFactory()
    {
        GameObject.Find("Scripts").GetComponent<BulletFactory>().GiveBackBullet(bulletType, GetComponent<Transform>());
    }
}
