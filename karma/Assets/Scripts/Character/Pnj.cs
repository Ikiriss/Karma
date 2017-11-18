using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pnj : Entity {

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

    [SerializeField]
    private Name pnjName;
    public Name PnjName
    {
        get { return pnjName; }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PnjEffect()
    {
        switch(pnjName)
        {
            case Name.PERE_ENFANT:
                // TODO complete effect
                break;

            case Name.ACOLYTE_DU_PERE:
                // TODO complete effect
                break;

            case Name.ENFANT:
                // TODO complete effect
                break;

            case Name.MARCHAND:
                // TODO complete effect
                break;

            case Name.CLODO:
                // TODO complete effect
                break;

            case Name.BOUGIE:
                // TODO complete effect
                break;

            case Name.CHEVALIER_DECHU:
                // TODO complete effect
                break;

            case Name.CORBEAU:
                // TODO complete effect
                break;

            case Name.BOSS_FINAL:
                // TODO complete effect
                break;
        }
    }

}
