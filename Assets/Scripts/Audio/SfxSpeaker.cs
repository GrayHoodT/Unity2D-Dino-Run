using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SfxSpeaker : MonoBehaviour
{
    public IObjectPool<SfxSpeaker> Pool { get; set; }

    //TODO: AudioSource 받아서 기능 구현하기.
}
