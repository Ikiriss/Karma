using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour {

    public enum Name
    {
        FLEUR,
        EPEE_EVENT2,
        OEUF_CORBEAU,
        ALLUMETTES,
        BAGUETTE_MAGIQUE,
        PLANTE_MAGIQUE,
        HACHE
    }

    [SerializeField]
    private Name itemName;

    public Name ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    [SerializeField]
    private bool isPicked =  false;

    public bool IsPicked
    {
        get { return isPicked; }
        set { isPicked = value; }
    }

}
