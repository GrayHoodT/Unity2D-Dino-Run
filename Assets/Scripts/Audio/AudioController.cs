using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AudioController : MonoBehaviour
{
    private IObjectPool<SfxSpeaker> sfxPool;
    private SfxSpeaker sfxSpeakerPrefab;

    [SerializeField]
    private List<AudioClip> sfxClipList;

    //TODO: �� �Ŀ� �ɼ� ���� ��� �߰��� �� AudioMixer ���� ��� �߰��ϱ�.
    private AudioSource bgmAudioSource;

    private void Awake()
    {
        sfxSpeakerPrefab = Resources.Load<GameObject>(Defines.SFX_SPEAKER_PREFAB_PATH).GetComponent<SfxSpeaker>();
        bgmAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        sfxPool = new ObjectPool<SfxSpeaker>(CreateSfxSpeaker, OnSfxSpeakerGot, OnSfxSpeakerReleased, OnSfxSpeakerDestroyed);
    }

    public bool Initialize(out AudioEvents audioEvent)
    {
        audioEvent = new AudioEvents();
        audioEvent.PlaySFX += PlaySFX;
        audioEvent.PlayBGM += PlayBGM;
        audioEvent.StopBGM += StopBGM;

        return true;
    }

    private void PlaySFX(Enums.SFXClipType clipType)
    {
        var sfxClip = sfxClipList[(int) clipType];
        var sfxSpeaker = sfxPool.Get();
        sfxSpeaker.Play(sfxClip);
    }

    private void PlayBGM()
    {
        bgmAudioSource.Play();
    }

    private void StopBGM()
    {
        bgmAudioSource.Stop();
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
