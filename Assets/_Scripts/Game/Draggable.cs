using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {

    [SerializeField] protected bool sourceIsParent;
    protected Transform originalParent, source;

    private void Awake()
    {
        source = sourceIsParent ? transform.parent : transform;
        originalParent = source.parent;
    }

    public virtual void OnDrag(Transform parent)
    {
        source.SetParent(parent);
    }

    public virtual void OnRelease(Transform parent)
    {
        source.SetParent(originalParent);
    }
}
