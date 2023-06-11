using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class MatchAndDestroy : MonoBehaviour
{
    [SerializeField] private GenerateTiles gt;
    [SerializeField] private MovingTilesByMouse moveTiles;

    [SerializeField] private List<TileEnvironmentDeterminer> matchingTiles;
    [SerializeField] private FallingTiles ft;    
    
    private TileEnvironmentDeterminer ted1, ted2;
    private ResourceCounter rc;    

    public TileEnvironmentDeterminer Ted1
    {
        get { return ted1; }
        set { ted1 = value; }
    }

    public TileEnvironmentDeterminer Ted2
    {
        get { return ted2; }
        set { ted2 = value; }
    }    

    void Start()
    {
        gt = GetComponent<GenerateTiles>();     
        ft = GetComponent<FallingTiles>();
        rc = GetComponent<ResourceCounter>();
    } 

    public void FindAndDestroyMatch()
    {      
        GetAllMatchOnField();
        ft.GetMatchTiles(matchingTiles);
        StartCoroutine(DelayToDeleteMatchTiles());        
    }    
   
    public void GetAllMatchOnField()
    {       
        matchingTiles.Clear();
        List<TileEnvironmentDeterminer> tempMatchTiles = new List<TileEnvironmentDeterminer>();        
        
        for (int i = 0; i < gt.TilesOnField.Count; i++)
        {
            GetMatchFromTile(tempMatchTiles, gt.TilesOnField[i]);
        }        

        matchingTiles = tempMatchTiles.Distinct().ToList();

        //Debug.Log("Нашли совпадения!" + matchingTiles.Count);
    }

    public void GetMatchFromTile(List<TileEnvironmentDeterminer> matchTiles, TileEnvironmentDeterminer checkTile)
    {
        if (!checkTile.IsMoving)
        {
            CheckTiles(matchTiles, checkTile.MatchTilesUp, checkTile.MatchTilesDown, checkTile);
            CheckTiles(matchTiles, checkTile.MatchTilesRight, checkTile.MatchTilesLeft, checkTile);
            CheckTiles(matchTiles, checkTile.MatchTilesUp, checkTile.MatchTilesUp?.MatchTilesUp, checkTile);
            CheckTiles(matchTiles, checkTile.MatchTilesDown, checkTile.MatchTilesDown?.MatchTilesDown, checkTile);
            CheckTiles(matchTiles, checkTile.MatchTilesRight, checkTile.MatchTilesRight?.MatchTilesRight, checkTile);
            CheckTiles(matchTiles, checkTile.MatchTilesLeft, checkTile.MatchTilesLeft?.MatchTilesLeft, checkTile);           
        }
    }

     


    private void CheckTiles(List<TileEnvironmentDeterminer> matchTiles, TileEnvironmentDeterminer checkTileOne,
        TileEnvironmentDeterminer checkTileTwo, TileEnvironmentDeterminer center)
    {
        if (checkTileOne != null && checkTileTwo != null)
        {
            if (!checkTileOne.IsMoving && !checkTileTwo.IsMoving)
                FindMatch(checkTileOne.Tag, checkTileTwo.Tag, center, matchTiles);
        }
    }


    private void FindMatch(string tag2, string tag3, TileEnvironmentDeterminer ted, List<TileEnvironmentDeterminer> listMatch)
    {
        string tagCenter = ted.Tag;
        if ((tagCenter == tag2) && (tagCenter == tag3))
        {
            listMatch.Add(ted);
            moveTiles.CanBeMoved = false;
        }
    }

    IEnumerator DelayToDeleteMatchTiles()
    {
        gt.DeterminationAllAdjacentTiles();
        ft.WorkWithMovingTiles();
        yield return new WaitForSeconds(0.1f);
        rc.ConsiderResources(matchingTiles);
        gt.ShufflingMatchTiles(ref matchingTiles);       
        
    }
}
