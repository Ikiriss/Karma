using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pnj : Entity {

    private Rigidbody2D rigidbody;
    [SerializeField]
    private Vector2 speed = new Vector2(10, 10);
    public Vector2 Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    [SerializeField]
    private float margePnjActivation = 2f;

    [SerializeField]
    private Transform linkedPnj;

    [SerializeField]
    private bool attackPattern = false;
    public bool AttackPattern
    {
        get { return attackPattern; }
        set { attackPattern = value; }
    }
    [SerializeField]
    private bool isAttacked = false;
    public bool IsAttacked
    {
        get { return isAttacked; }
        set { isAttacked = value; }
    }
    [SerializeField]
    private bool isCacAttack = false;
    public bool IsCacAttack
    {
        get { return isCacAttack; }
        set { isCacAttack = value; }
    }

    Player player;
    WeaponScript weapon;


    private bool isEvenementTrigger = false;
    public bool IsEvenementTrigger
    {
        get { return IsEvenementTrigger; }
        set { isEvenementTrigger = value; }
    }

    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public enum Name
    {
        PERE_ENFANT,
        ACOLYTE_DU_PERE,
        ENFANT,
        MARCHAND,
        VOLEUR,
        VICTIME_VOLEUR,
        CLODO,
        BOUGIE,
        SERPENT_CHANDELIER,
        CHEVALIER_DE_LA_LUNE,
        CHEVALIER_DECHU,
        CORBEAU,
        BOSS_FINAL
    }

    public enum State
    {
        ANGRY,
        HAPPY,
        NEUTRAL
    }

    [SerializeField]
    private State pnjState;
    public State PnjState
    {
        get { return pnjState; }
        set { pnjState = value; }
    }

    [SerializeField]
    private Name pnjName;
    public Name PnjName
    {
        get { return pnjName; }
    }

    [SerializeField]
    private float bossReturnCooldown = 2f;
    private float bossReturnCount = 0f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindObjectOfType<Player>();
        InitPnjState();
        weapon = GetComponentInChildren<WeaponScript>();
        if(weapon)
        {
            weapon.enabled = false;
        }
        rigidbody = GetComponent<Rigidbody2D>();
        if(pnjName == Name.BOSS_FINAL)
        {

        }
        else
        {
            hp = 2;
            damage = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        PnjAction();
    }

    void InitPnjState()
    {
        switch (pnjName)
        {
            case Name.PERE_ENFANT:
                pnjState = State.NEUTRAL;
                break;

            case Name.ACOLYTE_DU_PERE:
                pnjState = State.NEUTRAL;
                break;

            case Name.ENFANT:
                pnjState = State.HAPPY;
                break;

            case Name.MARCHAND:
                if (KarmaScript.karma == KarmaScript.KarmaState.NEGATIVE_KARMA)
                {
                    pnjState = State.ANGRY;
                }
                else
                {
                    pnjState = State.NEUTRAL;
                }
                break;

            case Name.CLODO:
                if (KarmaScript.karma == KarmaScript.KarmaState.NEGATIVE_KARMA)
                {
                    pnjState = State.ANGRY;
                }
                else
                {
                    pnjState = State.NEUTRAL;
                }
                break;

            case Name.BOUGIE:
                if (KarmaScript.karma == KarmaScript.KarmaState.NEGATIVE_KARMA)
                {
                    pnjState = State.ANGRY;
                }
                else if (KarmaScript.karma == KarmaScript.KarmaState.POSITIVE_KARMA)
                {
                    pnjState = State.HAPPY;
                }
                else
                {
                    pnjState = State.NEUTRAL;
                }
                break;

            case Name.CHEVALIER_DECHU:
                pnjState = State.NEUTRAL;
                break;

            case Name.CORBEAU:
                pnjState = State.NEUTRAL;
                break;

            case Name.BOSS_FINAL:
                pnjState = State.NEUTRAL;
                break;
        }
    }



    void PnjAction()
    {
        switch (KarmaScript.karma)
        {
            case KarmaScript.KarmaState.NEUTRAL_KARMA:
                switch (pnjName)
                {
                    case Name.PERE_ENFANT:
                        // TODO complete effect
                        if(attackPattern)
                        {
                            if(transform.position.y > player.transform.position.y + 0.2)
                            {
                                rigidbody.velocity = new Vector2(-speed.x,0);
                            }
                            else
                            {
                                weapon.enabled = true;
                                if (weapon != null && weapon.enabled && weapon.CanAttack)
                                {
                                    if (GetComponent<Animator>())
                                        GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                    weapon.Attack(true);
                                    //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                }
                            }
                        }
                        else
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 perePosition = transform.position;
                            if( transform.position.y < player.transform.position.y)
                            {
                                if(player.transform.position.x < transform.position.x)
                                {
                                    attackPattern = true;
                                }
                                
                            }
                            if(isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.MARCHAND:
                        // TODO complete effect
                        if(attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 marchandPosition = transform.position;
                            if(playerPosition.x < marchandPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            if(isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CLODO:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 clodoPosition = transform.position;
                            if (playerPosition.x < clodoPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.BOUGIE:
                        // TODO complete effect
                        attackPattern = true;
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 clodoPosition = transform.position;
                            Vector3 direction = playerPosition - clodoPosition;
                            direction.Normalize();
                            rigidbody.velocity = speed.x * direction;
                        }
                        else
                        {
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CHEVALIER_DECHU:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 chevalierPosition = transform.position;
                            if (playerPosition.x < chevalierPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 chevalierPosition = transform.position;
                            if (playerPosition.x > chevalierPosition.x)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CORBEAU:
                        // TODO complete effect
                        break;

                    case Name.BOSS_FINAL:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 bossPosition = transform.position;
                            if (transform.eulerAngles.y == 180)
                            {
                                if (playerPosition.x < bossPosition.x)
                                {
                                    
                                    weapon.enabled = true;
                                    bossReturnCount = 0f;
                                    if (weapon != null && weapon.enabled && weapon.CanAttack)
                                    {
                                        if (GetComponent<Animator>())
                                            GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                        weapon.Attack(true);
                                        //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                    }
                                }
                                else
                                {
                                    
                                    weapon.enabled = false;
                                    if (bossReturnCount > bossReturnCooldown)
                                    {
                                        transform.Rotate(0, -180, 0);
                                        bossReturnCount = 0f;
                                    }
                                    bossReturnCount += Time.deltaTime;
                                }
                            }
                            else
                            {
                                {
                                    if (playerPosition.x > bossPosition.x)
                                    {
                                        
                                        weapon.enabled = true;
                                        bossReturnCount = 0f;
                                        if (weapon != null && weapon.enabled && weapon.CanAttack)
                                        {
                                            if (GetComponent<Animator>())
                                                GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                            weapon.Attack(true);
                                            //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                        }
                                    }
                                    else
                                    {
                                        
                                        weapon.enabled = false;
                                        if (bossReturnCount > bossReturnCooldown)
                                        {
                                            transform.Rotate(0, 180, 0);
                                            bossReturnCount = 0f;
                                        }
                                        bossReturnCount += Time.deltaTime;
                                    }
                                }
                            }
                        }
                        break;
                }
                break;

            case KarmaScript.KarmaState.NEGATIVE_KARMA:
                switch (pnjName)
                {
                    case Name.PERE_ENFANT:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            if (transform.position.y > player.transform.position.y + 0.2)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                weapon.enabled = true;
                                if (weapon != null && weapon.enabled && weapon.CanAttack)
                                {
                                    if (GetComponent<Animator>())
                                        GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                    weapon.Attack(true);
                                    //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                }
                            }
                        }
                        else
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 perePosition = transform.position;
                            if (transform.position.y < player.transform.position.y)
                            {
                                if (player.transform.position.x < transform.position.x)
                                {
                                    attackPattern = true;
                                }

                            }
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.ACOLYTE_DU_PERE:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            if (transform.position.y > player.transform.position.y + 0.2)
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                            else
                            {
                                weapon.enabled = true;
                                if (weapon != null && weapon.enabled && weapon.CanAttack)
                                {
                                    if (GetComponent<Animator>())
                                        GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                    weapon.Attack(true);
                                    //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                }
                            }
                        }
                        else
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 acolytePosition = transform.position;
                            if (transform.position.y < player.transform.position.y)
                            {
                                if (player.transform.position.x < transform.position.x)
                                {
                                    attackPattern = true;
                                }

                            }
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.MARCHAND:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 marchandPosition = transform.position;
                            if (playerPosition.x < marchandPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CLODO:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 clodoPosition = transform.position;
                            if (playerPosition.x < clodoPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                            Vector3 playerPosition = player.transform.position;
                            Vector3 clodoPosition = transform.position;
                            if (playerPosition.x > clodoPosition.x)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.BOUGIE:
                        // TODO complete effect
                        attackPattern = true;
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 bougiePosition = transform.position;
                            Vector3 direction = playerPosition - bougiePosition;
                            direction.Normalize();
                            rigidbody.velocity = speed.x * direction;
                        }
                        else
                        {
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CHEVALIER_DECHU:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 chevalierPosition = transform.position;
                            if (playerPosition.x < chevalierPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 chevalierPosition = transform.position;
                            if (playerPosition.x > chevalierPosition.x)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CORBEAU:
                        // TODO complete effect
                        if(attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 clodoPosition = transform.position;
                            Vector3 direction = playerPosition - clodoPosition;
                            direction.Normalize();
                            rigidbody.velocity = speed.x * direction;
                        }
                        else
                        {
                            if(isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.BOSS_FINAL:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 bossPosition = transform.position;
                            if (transform.eulerAngles.y == 180)
                            {
                                if (playerPosition.x < bossPosition.x)
                                {
                                    weapon.enabled = true;
                                    bossReturnCount = 0f;
                                    if (weapon != null && weapon.enabled && weapon.CanAttack)
                                    {
                                        if (GetComponent<Animator>())
                                            GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                        weapon.Attack(true);
                                        //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                    }
                                }
                                else
                                {
                                    weapon.enabled = false;
                                    if (bossReturnCount > bossReturnCooldown)
                                    {
                                        transform.Rotate(0, 180, 0);
                                        bossReturnCount = 0f;
                                    }
                                    bossReturnCount += Time.deltaTime;
                                }
                            }
                            else if (transform.eulerAngles.y == 0)
                            {
                                {
                                    if (playerPosition.x > bossPosition.x)
                                    {
                                        weapon.enabled = true;
                                        if (weapon != null && weapon.enabled && weapon.CanAttack)
                                        {
                                            if (GetComponent<Animator>())
                                                GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                            weapon.Attack(true);
                                            //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                        }
                                    }
                                    else
                                    {
                                        weapon.enabled = false;
                                        if (bossReturnCount > bossReturnCooldown)
                                        {
                                            transform.Rotate(0, -180, 0);
                                            bossReturnCount = 0f;
                                        }
                                        bossReturnCount += Time.deltaTime;
                                    }
                                }
                            }
                        }
                        break;
                }
                break;

            case KarmaScript.KarmaState.POSITIVE_KARMA:
                switch (pnjName)
                {
                    case Name.PERE_ENFANT:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            if (transform.position.y > player.transform.position.y + 0.2)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                weapon.enabled = true;
                                if (weapon != null && weapon.enabled && weapon.CanAttack)
                                {
                                    if (GetComponent<Animator>())
                                        GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                    weapon.Attack(true);
                                    //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                }
                            }
                        }
                        else
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 perePosition = transform.position;
                            if (transform.position.y < player.transform.position.y)
                            {
                                if (player.transform.position.x < transform.position.x)
                                {
                                    attackPattern = true;
                                }

                            }
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.ENFANT:
                        // TODO complete effect
                        
                        break;

                    case Name.MARCHAND:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 marchandPosition = transform.position;
                            if (playerPosition.x < marchandPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CLODO:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 clodoPosition = transform.position;
                            if (playerPosition.x < clodoPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            if (isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.BOUGIE:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 clodoPosition = transform.position;
                            Vector3 direction = playerPosition - clodoPosition;
                            direction.Normalize();
                            rigidbody.velocity = speed.x * direction;
                        }
                        else
                        {
                            if(isAttacked)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CHEVALIER_DECHU:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 chevalierPosition = transform.position;
                            if (playerPosition.x < chevalierPosition.x)
                            {
                                rigidbody.velocity = new Vector2(-speed.x, 0);
                            }
                            else
                            {
                                rigidbody.velocity = new Vector2(speed.x, 0);
                            }
                        }
                        else
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 chevalierPosition = transform.position;
                            if (playerPosition.x > chevalierPosition.x)
                            {
                                attackPattern = true;
                            }
                        }
                        break;

                    case Name.CORBEAU:
                        // TODO complete effect

                        break;

                    case Name.BOSS_FINAL:
                        // TODO complete effect
                        if (attackPattern)
                        {
                            Vector3 playerPosition = player.transform.position;
                            Vector3 bossPosition = transform.position;
                            if (transform.eulerAngles.y == 180)
                            {
                                if (playerPosition.x < bossPosition.x)
                                {
                                    weapon.enabled = true;
                                    bossReturnCount = 0f;
                                    if (weapon != null && weapon.enabled && weapon.CanAttack)
                                    {
                                        if (GetComponent<Animator>())
                                            GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                        weapon.Attack(true);
                                        //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                    }
                                }
                                else
                                {
                                    weapon.enabled = false;
                                    if (bossReturnCount > bossReturnCooldown)
                                    {
                                        transform.Rotate(0, 180, 0);
                                        bossReturnCount = 0f;
                                    }
                                    bossReturnCount += Time.deltaTime;
                                }
                            }
                            else if (transform.eulerAngles.y == 0)
                            {
                                {
                                    if (playerPosition.x > bossPosition.x)
                                    {
                                        weapon.enabled = true;
                                        if (weapon != null && weapon.enabled && weapon.CanAttack)
                                        {
                                            if (GetComponent<Animator>())
                                                GetComponent<Animator>().SetTrigger(weapon.AnimatorParameter);
                                            weapon.Attack(true);
                                            //SoundEffectsHelper.Instance.MakeEnemyShotSound();                
                                        }
                                    }
                                    else
                                    {
                                        weapon.enabled = false;
                                        if (bossReturnCount > bossReturnCooldown)
                                        {
                                            transform.Rotate(0, -180, 0);
                                            bossReturnCount = 0f;
                                        }
                                        bossReturnCount += Time.deltaTime;
                                    }
                                }
                            }
                        }
                        break;
                }
                break;
    }
}

    void PnjEffect(bool specialEventTrigger = false)
    {
        switch (KarmaScript.karma)
        {
            case KarmaScript.KarmaState.NEUTRAL_KARMA:
                switch (pnjName)
                {
                    case Name.PERE_ENFANT:
                        // TODO complete effect
                        if (isDead)
                            Player.karma--;
                        else
                        {
                            Player.karma++;
                            Pnj fille = linkedPnj.GetComponent<Pnj>();
                            fille.pnjState = State.HAPPY;
                        }
                        break;

                    case Name.MARCHAND:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma--;
                        }
                        isEvenementTrigger = true;
                        break;

                    case Name.CLODO:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma += 2;
                        }
                        else
                        {
                            Player.karma -= 2;
                        }
                        isEvenementTrigger = true;
                        break;

                    case Name.BOUGIE:
                        // TODO complete effect
                        if (isDead)
                        {
                            Player.karma--;
                        }
                        else
                        {
                            Player.karma++;
                            isEvenementTrigger = true;
                        }    
                        break;

                    case Name.CHEVALIER_DECHU:
                        // TODO complete effect
                        Player.karma--;
                        break;

                    case Name.CORBEAU:
                        // TODO complete effect
                        break;

                    case Name.BOSS_FINAL:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma--;
                        }
                        break;
                }
                break;

            case KarmaScript.KarmaState.NEGATIVE_KARMA:
                switch (pnjName)
                {
                    case Name.PERE_ENFANT:
                        // TODO complete effect
                        if (isDead)
                            Player.karma--;
                        else
                        {
                            Player.karma++;
                            Pnj fille = linkedPnj.GetComponent<Pnj>();
                            fille.pnjState = State.HAPPY;
                        }                           
                        break;

                    case Name.ACOLYTE_DU_PERE:
                        // TODO complete effect
                        if (isDead)
                            Player.karma--;
                        else
                            Player.karma++;
                        break;

                    case Name.MARCHAND:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma--;
                            isEvenementTrigger = true;
                        }
                        else
                        {
                            Player.karma++;
                        }
                        break;

                    case Name.CLODO:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma--;
                        }
                        else
                        {
                            Player.karma++;
                        }
                        isEvenementTrigger = true;
                        break;

                    case Name.BOUGIE:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma--;
                        }
                        else
                        {
                            isEvenementTrigger = true;
                        }
                        break;

                    case Name.CHEVALIER_DECHU:
                        // TODO complete effect
                        Player.karma--;
                        break;

                    case Name.CORBEAU:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma--;
                        }
                        else
                        {
                            Player.karma++;
                        }
                        break;

                    case Name.BOSS_FINAL:
                        // TODO complete effect
                        if (isDead)
                        {
                            Player.karma--;
                        }
                        break;
                }
                break;

            case KarmaScript.KarmaState.POSITIVE_KARMA:
                switch (pnjName)
                {
                    case Name.PERE_ENFANT:
                        // TODO complete effect
                        if (isDead)
                            Player.karma--;
                        else
                        {
                            Player.karma++;
                            Pnj fille = linkedPnj.GetComponent<Pnj>();
                            fille.pnjState = State.HAPPY;
                        }                     
                        break;

                    case Name.ENFANT:
                        // TODO complete effect
                        Pnj pere = linkedPnj.GetComponent<Pnj>();
                        if (!pere.IsDead)
                        {
                            isEvenementTrigger = true;
                        }
                        break;

                    case Name.MARCHAND:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma--;
                        }
                        isEvenementTrigger = true;
                        break;

                    case Name.CLODO:
                        // TODO complete effect
                        if(specialEventTrigger)
                        {
                            Player.karma++;
                            isEvenementTrigger = true;
                        }
                        else
                        {
                            if(isDead)
                            {
                                Player.karma -= 2;
                                isEvenementTrigger = true;
                            }
                            else
                            {
                                Player.karma--;
                            }
                        }
                        break;

                    case Name.BOUGIE:
                        // TODO complete effect
                        if(isDead)
                        {
                            Player.karma--;
                        }
                        else
                        {
                            isEvenementTrigger = true;
                        }
                        break;

                    case Name.CHEVALIER_DECHU:
                        // TODO complete effect
                        if(specialEventTrigger)
                        {
                            Player.karma++;
                            pnjState = State.HAPPY;
                        }
                        else
                        {
                            Player.karma--;
                        }
                        break;

                    case Name.CORBEAU:
                        // TODO complete effect
                        break;

                    case Name.BOSS_FINAL:
                        // TODO complete effect
                        if (isDead)
                        {
                            Player.karma--;
                        }
                        break;
                }
                break;
        }
        
    }

}


    //    switch (KarmaScript.karma)
    //    {
    //        case KarmaScript.KarmaState.NEUTRAL_KARMA:
    //            switch (pnjName)
    //            {
    //                case Name.PERE_ENFANT:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.MARCHAND:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CLODO:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.BOUGIE:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CHEVALIER_DECHU:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CORBEAU:
    //                    // TODO complete effect
    //                    break;

    //                case Name.BOSS_FINAL:
    //                    // TODO complete effect
                        
    //                    break;
    //            }
    //            break;

    //        case KarmaScript.KarmaState.NEGATIVE_KARMA:
    //            switch (pnjName)
    //            {
    //                case Name.PERE_ENFANT:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.ACOLYTE_DU_PERE:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.MARCHAND:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CLODO:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.BOUGIE:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CHEVALIER_DECHU:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CORBEAU:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.BOSS_FINAL:
    //                    // TODO complete effect
                        
    //                    break;
    //            }
    //            break;

    //        case KarmaScript.KarmaState.POSITIVE_KARMA:
    //            switch (pnjName)
    //            {
    //                case Name.PERE_ENFANT:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.ENFANT:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.MARCHAND:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CLODO:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.BOUGIE:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CHEVALIER_DECHU:
    //                    // TODO complete effect
                        
    //                    break;

    //                case Name.CORBEAU:
    //                    // TODO complete effect

    //                    break;

    //                case Name.BOSS_FINAL:
    //                    // TODO complete effect
                        
    //                    break;
    //            }
    //            break;
    //}