using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Rename to interactable
public class Draggable : MonoBehaviour {

    [SerializeField] protected bool sourceIsParent;
    protected Transform originalParent, source;

    public bool lookAtTarget;
    public InteractType interactType;
    [SerializeField] protected Transform interactTransform;

    protected Collider _collider;

    public Vector3 InteractUIPos
    {
        get { return interactTransform 
                ? interactTransform.position 
                : _collider.bounds.center + Vector3.up * _collider.bounds.extents.y; }
    }

    private void Awake()
    {
        source = sourceIsParent ? transform.parent : transform;
        originalParent = source.parent;
        _collider = GetComponent<Collider>();
    }

    public virtual void OnTrigger()
    {
        GameManager.Instance.SetInteractIcon(interactType);
    }

    public virtual bool OnDrag(Transform parent)
    {
        // GameManager.Instance.SetInteractIcon(InteractType.Move);
        source.SetParent(parent);
        return true;
    }

    public virtual void OnRelease(Transform parent)
    {
        // GameManager.Instance.SetInteractIcon(interactType);
        source.SetParent(originalParent);
    }
}
