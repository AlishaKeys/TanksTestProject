using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Protection : Booster
{
    protected override void BoosterActivate()
    {
        base.BoosterActivate();

        gameObject.OnTriggerEnter2DAsObservable()
        .Where(x => x)
        .Select(x => x)
        .Subscribe(x =>
        {
            if(x.CompareTag("Player"))
            {
                gameObject.transform.parent = x.transform;
                gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
                gameObject.transform.localPosition = Vector2.zero;
                gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            }

            Destroy(gameObject, duration);
        });
    }

}
