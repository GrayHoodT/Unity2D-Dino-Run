using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AudioController : MonoBehaviour
{
    private IObjectPool<SfxSpeaker> sfxPool;
    private SfxSpeaker sfxSpeakerPrefab;

    private void Awake()
    {
        sfxSpeakerPrefab = Resources.Load<SfxSpeaker>(Defines.SFX_SPEAKER_PREFAB_PATH);
    }

    private void Start()
    {
        sfxPool = new ObjectPool<SfxSpeaker>(CreateSfxSpeaker, OnSfxSpeakerGot, OnSfxSpeakerReleased, OnSfxSpeakerDestroyed);
    }

    public bool Initialize(out AudioEvents audioEvent)
    {
        audioEvent = new AudioEvents();

        //TODO: AudioEvent 기능 구현 후, 콜백 메서드 할당하기.

        return true;
    }

    #region Pool Callbacks

    private SfxSpeaker CreateSfxSpeaker()
    {
        var sfxSpeaker = Instantiate<SfxSpeaker>(sfxSpeakerPrefab, transform);
        sfxSpeaker.Pool = sfxPool;
        return sfxSpeaker;
    }

    private void OnSfxSpeakerGot(SfxSpeaker sfxSpeaker)
    {
        sfxSpeaker.gameObject.SetActive(true);
    }

    private void OnSfxSpeakerReleased(SfxSpeaker sfxSpeaker)
    {
        sfxSpeaker.gameObject.SetActive(false);
    }

    private void OnSfxSpeakerDestroyed(SfxSpeaker sfxSpeaker)
    {
        Destroy(sfxSpeaker);
    }

    #endregion
}
