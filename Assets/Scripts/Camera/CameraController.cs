using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool Initialize(out CameraEvents events)
    {
        events = new CameraEvents();
        events.Move += MoveCameraByGameStart;
        events.Punch += PunchCameraByPlayerHit;

        return true;
    }

    private void MoveCameraByGameStart(float x)
    {
        transform.DOMoveX(x, 1f).SetEase(Ease.InOutFlash);
    }

    private void PunchCameraByPlayerHit()
    {
        transform.DOPunchPosition(Vector3.one * 0.1f, 1, 20).SetEase(Ease.InOutFlash);
    }
}
