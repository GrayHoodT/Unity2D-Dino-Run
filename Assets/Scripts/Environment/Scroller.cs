using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public int childCount;
    public float speedRate;

    private void Start()
    {
        childCount = transform.childCount;
    }

    private void Update()
    {
        transform.Translate(speedRate * Time.deltaTime * -1f, 0, 0);
    }
}
