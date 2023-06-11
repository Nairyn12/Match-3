using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileMap : MonoBehaviour
{
    [SerializeField] private TileEnvironmentDeterminer[] prefabs;
    [SerializeField] private int Width = 10;
    [SerializeField] private int Height = 10;
    [SerializeField] private float stepX = 1.0f;
    [SerializeField] private float stepY = -1.0f;
    [SerializeField] private float startX = -4.5f;
    [SerializeField] private float startY = 14.5f;
    [SerializeField] private float speedFall = 10f;

    private float _moveProgress;

    private List<List<TileEnvironmentDeterminer>> _map = new();
    private List<MovingData> _movingsData = new();

    private void Awake()
    {
        PrepareMap();
    }

    private void Update()
    {
        if (_movingsData.Count>0)
        {
            Move();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyRandomTile();
           
        }
    }

    private void Move()
    {
        _moveProgress += Time.deltaTime * speedFall;

        if (_moveProgress > 1)
        {
            _moveProgress = 1;
        }

        foreach (var item in _movingsData)
        {
            int x = item.x;
            int y = item.y;
            var tile = _map[x][y];
            tile.transform.position = new Vector3(x * stepX + startX, (y + _moveProgress) * stepY + startY, 0f);
        }
        
        if (_moveProgress>= 1)
        {
            foreach (var item in _movingsData)
            {
                int x = item.x;
                int y = item.y;
                _map[x][y] = null;
            }
            
            foreach (var item in _movingsData)
            {
                item.y++;
                int x = item.x;
                int y = item.y;
                _map[x][y] = item.tile;
            }

            for (int i = _movingsData.Count-1; i >= 0; i--)
            {
                var item = _movingsData[i];
                int x = item.x;
                int y = item.y;
                if (y + 1 >= Height || HasNotEmptyTilesBottom(x, y))
                {
                    _movingsData.RemoveAt(i);
                }
            }

            _moveProgress = 0;
        }
    }

    private bool HasNotEmptyTilesBottom(int x, int y)
    {
        for (int i = y+1; i < Height; i++)
        {            
            if (_map[x][i] == null)
                return false;
        }
        return true;
    }

    private void DestroyRandomTile()
    {
        int x = Random.Range(0,Width);
        int y = Random.Range(0, Height-3);

        DestroyTile(x,y);
        DestroyTile(x, y + 1);
        DestroyTile(x, y + 2);      

        for (int i = 0; i < y; i++)
        {
            var tile = _map[x][i];
            var tempData = new MovingData(x, i, tile);            
            _movingsData.Add(tempData);

        }
    }

    private void DestroyTile(int x, int y)
    {
        TileEnvironmentDeterminer tile = _map[x][y];
        
        if (tile != null) 
            Destroy(tile.gameObject);        
       
        _map[x][y] = null;
        
    }

    public void PrepareMap()
    {
        for (int x = 0; x < Width; x++)
        {
            List<TileEnvironmentDeterminer> column = new List<TileEnvironmentDeterminer>(Height);
            _map.Add(column);
            for (int y = 0; y < Height; y++)
            {
                column.Add(GetRandomTile(x, y));
            }
        }
    }

    private TileEnvironmentDeterminer GetRandomTile(int x, int y)
    {       
        int index = UnityEngine.Random.Range(0, prefabs.Length);
        Vector3 position = new Vector3(x * stepX + startX, y * stepY + startY, 0f);
        var tile = Instantiate(prefabs[index], position, Quaternion.identity);
        tile.transform.SetParent(this.transform);
        return tile;
    }

    

   
}
