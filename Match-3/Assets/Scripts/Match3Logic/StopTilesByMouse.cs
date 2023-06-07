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

    private void Awake()
    {
        Instance = this;
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
                check++;
            }
        }
        Debug.Log("Check: " + check);

        if (check == 0)
        {
            Debug.Log("Совпадений ноль ");
            moveTiles.CanBeMoved = true;
            Debug.Log("moveTiles.CanBeMoved " + moveTiles.CanBeMoved);
        }
        else if (check > 0)
        {
            isChecking = true;
        }

        if (check == 0 && !moveTiles.CanBeMoved)
        {
            Debug.Log("Совпадений ноль, но двигать нельзя ");
            moveTiles.CanBeMoved = true;
        }
    } 
}
