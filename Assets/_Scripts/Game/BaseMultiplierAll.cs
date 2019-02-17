using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMultiplierAll : MonoBehaviour
{
    public int width, height;
    public float multiplier;
    public GameObject baseObject;

    // Start is called before the first frame update
    void Start()
    {
        var scale = baseObject.transform.localScale;
        for (int i = 1; i < width / 2; i++)
        {
            float x = 0.5f + (i - 1) * multiplier;

            for(int j = 1; j < height / 2; j++)
            {
                float y = 0.5f + (j - 1) * multiplier;
                var t = Instantiate(baseObject, new Vector3(x, y), Quaternion.identity, baseObject.transform.parent);
                t.transform.localScale = scale;
                t = Instantiate(baseObject, new Vector3(-x, y), Quaternion.identity, baseObject.transform.parent);
                t.transform.localScale = scale;
                t = Instantiate(baseObject, new Vector3(-x, -y), Quaternion.identity, baseObject.transform.parent);
                t.transform.localScale = scale;
                t = Instantiate(baseObject, new Vector3(x, -y), Quaternion.identity, baseObject.transform.parent);
                t.transform.localScale = scale;
            }
        }
        baseObject.SetActive(false);
    }
}
