using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class FallingTiles : MonoBehaviour
{
    [SerializeField] private GenerateTiles gt;    
    [SerializeField] private List<StartPositionController> spc;
    [SerializeField] private List<TileEnvironmentDeterminer> deleteTiles = new List<TileEnvironmentDeterminer>(0);
    [SerializeField] private List<TileEnvironmentDeterminer> downMovingTiles = new List<TileEnvironmentDeterminer>(0); 
   
    public List<T> RemoveDuplicates<T>(List<T> list)
    {
        return new HashSet<T>(list).ToList();
    }

    void Start()
    {
        gt = GetComponent<GenerateTiles>();       
    }      

    public void WorkWithMovingTiles()
    {      
        FindDownMovingTiles();       
        MoveTiles();
        ClearList();
    }

    public void FindDownMovingTiles()
    { 
        for (int i = 0; i < deleteTiles.Count; i++)
        {
            if (deleteTiles[i].MatchTilesUp != null)
            {
                downMovingTiles.Add(deleteTiles[i].MatchTilesUp);                
            }
        }
    }   

    public void MoveTiles()
    {
        for (int i = 0; i < downMovingTiles.Count; i++)
        {
            EnableTileMovement(downMovingTiles[i]);
        }
    }

    public void EnableTileMovement (TileEnvironmentDeterminer ted)
    {
        StartCoroutine(ted.DelayToStartMove());

        if (ted.MatchTilesUp != null)
        {
            StartCoroutine(ted.DelayToStartMove());
            EnableTileMovement(ted.MatchTilesUp);           
        }
    }    

    public void GetMatchTiles(List<TileEnvironmentDeterminer> temp)
    {
        List<TileEnvironmentDeterminer> tempTiles = new List<TileEnvironmentDeterminer>(0);           

        for (int i = 0; i < temp.Count; i++)
        {
            deleteTiles.Add(temp[i]);
        }        

        for (int i = 0; i < deleteTiles.Count; i++)
        {
            for (int j = 0; j < deleteTiles.Count; j++)
            {
                if (deleteTiles[j] == deleteTiles[i].MatchTilesDown)
                {
                    tempTiles.Add(deleteTiles[j]);
                }
            }
        }

        for (int i = 0; i < tempTiles.Count; i++)
        {
            deleteTiles.Remove(tempTiles[i]);
        } 
    }

    private void ClearList()
    {
        if (deleteTiles.Count > 0) deleteTiles.Clear();
        if (downMovingTiles.Count > 0) downMovingTiles.Clear();
    }
}
