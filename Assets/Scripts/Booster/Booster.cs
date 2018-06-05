using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public abstract class Booster : MonoBehaviour
{
    public int duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            BoosterActivate();
        }
    }

    protected virtual void BoosterActivate()
    {
        BoostersSpawn.countBoosters--;
    }
}
