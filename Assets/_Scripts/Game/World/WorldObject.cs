using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WorldClonePair
{
    public WorldType worldType;
    public WorldObjectClone prefab;
    public WorldObjectClone Clone { get; set; }
}

public class WorldObject : MonoBehaviour {

    [SerializeField] private WorldClonePair[] pairs;

	public virtual void InitClone()
    {
        for(int i = 0; i < pairs.Length; i++)
        {
            var pair = pairs[i];
            var world = WorldManager.Instance.GetWorld(pair.worldType);
            var clone = pair.prefab.Spawn(world.transform);
            clone.transform.position = world.GetCurrentWorldPos(transform.position);
            clone.origin = this;
            pair.Clone = clone;
        }
    }
}
