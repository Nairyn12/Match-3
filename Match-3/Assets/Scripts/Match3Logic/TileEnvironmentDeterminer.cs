using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEnvironmentDeterminer : MonoBehaviour
{   
    [SerializeField] private int matchCount;
    [SerializeField] private bool onHorizontalMatchLeft;
    [SerializeField] private bool onHorizontalMatchRight;
    [SerializeField] private bool onVerticalMatchUp;
    [SerializeField] private bool onVerticalMatchDown;

    [SerializeField] private TileEnvironmentDeterminer matchTilesUp;
    [SerializeField] private TileEnvironmentDeterminer matchTilesDown;
    [SerializeField] private TileEnvironmentDeterminer matchTilesRight;
    [SerializeField] private TileEnvironmentDeterminer matchTilesLeft;

    [SerializeField] private float speedMove;
    [SerializeField] private bool isMoving;
    [SerializeField] private MatchAndDestroy mad;
    [SerializeField] private GenerateTiles gt;
    [SerializeField] private MovingTilesByMouse movingTilesByMouse;

    private Rigidbody2D rb;

    public string Tag => gameObject.tag;

    public TileEnvironmentDeterminer MatchTilesUp => matchTilesUp;

    public TileEnvironmentDeterminer MatchTilesDown => matchTilesDown;

    public TileEnvironmentDeterminer MatchTilesRight => matchTilesRight;

    public TileEnvironmentDeterminer MatchTilesLeft => matchTilesLeft;

    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }

    private void Awake()
    {
        mad = FindObjectOfType<MatchAndDestroy>();
        gt = FindObjectOfType<GenerateTiles>();
        rb = GetComponent<Rigidbody2D>();        
    }

    private void Start()
    {
        onHorizontalMatchLeft = false;
        onHorizontalMatchRight = false;
        onVerticalMatchUp = false;
        onVerticalMatchDown = false;        
    }

    private void Update()
    {        
        if (isMoving)
        {            
            MoveTiles();            
        }

        if (transform.position.y <= 3.5f)
        {
            matchTilesUp = FindTileInDirection(Vector2.up, 0.7f);
        }

        ClearingNeighborTiles(); 
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = new Vector2(0.0f, -speedMove);
        }
        else
        {
            rb.velocity *= 0;
        }
    }

    public int CheckMatchingOnStart()
    {
        matchCount = 0;

        FindNeighboringTiles();

        if (HasSameTag(matchTilesUp) && HasSameTag(matchTilesDown))
        {
            matchCount++;
        }

        if (HasSameTag(matchTilesRight) && HasSameTag(matchTilesLeft))
        {
            matchCount++;
        }

        return matchCount;
    }  

    public void FindNeighboringTiles()
    {
        matchTilesUp = FindTileInDirection(Vector2.up, 0.7f);        
        matchTilesDown = FindTileInDirection(Vector2.down, 0.7f);
        matchTilesRight = FindTileInDirection(Vector2.right, 0.7f);
        matchTilesLeft = FindTileInDirection(Vector2.left, 0.7f);
    }    

    private bool HasSameTag(TileEnvironmentDeterminer ted)
    {
        if (ted != null)
        {
            if (ted.gameObject.tag == gameObject.tag)
            {
                return true;
            }
        }

        return false;
    }

    public void RayForFindTileChange(out TileEnvironmentDeterminer t, Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.6f);
        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Metal") || hit.collider.gameObject.CompareTag("Wood") || hit.collider.gameObject.CompareTag("Food")
                || hit.collider.gameObject.CompareTag("Gold") || hit.collider.gameObject.CompareTag("Coal") || hit.collider.gameObject.CompareTag("Know"))
            {
                t = hit.collider.gameObject.GetComponent<TileEnvironmentDeterminer>();
            }
            else
            {
                t = null;
            }
        }
        else
        {
            t = null;
        }
    }

    public int FindMatch()
    {
        int match = 0;

        CompareNeighboringTiles(ref match, matchTilesUp, matchTilesDown);
        CompareNeighboringTiles(ref match, matchTilesRight, matchTilesLeft);
        if (matchTilesUp != null) CompareNeighboringTiles(ref match, matchTilesUp, matchTilesUp.matchTilesUp);
        if (matchTilesDown != null) CompareNeighboringTiles(ref match, matchTilesDown, matchTilesDown.matchTilesDown);
        if (matchTilesRight != null) CompareNeighboringTiles(ref match, matchTilesRight, matchTilesRight.matchTilesRight);
        if (matchTilesLeft != null) CompareNeighboringTiles(ref match, matchTilesLeft, matchTilesLeft.matchTilesLeft);

        return match;
    }

    private void CompareNeighboringTiles(ref int count, TileEnvironmentDeterminer t1, TileEnvironmentDeterminer t2)
    {
        if (t1 != null && t2 != null)
        {
            if (t1.gameObject.tag == gameObject.tag && t2.gameObject.tag == gameObject.tag)
            {
                count++;
            }
        }
    }

    public void FindAllNeighboringTiles()
    {
        ZeroingNeighboringTiles();
        FindNeighboringTiles();
        RedefiningAdjacentTiles();        
    }

    public TileEnvironmentDeterminer FindTileInDirection(Vector2 v, float distance)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, v, distance);

        if (hit2D)
        {            
            return hit2D.collider.gameObject.GetComponent<TileEnvironmentDeterminer>();
        }
        else return null;
    }

    public void RedefiningAdjacentTiles()
    {
        if (matchTilesUp != null && matchTilesUp.matchTilesDown != this)
        {
            matchTilesUp.matchTilesDown = this;
        }

        if (matchTilesDown != null && matchTilesDown.matchTilesUp != this)
        {            
            matchTilesDown.matchTilesUp = this;           
        }

        if (matchTilesRight != null && matchTilesRight.matchTilesLeft != this)
        {
            matchTilesRight.matchTilesLeft = this;
        }

        if (matchTilesLeft != null && matchTilesLeft.matchTilesRight != this)
        {
            matchTilesLeft.matchTilesRight = this;
        }
    }

    public void ZeroingNeighboringTiles()
    {
        if (matchTilesUp != null) matchTilesUp.matchTilesDown = null;
        if (matchTilesDown != null) matchTilesDown.matchTilesUp = null;
        if (matchTilesRight != null) matchTilesRight.matchTilesLeft = null;
        if (matchTilesLeft != null) matchTilesLeft.matchTilesRight = null;
        matchTilesUp = null;
        matchTilesDown = null;
        matchTilesRight = null;
        matchTilesLeft = null;
    }

    public void CheckNeedMove()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 1.0f))
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    public void MoveTiles()
    {        
        if (MatchTilesUp != null && MatchTilesUp.isMoving == false) MatchTilesUp.isMoving = true;

        if (transform.localPosition.y <= -4.5f)
        {            
            StopTiles();
            transform.position = new Vector2(transform.localPosition.x, -4.5f);
        }
    }

    public void StopTiles()
    {
        StartCoroutine(DelayToUpdateAdjacentTiles());
        isMoving = false;
        StopTilesByMouse.Instance.CheckTilesOnMoving();
      
        if (matchTilesUp != null) MatchTilesUp.StopTiles();
    }
   
    private float RoundToFraction(float value, float fraction)
    {
        return (float)(Math.Round(value / (double)fraction) * fraction);
    }

    public void ClearingNeighborTiles()
    {
        if (matchTilesUp != null && !matchTilesUp.gameObject.activeSelf) matchTilesUp = null;
        if (matchTilesDown != null && !matchTilesDown.gameObject.activeSelf) matchTilesDown = null;
        if (matchTilesRight != null && !matchTilesRight.gameObject.activeSelf) matchTilesRight = null;
        if (matchTilesLeft != null && !matchTilesLeft.gameObject.activeSelf) matchTilesLeft = null;        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((transform.position.y - col.gameObject.transform.position.y) > 0.5f)
        {
            if (!col.gameObject.GetComponent<TileEnvironmentDeterminer>().isMoving && isMoving)
            {               
                transform.position = new Vector2(transform.position.x, col.gameObject.transform.position.y + 1.0f);
                StopTiles();

            }
        }
    }

    IEnumerator DelayToUpdateAdjacentTiles()
    {
        yield return new WaitForSeconds(0.1f);
        gt.GetNeighboringTiles();
        if (matchTilesDown != null)
        {
            transform.position = new Vector2(transform.position.x, matchTilesDown.gameObject.transform.position.y + 1);
        }
        yield return new WaitForSeconds(0.1f);
        mad.FindAndDestroyMatch();
        
    }

    public IEnumerator DelayToStartMove()
    {
        yield return new WaitForSeconds(0.2f);

        Debug.Log(gameObject.name + " " + isMoving);
        isMoving = true;
    }

    public IEnumerator DelayToCheckNeedMove()
    {
        yield return new WaitForSeconds(0.2f);

        CheckNeedMove();
    }
}
