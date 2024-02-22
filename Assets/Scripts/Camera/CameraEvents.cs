using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvents : EventArgs
{
    public event Action Move;
    public void NotifyMove() => Move?.Invoke();

    public event Action Punch;
    public void NotifyPunch() => Punch?.Invoke();
}
