using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTilesByMouse : MonoBehaviour
{       
    [SerializeField] private Camera cam;     
    [SerializeField] private bool isTileSelected;    
    [SerializeField] private bool isCoroutine;
    [SerializeField] private TileEnvironmentDeterminer ted, ted2;
    [SerializeField] private MatchAndDestroy mad;
    [SerializeField] private int countOfMoving;

    public bool CanBeMoved { get; set; }

    private Vector2 startPosMouse;
    private Vector2 endPosMouse;     
    private Vector2 targetPoint1, targetPoint2;
    
    void Start()
    {
        mad = GetComponent<MatchAndDestroy>();
        isTileSelected = false;
        isCoroutine = false;
        CanBeMoved = true;
    }
   
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isTileSelected)
        {
            startPosMouse = cam.ScreenToWorldPoint(Input.mousePosition);
            MouseCapturesATile();
        }
       
        if (ted != null && ted2 != null)
        {     
            if (!isCoroutine)
            {
                StartCoroutine(MovingTileTime());
                isCoroutine = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (ted != null && ted2 != null)
        {
            ted.gameObject.transform.position = Vector2.MoveTowards(ted.gameObject.transform.position, targetPoint2, 0.1f);
            ted2.gameObject.transform.position = Vector2.MoveTowards(ted2.gameObject.transform.position, targetPoint1, 0.1f);
        }
    }

    private void MouseCapturesATile()
    {       
        RaycastHit2D hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Input.mousePosition));
       
        if (hit && CanBeMoved)
        {
            if (hit.collider.gameObject.CompareTag("Metal") || hit.collider.gameObject.CompareTag("Wood") || hit.collider.gameObject.CompareTag("Food")
                || hit.collider.gameObject.CompareTag("Gold") || hit.collider.gameObject.CompareTag("Coal") || hit.collider.gameObject.CompareTag("Know"))
            {
                ted = hit.collider.gameObject.GetComponent<TileEnvironmentDeterminer>();
                mad.Ted1 = hit.collider.gameObject.GetComponent<TileEnvironmentDeterminer>();
                targetPoint1 = ted.gameObject.transform.position;
                
                if (!isCoroutine)
                {                    
                    StartCoroutine(TrackTheMovement());
                    isCoroutine = true;
                }

                isTileSelected = true;
            }
        }        
    }

    private void PermutationOfTiles()
    {
        Vector2 directionOfMouse = endPosMouse - startPosMouse;        
        
        if (Mathf.Abs(directionOfMouse.x) > Mathf.Abs(directionOfMouse.y))
        {            
            if (directionOfMouse.x > 0)
            {                
                ted.RayForFindTileChange(out ted2, new Vector2(1.1f, 0));
            }
            else
            {
                ted.RayForFindTileChange(out ted2, new Vector2(-1.1f, 0));
            }
            mad.Ted2 = ted2;
        }
        else if (Mathf.Abs(directionOfMouse.x) < Mathf.Abs(directionOfMouse.y))
        {            
            if (directionOfMouse.y > 0)
            {
                ted.RayForFindTileChange(out ted2, new Vector2(0, 1.1f));
            }
            else
            {
                ted.RayForFindTileChange(out ted2, new Vector2(0, -1.1f));
            }
            mad.Ted2 = ted2;
        }
        else
        {
            ClearingTheValues();
        }
        
        if (ted2!=null)
        {
            targetPoint2 = ted2.gameObject.transform.position;            
        }
        else
        {
            isTileSelected = false;
        }
    }   

    private void RefreshingFlavors()
    {
        ted.ZeroingNeighboringTiles();
        ted2.ZeroingNeighboringTiles();
        ted.FindNeighboringTiles();
        ted2.FindNeighboringTiles();        
        ted.RedefiningAdjacentTiles();
        ted2.RedefiningAdjacentTiles();        
    }

    private void ClearingTheValues()
    {
        isCoroutine = false;
        isTileSelected = false;
        ted = null;
        ted2 = null;
        mad.Ted1 = null;
        targetPoint1 = new Vector2(0, 0);
        targetPoint2 = new Vector2(0, 0);
    }

    IEnumerator TrackTheMovement()
    {
        yield return new WaitForSeconds(0.2f);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            endPosMouse = cam.ScreenToWorldPoint(Input.mousePosition);
            PermutationOfTiles();
            isCoroutine = false;
        }
        else
        {
            ClearingTheValues();
        }
    }

    IEnumerator MovingTileTime()
    {        
        yield return new WaitForSeconds(0.3f);
        RefreshingFlavors();

        if (ted.FindMatch() > 0 || ted2.FindMatch() > 0)
        {
            mad.FindAndDestroyMatch();
            ClearingTheValues();
        }
        else
        {
            Vector2 tempPoint = targetPoint1;
            targetPoint1 = targetPoint2;
            targetPoint2 = tempPoint;            
            yield return new WaitForSeconds(0.3f);
            RefreshingFlavors();
            ClearingTheValues();
        }
    }    
}
