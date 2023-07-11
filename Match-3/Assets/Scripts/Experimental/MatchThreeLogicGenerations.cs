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
    [SerializeField] private ResourceCounter _rc;
    [SerializeField] private SoundsController _sc;
    [SerializeField] private ParticleMatch _pm;
    [SerializeField] private CounterMoves _cm;

    private int _width = 10;
    private int _height = 10;
    private bool _isRefind;
    [SerializeField] private bool _isCheckMatchAfter;

    public List<List<Tile>> TilesOnField { get => _tilesOnField; set => _tilesOnField = value; }

    public bool CanBeMovedByMouse { get; set; }

    private void Start()
    {
        TilesPreparation();
        PrepareField();
        CanBeMovedByMouse = true;
    }

    private void Update()
    {
        if (_isCheckMatchAfter)
        {
            CheckMatchAfterMoveTile();
        }
    }

    private void TilesPreparation()
    {
        for (int i = 0; i < 300; i++)
        {
            _tiles.Add(Instantiate(_tile));
            _tiles[i].refindMatch += RefindMatchTiles;
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
                if (_tilesOnField[x][y] != null && _tilesOnField[x][y].IsSelected)
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
                    }
                    else if (x != 0 && direction == "left")
                    {
                        secondTile = _tilesOnField[x - 1][y];                       
                    }
                    else if (y != 9 && direction == "up")
                    {
                        secondTile = _tilesOnField[x][y + 1];                        
                    }
                    else if (y != 0 && direction == "down")
                    {
                        secondTile = _tilesOnField[x][y - 1];                        
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
                    if (_tilesOnField[x][y] != null && _tilesOnField[x + 1][y] != null &&
                        _tilesOnField[x - 1][y] != null)
                    {
                        if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x + 1][y].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x - 1][y].TypeOfTiles)
                        {
                            isMatching = true;
                        }
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
                    if (_tilesOnField[x][y] != null && _tilesOnField[x][y + 1] != null &&
                        _tilesOnField[x][y - 1] != null)
                    {
                        if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y + 1].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y - 1].TypeOfTiles)
                        {
                            isMatching = true;
                        }
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
       
        temp = _tilesOnField[change1][change2];
        _tilesOnField[change1][change2] = _tilesOnField[change3][change4];
        _tilesOnField[change3][change4] = temp;        

        temp = tile1;
        tile1 = tile2;
        tile2 = temp;
    }

    private void RefindMatchTiles()
    {
        if (!_isRefind)
        {
            _isRefind = true;
            Debug.Log("Check match!!!!!!!!");
            FindMatch();
        }       
    }

    public bool FindMatch()
    {
        CanBeMovedByMouse = false;
        bool isMatch = false;

        for (int x = 0; x < _width; x++)
        {
            if (x != 9 && x != 0)
            {
                for (int y = 0; y < _height; y++)
                {                    
                    if (_tilesOnField[x][y] != null && _tilesOnField[x + 1][y] != null &&
                        _tilesOnField[x - 1][y] != null)
                    {
                        if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x + 1][y].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x - 1][y].TypeOfTiles)
                        {
                            _matchTiles.Add(_tilesOnField[x][y]);
                            _matchTiles.Add(_tilesOnField[x + 1][y]);
                            _matchTiles.Add(_tilesOnField[x - 1][y]);

                            isMatch = true;
                            Debug.Log("+++++++++++++++++++++++++++++++++++++++++++++++Совпадения по горизонтали найдены");
                        }
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
                    if (_tilesOnField[x][y] != null && _tilesOnField[x][y + 1] != null &&
                        _tilesOnField[x][y - 1] != null)
                    {
                        if (_tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y + 1].TypeOfTiles &&
                        _tilesOnField[x][y].TypeOfTiles == _tilesOnField[x][y - 1].TypeOfTiles)
                        {
                            _matchTiles.Add(_tilesOnField[x][y]);
                            _matchTiles.Add(_tilesOnField[x][y + 1]);
                            _matchTiles.Add(_tilesOnField[x][y - 1]);

                            isMatch = true;
                            Debug.Log("+++++++++++++++++++++++++++++++++++++++++++++++Совпадения по вертикали найдены");
                        }
                    }
                }
            }
        }

        if (_matchTiles.Count > 0)
        {
            DestroyMatchTile();
            FindAndStartMoveTiles();            
        }
       
        _isRefind = false;
        return isMatch;
    }

    private void RemoveDuplicateTile()
    {
        List<Tile> temp = new();
        temp = _matchTiles;

        for (int i = 0; i < _matchTiles.Count; i++)
        {
            bool isMatch = false;
            for (int j = 0; j < temp.Count; j++)
            {
                if (_matchTiles[i].gameObject.name == temp[j].gameObject.name && isMatch)
                {                    
                    temp.Remove(_matchTiles[j]);
                }

                if (j < temp.Count && _matchTiles[i].gameObject.name == temp[j].gameObject.name && !isMatch)
                {
                    isMatch = true;
                }
            }
        }

        _matchTiles = temp;

        for (int i = 0; i < temp.Count; i++)
        {            
            _matchTiles[i].IsSelected = false;            
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
                    if (_tilesOnField[x][y] != null && _tilesOnField[x][y] == _matchTiles[i])
                    {
                        _tilesOnField[x][y] = null;
                    }
                }
            }        
        }

        _rc.ConsiderResources(_matchTiles);
        _sc.PlaySoundDestroyTiles();
        ReturnToPoolAndShutdownTile();
        ClearPool(_matchTiles);
        //if (_isRefind) _isRefind = false;
    }

    private void ReturnToPoolAndShutdownTile()
    {
        for (int i = 0; i < _matchTiles.Count; i++)
        {
            _tiles.Add(_matchTiles[i]);
            _matchTiles[i].gameObject.SetActive(false);
            _pm.PlayParticleSystem(_matchTiles[i]);
        }        
    }

    private void ClearPool(List<Tile> list)
    {
        list.Clear();
    }

    public void FindAndStartMoveTiles()
    {
        int fullTile = 0;
        for (int i = 0; i < _tilesOnField.Count; i++)
        {           
            int voidTile = 0;
            
            
            for (int j = 0; j < _tilesOnField[i].Count; j++)
            {                
                if (_tilesOnField[i][j] == null)
                {
                    voidTile++;                    
                }

                if (_tilesOnField[i][j] != null && voidTile > 0)
                {            
                   
                    _tilesOnField[i][j - voidTile] = _tilesOnField[i][j];
                    _tilesOnField[i][j] = null;
                    _tilesOnField[i][j - voidTile].TargetPos = new Vector2(i, j - voidTile);
                    _tilesOnField[i][j - voidTile].IsMoving = true;

                    if (fullTile == 0 && !_tilesOnField[i][j - voidTile].IsRefindMatch)
                    {
                        _tilesOnField[i][j - voidTile].IsRefindMatch = true;
                        Debug.Log("_tilesOnField[i][j - voidTile] " + _tilesOnField[i][j - voidTile].name + " isRefind: " + _isRefind);
                    }
                    fullTile++;                    
                }                
            }
            GenerateNewTilesInLines(new Vector2(i, 10f), voidTile);
        }       
    }

    private void GenerateNewTilesInLines(Vector2 position, int lenght)
    {       
        List<Tile> random = new();
        for (int i = 0; i < lenght; i++)
        {
            System.Random rnd = new System.Random();
            int randIndex = rnd.Next(_tiles.Count);
            random.Add(_tiles[randIndex]);
            _tiles.Remove(random[i]);
            random[i].gameObject.SetActive(true);
            random[i].gameObject.transform.position = new Vector2(position.x, position.y + i);
            random[i].IsMoving = true;
            random[i].TargetPos = new Vector2(position.x, random[i].transform.position.y - lenght);

            

            //Debug.Log(random[i].gameObject.name + " имеет направление к " + random[i].TargetPos + " при этом создан в позиции " + random[i].gameObject.transform.position +
            //" //итерация i = " + i + " //количество пустых тайлов в столбце: " + lenght);
        }
        
        for (int i = 0; i < _tilesOnField[(int)position.x].Count; i++)
        {
            for (int j = 0; j < random.Count; j++)
            {
                if (_tilesOnField[(int)position.x][i] == null)
                {
                    _tilesOnField[(int)position.x][i] = random[j];                   
                    random.Remove(random[j]);                   
                }
            }            
        }

        _isCheckMatchAfter = true;
    }

    private void CheckMatchAfterMoveTile()
    {
        bool isChecking = false;

        for (int x = 0; x < _tilesOnField.Count; x++)
        {
            for (int y = 0; y < _tilesOnField[x].Count; y++)
            {
                if (_tilesOnField[x][y].IsMoving || _tilesOnField[x][y].IsRefindMatch)
                {
                    isChecking = true;
                }
            }
        }

        if (!isChecking)
        {
            _isCheckMatchAfter = false;
            StartCoroutine(RefindMatchDelay());
        }
    }


    IEnumerator RefindMatchDelay()
    {
        yield return new WaitForSeconds(0.25f);        
        //FindMatch();
        if (!FindMatch())
        {
            CanBeMovedByMouse = true;
            if (_cm.CountOfMoves <= 44) _cm.OnFinishPanel();
        }
    }
}





