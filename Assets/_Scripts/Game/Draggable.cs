using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {

    protected Transform originalParent;

    private void Awake()
    {
        originalParent = transform.parent;
    }

    public virtual void OnDrag(Transform parent)
    {
        transform.SetParent(parent);
    }

    public virtual void OnRelease(Transform parent)
    {
        transform.SetParent(originalParent);
    }
}
