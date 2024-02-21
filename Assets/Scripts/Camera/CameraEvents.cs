using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvents : EventArgs
{
    public event Action Moved;
    public void NotifyMoved() => Moved?.Invoke();

    public event Action Punched;
    public void NotifyPunched() => Punched?.Invoke();
}
