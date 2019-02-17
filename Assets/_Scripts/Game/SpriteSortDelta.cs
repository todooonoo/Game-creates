using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortDelta : MonoBehaviour
{
    public int delta;

    // Start is called before the first frame update
    void Start()
    {
        var sprites = GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sortingOrder += delta;
        }
    }
}
