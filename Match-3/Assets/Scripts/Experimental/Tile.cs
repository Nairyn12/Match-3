using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Action refindMatch;


    [SerializeField] private TilesObject _tilesObject;
    [SerializeField] private SpriteRenderer _renderer;
    //[SerializeField] private MatchThreeLogicGenerations _logic;

    private string tempType;

    [SerializeField] private string typeOfTiles;
    public string TypeOfTiles { get => typeOfTiles; }

    [SerializeField] private bool isSelected;
    public bool IsSelected { get => isSelected; set => isSelected = value; }

    [SerializeField] private bool isMoving;
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    [SerializeField] private Vector2 _targetPos;
    public Vector2 TargetPos { get => _targetPos; set => _targetPos = value; }
    
    [SerializeField] private bool isRefindMatch;
    public bool IsRefindMatch { get => isRefindMatch; set => isRefindMatch = value; }


    private void Update()
    {
        if (isMoving == true)
        {
            if (typeOfTiles != gameObject.name)
            {
                tempType = typeOfTiles;
                typeOfTiles = gameObject.name;
            }            
            Falling();            
        }

        if (gameObject.transform.position.y == _targetPos.y && isMoving)
        {
            isMoving = false;
            _targetPos = this.transform.position;
            typeOfTiles = tempType;
            if (isRefindMatch) StartCoroutine(RefindMatchDelay());
        }
    }

    public void GetRandomTipeOfTile()
    {
        int r = UnityEngine.Random.Range(0, _tilesObject.TypeOfTiles.Count-1);

        typeOfTiles = _tilesObject.IdTags[r];
        _renderer.sprite = _tilesObject.TypeOfTiles[typeOfTiles];
    }

    public void Falling()
    {
        if (isMoving == true)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, _targetPos, 4f * Time.deltaTime);            
        }
    }

    void OnMouseDown()
    {
        if (!isSelected) isSelected = true;
    }

    private void OnMouseUp()
    {        
        isSelected = false;
    }


    IEnumerator RefindMatchDelay()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log("œ≈–≈œ–Œ¬≈– ¿ ¬€«¬¿Õ¿ “¿…ÀŒÃ " + gameObject.name + " " + gameObject.transform.position.x + " " + gameObject.transform.position.y);
        refindMatch?.Invoke();
        isRefindMatch = false;
    }



}
