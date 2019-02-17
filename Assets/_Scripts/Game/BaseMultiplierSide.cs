﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMultiplierSide : MonoBehaviour
{
    public GameObject baseObject;
    public int count;
    public float multiplier = 1;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = -count / 2; i <= count / 2; i++)
        {
            var obj = Instantiate(baseObject, transform);
            obj.transform.localPosition = new Vector3(i * multiplier, 0);
        }
        baseObject.SetActive(false);
    }
}
