using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMultiplierVertical : MonoBehaviour
{
    public GameObject baseObject;
    public int count, multiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(baseObject, transform);
            obj.transform.localPosition = baseObject.transform.localPosition + new Vector3(0, i * multiplier);
        }
        baseObject.SetActive(false);
    }
}
