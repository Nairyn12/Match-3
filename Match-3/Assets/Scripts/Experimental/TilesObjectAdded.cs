using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesObjectAdded : MonoBehaviour
{
    [SerializeField] private TilesObject to;
    private void Awake()
    {
        for (int i = 0; i < to.IdTags.Count; i++)
        {
            to.TypeOfTiles.Add(to.IdTags[i], to.Sprites[i]);
        }
    }
}
