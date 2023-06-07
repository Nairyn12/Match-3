using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyAndBuildVillage : MonoBehaviour
{    
    [SerializeField] private PriceDefenders pd;
    [SerializeField] private BuildingPrice bp;
    [SerializeField] private ResourceCounter rc;
    [SerializeField] private DefendersContainer dc;
    [SerializeField] private BuildingContainer bc;

    public void PanelDefenderOnnOf (GameObject panel)
    {
        if (panel.activeSelf) panel.SetActive(false);
        else panel.SetActive(true);
    }

    public void ButtonPressBuyPurchase(int purchase)
    {
        ChoosePurchase(purchase);        
        rc.OutputtingResourcesToIinterface();
    }

    private void ChoosePurchase(int choosePurchase)
    {
        if (choosePurchase == 1) Buy(pd.PriceWarrior, dc.Warriors.Count, choosePurchase);
        else if (choosePurchase == 2) Buy(pd.PriceKnight, dc.Knights.Count, choosePurchase);
        else if (choosePurchase == 3) Buy(pd.PriceHero, dc.Heroes.Count, choosePurchase);
        else if (choosePurchase == 4) Buy(pd.PriceHealer, dc.Healers.Count, choosePurchase);
        else if (choosePurchase == 5) Buy(pd.PriceWizzard, dc.Wizzards.Count, choosePurchase);
        else if (choosePurchase == 6) Buy(pd.PriceDragon, dc.Dragons.Count, choosePurchase);
        else if (choosePurchase == 7)
        {
            Buy(bp.PriceStockade, bc.Stockades.Count, choosePurchase);            
        }
        else if (choosePurchase == 8) Buy(bp.PriceForge, bc.Forges.Count, choosePurchase);
        else if (choosePurchase == 9) Buy(bp.PriceBarracks, bc.Barracks.Count, choosePurchase);
        else if (choosePurchase == 10) Buy(bp.PriceTower, bc.MageTowers.Count, choosePurchase);
        else if (choosePurchase == 11) Buy(bp.PriceMarket, bc.Markets.Count, choosePurchase);
        else if (choosePurchase == 12) Buy(bp.PriceDragonCave, bc.DragonCaves.Count, choosePurchase);
    }

    private void Buy (List<int> price, int purchase, int typeOfPurchase)
    {        
        if (ResourcesContainer.gold >= (price[0] * (purchase + 1)) && ResourcesContainer.metall >= (price[1] * (purchase + 1)) &&
            ResourcesContainer.coal >= (price[2] * (purchase + 1)) && ResourcesContainer.food >= (price[3] * (purchase + 1)) &&
            ResourcesContainer.wood >= (price[4] * (purchase + 1)) && ResourcesContainer.know >= (price[5] * (purchase + 1)))
        {           
            ResourcesContainer.gold -= price[0] * (purchase + 1);
            ResourcesContainer.metall -= price[1] * (purchase + 1);
            ResourcesContainer.coal -= price[2] * (purchase + 1);
            ResourcesContainer.food -= price[3] * (purchase + 1);
            ResourcesContainer.wood -= price[4] * (purchase + 1);
            ResourcesContainer.know -= price[5] * (purchase + 1);
            dc.AddListPers(typeOfPurchase); 
            bc.AddListBuild(typeOfPurchase); 
        }       
    }     
}
