using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDragging : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private MatchThreeLogicGenerations _gen;
    [SerializeField] private CounterMoves _cm;
    [SerializeField] private bool isCoroutine;    
    [SerializeField] private float speedDragging;

    private Tile _tile1, _tile2;
    private Vector2 startPosMouse, endPosMouse;   
    private Vector2 targetPoint1, targetPoint2;

    private void Start()
    {
        _gen = GetComponent<MatchThreeLogicGenerations>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _gen.CanBeMovedByMouse && _cm.CountOfMoves > 0)
        {
            if (_gen.SelectedTile() != null)
            {                
                _tile1 = _gen.SelectedTile();
                startPosMouse = _cam.ScreenToWorldPoint(Input.mousePosition);
                targetPoint1 = _tile1.gameObject.transform.position;
                StartCoroutine(TrackTheMovement());
            }
        }
    }

    private void FixedUpdate()
    {
        if (_tile1 != null && _tile2 != null)
        {
            if (!isCoroutine)
            {
                StartCoroutine(MovingTileTime());
                isCoroutine = true;
            }

            _tile1.gameObject.transform.position = Vector2.MoveTowards(_tile1.gameObject.transform.position, targetPoint2, speedDragging * Time.fixedDeltaTime);
            _tile2.gameObject.transform.position = Vector2.MoveTowards(_tile2.gameObject.transform.position, targetPoint1, speedDragging * Time.fixedDeltaTime);
        }
    }

    private void PermutationOfTiles()
    {
        Vector2 directionOfMouse = endPosMouse - startPosMouse;

        if (Mathf.Abs(directionOfMouse.x) > Mathf.Abs(directionOfMouse.y))
        {

            
            if (directionOfMouse.x > 0)
            {                
                _tile2 = _gen.SearchSecondTile(_tile1, "right");
            }
            else
            {                
                _tile2 = _gen.SearchSecondTile(_tile1, "left");
            }            
        }
        else if (Mathf.Abs(directionOfMouse.x) < Mathf.Abs(directionOfMouse.y))
        {
            
            if (directionOfMouse.y > 0)
            {                
                _tile2 = _gen.SearchSecondTile(_tile1, "up");
            }
            else
            {                
                _tile2 = _gen.SearchSecondTile(_tile1, "down");
            }            
        }
        else
        {
            ClearingTheValues();
        }

        if (_tile2 != null)
        {
            targetPoint2 = _tile2.gameObject.transform.position;
        }        
    }

    private void ClearingTheValues()
    {
        isCoroutine = false;       
        _tile1 = null;
        _tile2 = null;        
        targetPoint1 = new Vector2(0, 0);
        targetPoint2 = new Vector2(0, 0);
    }

    IEnumerator TrackTheMovement()
    {
        yield return new WaitForSeconds(0.15f);
        
        if (Input.GetKey(KeyCode.Mouse0))
        {            
            endPosMouse = _cam.ScreenToWorldPoint(Input.mousePosition);
            PermutationOfTiles();            
        }
        else
        {
            ClearingTheValues();            
        }
    }

    IEnumerator MovingTileTime()
    {
        yield return new WaitForSeconds(0.2f);        

        if (_gen.FindMatchProbability(_tile1, _tile2))
        {            
            _gen.FindMatch();           
            ClearingTheValues();
            _cm.CountOfMoves--;
        }
        else
        {
            Vector2 tempPoint = targetPoint1;
            targetPoint1 = targetPoint2;
            targetPoint2 = tempPoint;
            yield return new WaitForSeconds(0.2f);            
            ClearingTheValues();
        }
    }


}
