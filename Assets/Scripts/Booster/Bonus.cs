using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Booster
{
    protected override void BoosterActivate()
    {
        base.BoosterActivate();

        GameManager.Instance.AddScore(10);
        Destroy(gameObject);
    }
}
