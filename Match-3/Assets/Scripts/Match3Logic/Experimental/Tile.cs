using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TilesObject _tilesObject;
    [SerializeField] private SpriteRenderer _renderer;

    private string typeOfTiles;
    public string TypeOfTiles { get => typeOfTiles; }    

    private bool isSelected;
    public bool IsSelected  => isSelected; 


    public void GetRandomTipeOfTile()
    {
        int r = Random.Range(0, _tilesObject.TypeOfTiles.Count);

        typeOfTiles = _tilesObject.IdTags[r];
        _renderer.sprite = _tilesObject.TypeOfTiles[typeOfTiles];
    }

    void OnMouseDown()
    {
        if (!isSelected) isSelected = true;
    }

    private void OnMouseUp()
    {        
        if (isSelected) isSelected = false;
    }



}
