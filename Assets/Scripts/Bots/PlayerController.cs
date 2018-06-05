using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerController : BotsController
{
    private void Awake()
    {
        gameObject.OnTriggerEnter2DAsObservable()
        .Where(c => c.CompareTag("Bullet"))
        .Select(c => c)
        .Take(1)
        .Subscribe(_ =>
        {
            AudioManager.Instance.PlaySound("Lose");
            GameManager.Instance.LoseGame();
        })
        .AddTo(gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Translate(Vector3.up, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Translate(Vector3.down, 180);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Translate(Vector3.left, 90);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Translate(Vector3.right, 270);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (shot)
            {
                Shot();
            }
        }
    }

    void Translate(Vector3 direction, int rotationZ)
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationZ), speed * Time.timeScale);
    }
}
