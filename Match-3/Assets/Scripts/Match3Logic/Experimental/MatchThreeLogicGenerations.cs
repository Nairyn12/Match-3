using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchThreeLogicGenerations : MonoBehaviour
{
    [SerializeField] private Tile _tile;

    [SerializeField] List<Tile> _tiles = new (300);
    [SerializeField] List<Tile> _lineTiles;
    [SerializeField] List<Tile> _columnTiles;
    [SerializeField] List<List<Tile>> _tilesOnField = new();
    [SerializeField] private List<Tile> _matchTiles = new List<Tile>();
    
    private int _width = 10;
    private int _height = 10;

    private void Start()
    {
        TilesPreparation();
        PrepareField();
    }

    private void TilesPreparation()
    {
        for (int i = 0; i < 300; i++)
        {
            _tiles.Add(Instantiate(_tile));
        }

        for (int i = 0; i < 300; i++)
        {
            _tiles[i].GetRandomTipeOfTile();
        }

        for (int i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].name += i;
            _tiles[i].gameObject.SetActive(false);
        }
    }


    private void PrepareField()
    {
        for (int x = 0; x < _width; x++)
        {
            List<Tile> column = new List<Tile>(_height);
            _tilesOnField.Add(column);
           
            for (int y = 0; y < _height; y++)
            {
                column.Add(GetRandomTile(x, y));
                _tiles.Remove(column.Last());                
                column.Last(). transform.position = new Vector3(x, y, 0.0f);
                column.Last().gameObject.SetActive(true);
            }
        }

        FindMatchOnStart();
    }

    private Tile GetRandomTile(int x, int y)
    {
        int r = UnityEngine.Random.Range(0, _tiles.Count);

        return _tiles[r];
    }

    private void FindMatchOnStart()
    {
        bool isMatching = false;

        for (int x = 0; x < _width; x++)
        {
            if (x != 0 && x != 9)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x + 1][y].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x - 1][y].TypeOfTiles)
                    {
                        isMatching = true;
                        _tilesOnField[x][y].GetRandomTipeOfTile();
                    }
                }
            }
        }

        for (int x = 0; x < _width; x++)
        { 
            for (int y = 0; y < _height; y++)
            {
                if (y != 0 && y != 9)
                {
                    if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y + 1].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y - 1].TypeOfTiles)
                    {
                        isMatching = true;
                        _tilesOnField[x][y].GetRandomTipeOfTile();
                    }
                }
            }            
        }

        if (isMatching) FindMatchOnStart();
    }

    public Tile SelectedTile()
    {
       Tile tile = null;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_tilesOnField[x][y].IsSelected)
                {
                    tile = _tilesOnField[x][y];
                }
            }
        }

        return tile;
    }

    public Tile SearchSecondTile(Tile original, string direction)
    {
        Tile secondTile = null;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_tilesOnField[x][y] == original)
                {
                    if (x != 9 && direction == "right")
                    {
                        secondTile = _tilesOnField[x + 1][y];
                        //Debug.Log("x: " + x + " right");                       
                    }
                    else if (x != 0 && direction == "left")
                    {
                        secondTile = _tilesOnField[x - 1][y];
                        //Debug.Log("x: " + x + " left");
                    }
                    else if (y != 9 && direction == "up")
                    {
                        secondTile = _tilesOnField[x][y + 1];
                        //Debug.Log("y: " + y + " up");
                    }
                    else if (y != 0 && direction == "down")
                    {
                        secondTile = _tilesOnField[x][y - 1];
                        //Debug.Log("y: " + y + " down");
                    }
                    else secondTile = null;
                }
            }
        }      

        return secondTile;
    }

    public bool FindMatchProbability(Tile tile1, Tile tile2)
    {
        bool isMatching = false;

        ChangeTileOnCollection(ref tile1, ref tile2);

        for (int x = 0; x < _width; x++)
        {
            if (x != 0 && x != 9)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x + 1][y].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x - 1][y].TypeOfTiles)
                    {
                        isMatching = true;                        
                    }
                }
            }
        }

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (y != 0 && y != 9)
                {
                    if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y + 1].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y - 1].TypeOfTiles)
                    {
                        isMatching = true;                       
                    }
                }
            }
        }

        if (!isMatching)
        {
            ChangeTileOnCollection(ref tile1, ref tile2);
        }

        return isMatching;
    }  
    
    public void ChangeTileOnCollection(ref Tile tile1, ref Tile tile2)
    {
        Debug.Log(tile1.gameObject.name + " " + tile2.gameObject.name);
        Tile temp = null;
        int change1 = 0;
        int change2 = 0;
        int change3 = 0;
        int change4 = 0;        

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if(tile1 == _tilesOnField[x][y])
                {
                    change1 = x;
                    change2 = y;
                }

                if (tile2 == _tilesOnField[x][y])
                {
                    change3 = x;
                    change4 = y;
                }
            }
        }

        //Debug.Log("В коллекции: " + _tilesOnField[change1][change2].gameObject.name + " " + _tilesOnField[change3][change4].gameObject.name);
        temp = _tilesOnField[change1][change2];
        _tilesOnField[change1][change2] = _tilesOnField[change3][change4];
        _tilesOnField[change3][change4] = temp;
        //Debug.Log("В коллекции: " + _tilesOnField[change1][change2].gameObject.name + " " + _tilesOnField[change3][change4].gameObject.name);

        temp = tile1;
        tile1 = tile2;
        tile2 = temp;

        //Debug.Log(tile1.gameObject.name + " " + tile2.gameObject.name);
    }

    public void FindMatch()
    {
        for (int x = 0; x < _width; x++)
        {
            if (x != 9 && x != 0)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x + 1][y].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x - 1][y].TypeOfTiles)
                    {
                        _matchTiles.Add(_tilesOnField[x][y]);                        
                        _matchTiles.Add(_tilesOnField[x+1][y]);
                        _matchTiles.Add(_tilesOnField[x-1][y]);
                        Debug.Log("СОВПАДЕНИЕ!!!!");
                    }
                }
            }
        }

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (y != 0 && y != 9)
                {
                    if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y + 1].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y - 1].TypeOfTiles)
                    {
                        _matchTiles.Add(_tilesOnField[x][y]);
                        _matchTiles.Add(_tilesOnField[x][y+1]);
                        _matchTiles.Add(_tilesOnField[x][y-1]);
                        Debug.Log("СОВПАДЕНИЕ!!!!");
                    }
                }
            }
        }

        Debug.Log("Количество совпадений: " + _matchTiles.Count);
    }

    private void RemoveDuplicateTile()
    {
        List<Tile> temp = new();

        for (int i = 0; i < _matchTiles.Count; i++)
        {
            for (int j = 0; j < _matchTiles.Count; j++)
            {
                if (_matchTiles[i].gameObject.name == _matchTiles[j].gameObject.name && i < j)
                {                    
                    temp.Add(_matchTiles[j]);
                }
            }
        }

        for (int i = 0; i < temp.Count; i++)
        {
            _matchTiles.Remove(temp[i]);
        }

        Debug.Log("Количество совпадений: " + _matchTiles.Count);

        for (int i = 0; i < _matchTiles.Count; i++)
        {
            Debug.Log("Совпадения: " + _matchTiles[i]);
        }
    }

    public void DestroyMatchTile()
    {
        RemoveDuplicateTile();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int i = 0; i < _matchTiles.Count; i++)
                {
                    if (_tilesOnField[x][y] == _matchTiles[i])
                    {
                        _tilesOnField[x][y] = null;
                    }
                }
            }        
        }

        ReturnToPoolAndShutdownTile();
        ClearPool(_matchTiles);
    }

    private void ReturnToPoolAndShutdownTile()
    {
        for (int i = 0; i < _matchTiles.Count; i++)
        {
            _tiles.Add(_matchTiles[i]);
            _matchTiles[i].gameObject.SetActive(false);
        }        
    }

    private void ClearPool(List<Tile> list)
    {
        list.Clear();
    }
}





