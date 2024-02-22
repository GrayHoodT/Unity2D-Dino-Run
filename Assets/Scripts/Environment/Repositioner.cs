using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Repositioner : MonoBehaviour
{
    public UnityEvent Moved;

    private void LateUpdate()
    {
        if (transform.position.x > -10)
            return;

        transform.Translate(24, 0, 0, Space.Self);
        Moved?.Invoke();
    }
}
