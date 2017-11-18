using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarmaScript : MonoBehaviour {

    public enum KarmaState
    {
        POSITIVE_KARMA,
        NEUTRAL_KARMA,
        NEGATIVE_KARMA
    }

    static public KarmaState karma = KarmaState.NEUTRAL_KARMA;
}
