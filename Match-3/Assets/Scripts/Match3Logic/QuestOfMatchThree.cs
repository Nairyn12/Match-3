using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class QuestOfMatchThree : MonoBehaviour
{
    [SerializeField] private TMP_Text moveCount;
    [SerializeField] private Image counterMovesImage;
    [SerializeField] private GameObject finishPanel;    
    
    private int countOfMoves;

    public int CountOfMoves 
    {
        get { return countOfMoves; }
        set 
        { 
            countOfMoves = value;
            moveCount.text = countOfMoves.ToString();            
        }
    }
   

    //[SerializeField] private GameObject prefabGlass, prefabRodents, prefabThieves, prefabGreedy;
    //[SerializeField] private Sprite spriteMine, spriteField, spriteMarket, spriteWood, spriteLibrary;



    private void Start()
    {
        countOfMoves = 45;
        moveCount.text = countOfMoves.ToString();
        finishPanel.SetActive(false);
    }

    public void OnFinishPanel ()
    {
        if (countOfMoves < 44)
        {
            if (!finishPanel.activeInHierarchy) finishPanel.SetActive(true);            
        }
    }

    public void OffFinishPanel()
    {
        if (countOfMoves > 1)
        {
            if (finishPanel.activeInHierarchy) finishPanel.SetActive(false);
        }
    }

    public void BuyMoves ()
    {
        if (ResourcesContainer.food > 0)
        {
            countOfMoves++;
            ResourcesContainer.food--;
        }
    }
}
