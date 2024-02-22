using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SfxSpeaker : MonoBehaviour
{
    public IObjectPool<SfxSpeaker> Pool { get; set; }
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.mute = false;
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(CheckAudioEndCoroutine());
    }

    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    private IEnumerator CheckAudioEndCoroutine()
    {
        while(audioSource.isPlaying)
            yield return null;

        ReturnToPool();
    }
}
