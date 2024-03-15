using UnityEngine;

public class Scroller : MonoBehaviour
{
    public int childCount;
    public float speedRate;
    public bool isPlay;

    private void Start()
    {
        childCount = transform.childCount;
    }

    private void Update()
    {
        if (isPlay == false)
            return;

        transform.Translate(speedRate * Time.deltaTime * -1f, 0, 0);
    }
}
