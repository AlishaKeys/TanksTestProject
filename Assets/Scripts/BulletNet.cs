using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class BulletNet: MonoBehaviour {

    public int speed;
    [SerializeField] GameObject effectPrefab;

    //public void ClientConnect()
    //{
    //    ClientScene.RegisterPrefab(effectPrefab);
    //}

    private void Start()
    {
        //если объект находится вне зоны видимости камеры, то уничтожается
        gameObject.OnBecameInvisibleAsObservable()
            .Subscribe(_ => Destroy(gameObject));

        gameObject.OnTriggerEnter2DAsObservable()
            .Where(x => x.gameObject)
            .Select(x => x)
            .Subscribe(x =>
            {
                AudioManager.Instance.PlaySound("Boom");
                var effect = Instantiate(effectPrefab, new Vector2(transform.position.x, transform.position.y), transform.localRotation);
                //if(NetworkServer.active)
                //NetworkServer.Spawn(effect);
                Destroy(gameObject);
                Destroy(effect, 1);
            });
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
