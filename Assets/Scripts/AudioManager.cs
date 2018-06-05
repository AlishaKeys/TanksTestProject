using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }

    public AudioClip musicClip;
    public List<AudioClip> soundsClips;

    private AudioSource musicSource;

    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.clip = musicClip;
        musicSource.Play();

        musicSource.volume = 1;
    }

    public void PlaySound(string filename)
    {
        var cl = soundsClips.Find(x => x.name == filename);

        var src = gameObject.AddComponent<AudioSource>();
        src.spatialBlend = 0;
        src.clip = cl;
        src.Play();

        Observable.ReturnUnit()
        .Delay(TimeSpan.FromSeconds(cl.length + .1f), Scheduler.MainThreadIgnoreTimeScale)
        .Subscribe(_ => Destroy(src)).AddTo(this);
    }
}
