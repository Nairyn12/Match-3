using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class CounterMoves : MonoBehaviour
{
    [SerializeField] private TMP_Text moveCount;
    [SerializeField] private Image counterMovesImage;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject buyMovesButton;
    private ResourceCounter rc;
    private Color colorBuybutton;
    
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

    private void Start()
    {
        countOfMoves = 45;
        moveCount.text = countOfMoves.ToString();
        finishPanel.SetActive(false);
        rc = GetComponent<ResourceCounter>();
        colorBuybutton = buyMovesButton.GetComponent<Graphic>().color;
    }

    public void OnFinishPanel ()
    {
        if (countOfMoves < 44)
        {
            if (!finishPanel.activeInHierarchy) finishPanel.SetActive(true);
            buyMovesButton.GetComponent<Graphic>().color = Color.gray;
            buyMovesButton.GetComponent<Button>().interactable = false;
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
            moveCount.text = countOfMoves.ToString();
            ResourcesContainer.food -= 2;
            rc.OutputtingResourcesToIinterface();
            ReturnColorBuyButton();
        }
    }

    public void ReturnColorBuyButton ()
    {
        if (countOfMoves > 0)
        {            
            buyMovesButton.GetComponent<Graphic>().color = colorBuybutton;
            buyMovesButton.GetComponent<Button>().interactable = true;
        }
    }    
}
