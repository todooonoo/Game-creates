using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWalk : MonoBehaviour
{
    public bool walkHard;

    // Start is called before the first frame update
    void Start()
    {
        Player3D.Instance.walkHard = walkHard;   
    }
}
