using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectChanger : MonoBehaviour
{
    public GameObject[] gobjs;

    public void ChangeGameObjectToRandom()
    {
        var random = Random.Range(0, gobjs.Length);

        for (var index = 0; index < gobjs.Length; index++)
            transform.GetChild(index).gameObject.SetActive(random == index);
    }
}
