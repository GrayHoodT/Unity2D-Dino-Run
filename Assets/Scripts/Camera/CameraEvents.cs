using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvents : EventArgs
{
    public event Action<float> Move;
    public void NotifyMove(float x) => Move?.Invoke(x);

    public event Action Punch;
    public void NotifyPunch() => Punch?.Invoke();
}
