using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControllerNet : NetworkBehaviour
{
    public float speed;
    public GameObject bulletPrefab;

    private void Awake()
    {
        gameObject.OnTriggerEnter2DAsObservable()
        .Where(c => c)
        .Select(c => c)
        .Take(1)
        .Subscribe(x =>
        {
            AudioManager.Instance.PlaySound("Lose");
            transform.position = Vector3.zero;
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
            CmdBull();
        }

    }



    void Translate(Vector3 direction, int rotationZ)
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationZ), speed * Time.timeScale);
    }

    [Command]
    void CmdBull()
    {
        var bullet = Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y), transform.localRotation);

        AudioManager.Instance.PlaySound("Bull");

        if(NetworkServer.active)
        NetworkServer.Spawn(bullet);        
    }


}
