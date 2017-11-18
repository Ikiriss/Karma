using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pnj : Entity {

    [SerializeField]
    private Transform linkedPnj;


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


    // Use this for initialization
    void Start () {
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
	
	// Update is called once per frame
	void Update () {
		
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
                            Player.karma++;
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
                            Player.karma++;
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
                            Player.karma++;
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
