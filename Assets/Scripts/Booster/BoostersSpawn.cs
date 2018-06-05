using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;

public class BoostersSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] boosters;
    public static int countBoosters = 0;
    public int maxBoosters;

    public void GenerationBooster(List<Transform> listEmpty, Transform spawn) 
    {
        Observable.Timer(System.TimeSpan.FromSeconds(10f))
            .RepeatUntilDisable(gameObject)
            .Subscribe(_ =>
            {
                if(countBoosters < maxBoosters)
                {
                    if (spawn && listEmpty != null)
                    {
                        countBoosters++;
                        var randomBooster = Random.Range(0, boosters.Length);
                        var randomEmptyPoint = Random.Range(0, listEmpty.Count());
                        var booster = Instantiate(boosters[randomBooster], listEmpty[randomEmptyPoint].transform.position, boosters[randomBooster].transform.localRotation, spawn);
                    }
                }

            })
            .AddTo(gameObject);
    }
}
