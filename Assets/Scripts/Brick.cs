using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;

    private void Start()
    {
        gameObject.OnTriggerEnter2DAsObservable()
            .Where(x => x.CompareTag("Bullet"))
            .Select(x => x)
            .Subscribe(x =>
            {
                var effect = Instantiate(effectPrefab, new Vector2(transform.position.x, transform.position.y), transform.localRotation);
                Destroy(gameObject);
                Destroy(effect, 1);
            });
    }

}
