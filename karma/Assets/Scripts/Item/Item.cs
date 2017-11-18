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
        ARC,
        PLANTE_MAGIQUE,
        HACHE
    }

    [SerializeField]
    private Name itemName;

    public Name ItemName
    {
        get { return itemName; }
    }
}
