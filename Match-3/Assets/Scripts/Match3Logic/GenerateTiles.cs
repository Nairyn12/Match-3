using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerateTiles : MonoBehaviour
{
    [SerializeField] private GameObject prefabGold, prefabCoal, prefabMetal, prefabWood, prefabFood, prefabKnow;
    [SerializeField] private List<GameObject> poolTilesGO;
    [SerializeField] private List<TileEnvironmentDeterminer> poolTilesTED;
    [SerializeField] private List<Vector2> positionTiles;
    [SerializeField] private List<Vector2> positionOnChange;
    [SerializeField] private List<TileEnvironmentDeterminer> allTiles;
    [SerializeField] private List<TileEnvironmentDeterminer> matchTiles;
    [SerializeField] private StartPositionController[] spc;
    [SerializeField] private List<TileEnvironmentDeterminer> tilesOnField;

    [SerializeField] private SoundsController _sc;


    public List<TileEnvironmentDeterminer> PoolTilesTED
    {
        get { return poolTilesTED; }
        set { poolTilesTED = value; }
    }  
    
    public List<TileEnvironmentDeterminer> TilesOnField
    {
        get { return tilesOnField; }
        set { tilesOnField = value; }
    }

    void Awake()
    {        
        TilesPreparation();
        PointPreparation();
        FieldPreparation(positionTiles, poolTilesTED);
    }   

    private void TilesPreparation ()
    {
        for (int i = 0; i < 50; i++)
        {
            poolTilesGO.Add(Instantiate(prefabGold));
            poolTilesGO.Add(Instantiate(prefabCoal));
            poolTilesGO.Add(Instantiate(prefabMetal));
            poolTilesGO.Add(Instantiate(prefabWood));
            poolTilesGO.Add(Instantiate(prefabFood));
            poolTilesGO.Add(Instantiate(prefabKnow));
        }

        for (int i = 0; i < poolTilesGO.Count; i++)
        {
            poolTilesGO[i].name += i;
            poolTilesTED.Add(poolTilesGO[i].GetComponent<TileEnvironmentDeterminer>());
            poolTilesGO[i].SetActive(false);
        }
    }

    private void PointPreparation ()
    {
        float x = -4.5f;
        float y = 4.5f;
        int iterationY = 0;
        int iterationX = 0;

        for (int i = 0; i < 100; i++)
        {
            if (iterationX < 10)
            {               
                positionTiles.Add(new Vector2(x + iterationX, y - iterationY));
                iterationX++;
            }
            else
            {
                iterationY++;
                iterationX = 0;
                positionTiles.Add(new Vector2(x + iterationX, y - iterationY));
                iterationX++;
            }            
        }
    }

    private void FieldPreparation(List<Vector2> tempPos, List<TileEnvironmentDeterminer> poolTiles)
    {        
        int r;
        
        for (int i = 0; i < tempPos.Count; i++)
        {
            r = UnityEngine.Random.Range(0, poolTiles.Count);            
            poolTiles[r].transform.position = tempPos[i];
            poolTiles[r].gameObject.SetActive(true);
            tilesOnField.Add(poolTiles[r]);            
            poolTiles.RemoveAt(r);            
        }

        positionOnChange.Clear();
        bool check = MatchCheck();

        if (check)
        {            
            ShufflingMatchTiles(ref matchTiles, true);            
            FieldPreparation(positionOnChange, poolTiles);            
        }        
    }

    private bool MatchCheck ()
    {
        int checkCount = 0;

        for (int i = 0; i < tilesOnField.Count; i++)
        {
            if (tilesOnField[i].CheckMatchingOnStart() > 0)
            {
                //Debug.Log("Совпадения найдены: " + ted.gameObject.name);
                matchTiles.Add(tilesOnField[i]);
                checkCount++;
            }           
        }       

        if (checkCount > 0)
        {
            return true;
        }
        else
        {
            DeterminationAllAdjacentTiles();
            return false;
        }
    }

    public void DeterminationAllAdjacentTiles()
    {        
        for (int i = 0; i < tilesOnField.Count; i++)
        {
            tilesOnField[i].FindAllNeighboringTiles();
        }
    }

    public void ShufflingMatchTiles(ref List<TileEnvironmentDeterminer> deleteTile, bool isStartGame)
    {        
        for (int i = 0; i < deleteTile.Count; i++)
        {            
            positionOnChange.Add(deleteTile[i].transform.position);
            poolTilesTED.Add(deleteTile[i]);
            tilesOnField.Remove(deleteTile[i]);            
            deleteTile[i].ZeroingNeighboringTiles();
            deleteTile[i].IsMoving = false;
            deleteTile[i].gameObject.SetActive(false);
            _sc.PlaySoundDestroyTiles();
        }

        deleteTile.Clear();
    }

    public TileEnvironmentDeterminer GenerateRandomTile(Vector3 pos)
    {  
        int r = UnityEngine.Random.Range(0, poolTilesTED.Count);          
        TilesOn(poolTilesTED[r].gameObject, pos);
        TileEnvironmentDeterminer tempTED = poolTilesTED[r];
        GetNeighboringTiles();        
        tilesOnField.Add(tempTED);        
        poolTilesTED.Remove(tempTED);        
        
        if (tempTED.MatchTilesDown == null) tempTED.IsMoving = true;
        else tempTED.IsMoving = false;        
       
        return tempTED;
    }    

    private void TilesOn (GameObject temp, Vector3 pos)
    {        
        temp.SetActive(true);
        temp.transform.position = pos;
    }       

    public void GetNeighboringTiles()
    {        
        for (int i = 0; i < tilesOnField.Count; i++)
        {
            tilesOnField[i].FindAllNeighboringTiles();        
        }
    }
}
