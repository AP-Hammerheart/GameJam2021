using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSegment : MonoBehaviour
{
    public enum Kloss
    {
        largeLeftEnd, largeMiddleSingle, largeMiddleSingleW, largeRightEnd,
        largeToSmall, SmallLeftEnd, SmallLeftStaircaseW1, SmallMiddleSingle,
        SmallMiddleSingleW, SmallRightEnd, SmallRightStaircase, SmallToLarge
    }

    public Kloss thisKloss;

    public void SetKloss(Kloss kloss)
    {
        thisKloss = kloss;
    }

}
