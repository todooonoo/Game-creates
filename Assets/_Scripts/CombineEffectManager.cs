using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineEffectManager : MonoBehaviour
{
    public static CombineEffectManager Instance { get; private set; }

    public GameObject effectObject;
    private List<GameObject> objects;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        objects = new List<GameObject>();
    }

    public void PlayEffect(Vector3 pos)
    {
        objects.Add(Instantiate(effectObject, pos, Quaternion.identity));
    }
}
