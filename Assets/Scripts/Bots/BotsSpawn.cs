using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

public class BotsSpawn : MonoBehaviour
{
    [SerializeField] Transform spawn;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Sprite[] enemySprites;

    [SerializeField] Vector2 pointSpawnPlayer;
    [SerializeField] Vector2 pointSpawnEnemy;
    public int maxEnemy;
    public static int countEnemy;

    private void Start()
    {
        GameManager.OnGenerationLevel += Spawn;
    }

    void Spawn()
    {
        SpawnPlayer();
        Observable.Timer(System.TimeSpan.FromSeconds(5))
            .RepeatUntilDestroy(this)
            .Subscribe(_ =>
            {
                if (countEnemy < maxEnemy)
                {
                    countEnemy++;
                    SpawnEnemy();
                }
            })
            .AddTo(this);

    }

    void SpawnNetwork()
    {
        SpawnPlayer();

        Observable.Timer(System.TimeSpan.FromSeconds(5))
            .RepeatUntilDestroy(this)
            .Subscribe(_ =>
            {
                if (countEnemy < maxEnemy)
                {
                    countEnemy++;
                    SpawnEnemy();
                }
            })
            .AddTo(this);

    }

    void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, pointSpawnPlayer, playerPrefab.transform.rotation, spawn);
    }

    void SpawnEnemy()
    {
        var randomEnemySprite = Random.Range(0, enemySprites.Length);
        var enemy = Instantiate(enemyPrefab, pointSpawnEnemy, enemyPrefab.transform.rotation, spawn);
        enemy.GetComponent<SpriteRenderer>().sprite = enemySprites[randomEnemySprite];
    }
}
