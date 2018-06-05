using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyController : BotsController
{
    [SerializeField] Sprite[] bulletsSprites;

    Vector3[] directions = new Vector3[] { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
    int[] rotations = new int[] { 0, 180, 270, 90 };

    int randomDir;

    [SerializeField] Transform raycastOrigin;

    private void Awake()
    {
        var randomBulletSprite = Random.Range(0, bulletsSprites.Length);
        bulletPrefab.GetComponent<SpriteRenderer>().sprite = bulletsSprites[randomBulletSprite];

        gameObject.OnTriggerEnter2DAsObservable()
            .Where(c => c.CompareTag("Bullet"))
            .Select(c => c)
            .Take(1)
            .Subscribe(_ =>
            {
                BotsSpawn.countEnemy--;
                GameManager.Instance.AddScore(1);
                Destroy(gameObject);
            })
            .AddTo(this);

        Observable.Timer(System.TimeSpan.FromSeconds(.3f))
            .RepeatUntilDestroy(gameObject)
            .Subscribe(_ =>
            {
                RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, directions[randomDir], 10);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        if (shot)
                        {
                            Shot();
                        }
                    }
                    else
                    {
                        float distance = Mathf.Abs(hit.point.y - transform.position.y);

                        if (distance < .4f)
                        {
                            RandomDir();
                        }
                    }
                }

            })
            .AddTo(gameObject);

        Observable.Timer(System.TimeSpan.FromSeconds(3f))
            .RepeatUntilDestroy(gameObject)
            .Subscribe(_ =>
            {
                RandomDir();
            })
            .AddTo(gameObject);

    }

    private void Update()
    {

        transform.position += directions[randomDir] * speed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotations[randomDir]), speed * Time.timeScale);
    }

    void RandomDir()
    {
        randomDir = Random.Range(0, directions.Length);
    }
}
