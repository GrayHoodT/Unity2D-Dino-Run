using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvents : EventArgs
{
    public event Action<Enums.SFXClipType> PlaySFX;
    public void NotifyPlaySFX(Enums.SFXClipType clipType) => PlaySFX?.Invoke(clipType);
}
