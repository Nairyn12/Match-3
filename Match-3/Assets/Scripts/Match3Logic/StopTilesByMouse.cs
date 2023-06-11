using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTilesByMouse : MonoBehaviour
{
    [SerializeField] private GenerateTiles gt;
    [SerializeField] private MovingTilesByMouse moveTiles;

    public static StopTilesByMouse Instance;

    [SerializeField] private bool isChecking;
    [SerializeField] private int check;

    private CounterMoves quest;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        quest = GetComponent<CounterMoves>();
    }

    void Update()
    {
        if (isChecking)
        {
            CheckTilesOnMoving();
            isChecking = false;
        }        
    }


    public void CheckTilesOnMoving ()
    {
        check = 0;
        for (int i = 0; i < gt.TilesOnField.Count; i++)
        {
            if (gt.TilesOnField[i].IsMoving == true)
            {
                for (int j = 0; i < gt.TilesOnField.Count; i++)
                {
                    if (gt.TilesOnField[j].FindMatch() > 0)
                    {
                        check++;
                    }
                }
            }
        }
        Debug.Log("Check: " + check);

        if (check == 0)
        {            
            Debug.Log("���������� ���� ");
            moveTiles.CanBeMoved = true;
            Debug.Log("moveTiles.CanBeMoved " + moveTiles.CanBeMoved);
            quest.OnFinishPanel();
        }
        else if (check > 0)
        {
            isChecking = true;
        }

        if (check == 0 && !moveTiles.CanBeMoved)
        {
            Debug.Log("���������� ����, �� ������� ������ ");
            moveTiles.CanBeMoved = true;
        }
    } 
}
