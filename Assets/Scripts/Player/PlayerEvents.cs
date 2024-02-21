using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : EventArgs
{
    public event Action Jumped;
    public void NotifyJumped() => Jumped?.Invoke();

    public event Action Landed;
    public void NotifyLanded() => Landed?.Invoke();

    public event Action Hit;
    public void NotifyHit() => Hit?.Invoke();
}
