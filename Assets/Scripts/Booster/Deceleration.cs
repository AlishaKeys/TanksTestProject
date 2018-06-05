using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Deceleration : Booster
{
    protected override void BoosterActivate()
    {
        base.BoosterActivate();
        var enemies = FindObjectsOfType<EnemyController>();

        foreach(var enemy in enemies)
        {
            enemy.speed /= 2;
        }

        gameObject.SetActive(false);

        Observable.Timer(TimeSpan.FromSeconds(duration))
            .Subscribe(_ =>
            {
                foreach (var enemy in enemies)
                {
                    enemy.speed *= 2;
                }
                Destroy(gameObject);
            })
            .AddTo(gameObject);
    }
}
