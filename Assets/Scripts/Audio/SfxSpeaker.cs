using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SfxSpeaker : MonoBehaviour
{
    public IObjectPool<SfxSpeaker> Pool { get; set; }

    //TODO: AudioSource �޾Ƽ� ��� �����ϱ�.
}
