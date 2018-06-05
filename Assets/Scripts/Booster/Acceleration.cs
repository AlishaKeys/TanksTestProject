using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Acceleration : Booster
{
	protected override void BoosterActivate()
    {
        base.BoosterActivate();

        gameObject.OnTriggerEnter2DAsObservable()
        .Where(x => x.CompareTag("Player"))
        .Select(x => x)
        .Subscribe(x =>
        {
            x.GetComponent<PlayerController>().speed *= 2;

            gameObject.transform.parent = x.transform;
            gameObject.transform.localPosition = new Vector2(0, -.75f);
            gameObject.transform.localRotation = new Quaternion(-90, 0, 0, 0);

            Observable.Timer(TimeSpan.FromSeconds(duration))
                .Subscribe(_ =>
                {
                    x.GetComponent<PlayerController>().speed /= 2;
                    Destroy(gameObject);
                })
                .AddTo(gameObject);

        });
    }

}
