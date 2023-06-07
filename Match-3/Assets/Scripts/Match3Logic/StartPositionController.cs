using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionController : MonoBehaviour
{
    [SerializeField] GenerateTiles gt;    
    [SerializeField] private TileEnvironmentDeterminer ted;
    [SerializeField] private TileEnvironmentDeterminer generatedTile;    

    private void Update()
    {
        if (ted != null && !ted.gameObject.activeSelf)
        {            
            GenerateTileOnPosition();           
        }

        if (ted != null && ted.gameObject.activeSelf && ted.gameObject.transform.position.x - gameObject.transform.position.x >= 0.9f &&
            ted.gameObject.transform.position.x - gameObject.transform.position.x <= 0.9f)
        {
            GenerateTileOnPosition();
        }
    }

    private void GenerateTileOnPosition()
    {
        ted = null;
        generatedTile = gt.GenerateRandomTile(transform.position);
        generatedTile.FindAllNeighboringTiles();
        StartCoroutine(generatedTile.DelayToCheckNeedMove());
        generatedTile = null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        ted = col.gameObject.GetComponent<TileEnvironmentDeterminer>();       
    }

    private void OnTriggerExit2D(Collider2D col)
    {        
        if ((transform.position.y - col.gameObject.transform.position.y >= 0.9f ) && col.gameObject.GetComponent<TileEnvironmentDeterminer>().IsMoving)
        {
            GenerateTileOnPosition();
        }
    }
}
