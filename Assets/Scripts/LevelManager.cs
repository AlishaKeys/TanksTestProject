using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform spawn;

    [SerializeField] GameObject riverPrefab;
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject brickPrefab;
    [SerializeField] GameObject emptyPrefab;

    List<Transform> listObstacles;

    GameObject[,] tiles;

    Camera cam;

    const float STEP_POS = 1f;
    const int WIDTH = 12;
    const int HEIGHT = 10;
    const float CHANCE_SPAWN_RIVER = .05f;
    const float CHANCE_SPAWN_WALL = .1f;
    const float CHANCE_SPAWN_BRICK = .15f;
    float minX;
    float maxX;
    float minY;
    float maxY;

    public BoostersSpawn boosterSpawn;

    void Start()
    {
        GameManager.OnGenerationLevel += StartLevel;
        GameManager.OnReset += LoseLevel;
    }

    public void StartLevel()
    {
        GenerationObstacle();
        CreateObstacle();

        var listEmpty = listObstacles.Where(x => x.tag == "Empty").Select(x => x).ToList();
        boosterSpawn.GenerationBooster(listEmpty, spawn);

    }

    void GenerationObstacle()
    {
        cam = Camera.main;
        tiles = new GameObject[WIDTH, HEIGHT];

        var hfHeight = HEIGHT / 2;
        var hfWidth = WIDTH / 2;

        var spawnPrefab = emptyPrefab;

        listObstacles = new List<Transform>();

        for (int i = -hfWidth; i < hfWidth; ++i)
        {
            for (int j = -hfHeight; j < hfHeight; ++j)
            {
                var randomObstacle = UnityEngine.Random.value;
                if (randomObstacle < CHANCE_SPAWN_RIVER)
                {
                    spawnPrefab = riverPrefab;
                }
                else if (randomObstacle > CHANCE_SPAWN_RIVER && randomObstacle < CHANCE_SPAWN_WALL)
                {
                    spawnPrefab = wallPrefab;
                }
                else if (randomObstacle > CHANCE_SPAWN_WALL && randomObstacle < CHANCE_SPAWN_BRICK)
                {
                    spawnPrefab = brickPrefab;
                }
                else
                {
                    spawnPrefab = emptyPrefab;
                }
                tiles[i + hfWidth, j + hfHeight] = Instantiate(spawnPrefab, new Vector2(i * STEP_POS, j * STEP_POS), spawnPrefab.transform.localRotation, spawn);
            }
        }
    }

    void CreateObstacle()
    {
        Vector3 upperRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 lowerLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));

        var coeffShift = .25f;

        if (upperRight.x > maxX - STEP_POS)
        {
            ShiftAll(Vector3.right);
        }

        if (lowerLeft.x < minX + STEP_POS)
        {
            ShiftAll(Vector3.left);
        }

        if (upperRight.y > maxY - STEP_POS)
        {
            ShiftAll(Vector3.up * coeffShift);
        }

        if (lowerLeft.y < minY + STEP_POS)
        {
            ShiftAll(Vector3.down * -coeffShift);
        }
    }

    void ShiftAll(Vector3 dir)
    {
        for (int i = 0; i < WIDTH; ++i)
        {
            for (int j = 0; j < HEIGHT; ++j)
            {
                tiles[i, j].transform.position += dir * STEP_POS;
                listObstacles.Add(tiles[i, j].transform);
            }
        }
        minX = tiles[0, 0].transform.position.x;
        maxX = tiles[WIDTH - 1, HEIGHT - 1].transform.position.x;
        minY = tiles[0, 0].transform.position.y;
        maxY = tiles[WIDTH - 1, HEIGHT - 1].transform.position.y;
    }

    void LoseLevel()
    {
        foreach (Transform child in spawn.transform)
        {
            Destroy((child as Transform).gameObject);
        }
    }
}