using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvents : EventArgs
{
    public event Action<Enums.SFXClipType> PlaySFX;
    public void NotifyPlaySFX(Enums.SFXClipType clipType) => PlaySFX?.Invoke(clipType);

    public event Action PlayBGM;
    public void NotifyPlayBGM() => PlayBGM?.Invoke();

    public event Action StopBGM;
    public void NotifyStopBGM() => StopBGM?.Invoke();
}
