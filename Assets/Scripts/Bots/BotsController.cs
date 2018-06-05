using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

public abstract class BotsController : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    protected bool shot = true;

    protected void Shot()
    {
        var bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y), transform.localRotation);
        shot = false;

        AudioManager.Instance.PlaySound("Bull");

        //ограничение стрельбы
        Observable.Timer(TimeSpan.FromSeconds(1))
            .Subscribe(_ => shot = true);
    }
}
