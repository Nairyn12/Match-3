using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceBuilding : MonoBehaviour
{
    [SerializeField] private List<int> priceStockade = new List<int>() {15, 10};
    [SerializeField] private List<int> priceForge = new List<int>() { 10, 10, 10, 20};
    [SerializeField] private List<int> priceTower = new List<int>() { 10, 15, 10 };
    [SerializeField] private List<int> priceBarracks = new List<int>() { 6, 8 };
    [SerializeField] private List<int> priceMarket = new List<int>() { 10, 15, 10 };
    [SerializeField] private List<int> priceDragonCave = new List<int>() { 20, 20, 40, 30 };

    //public void CheckingCostStockade(int defender)
    //{
    //    if (ResourcesContainer.wood >= (priceStockade[0] * (defender + 1)) && ResourcesContainer.gold >= (priceStockade[1] * (defender + 1)))
    //    {
    //        BuildingContainer.stockade++;
    //        ResourcesContainer.wood -= priceStockade[0] * (defender + 1); 
    //        ResourcesContainer.gold -= priceStockade[1] * (defender + 1);
            
    //    }
    //}

    //public void CheckingCostForge(int defender)
    //{
    //    if (ResourcesContainer.wood >= priceForge[0] * (defender + 1) && ResourcesContainer.gold >= priceForge[1] * (defender + 1) &&
    //        ResourcesContainer.metall >= priceForge[2] * (defender + 1) && ResourcesContainer.coal >= priceForge[3] * (defender + 1))
    //    {
    //        BuildingContainer.forge++;
    //        ResourcesContainer.wood -= priceForge[0] * (defender + 1);
    //        ResourcesContainer.gold -= priceForge[1] * (defender + 1);
    //        ResourcesContainer.metall -= priceForge[2] * (defender + 1);
    //        ResourcesContainer.coal -= priceForge[2] * (defender + 1);
    //    }
    //}

    //public void CheckingCostTower(int defender)
    //{
    //    if (ResourcesContainer.wood >= priceTower[0] * (defender + 1) && ResourcesContainer.gold >= priceTower[1] * (defender + 1) &&
    //        ResourcesContainer.coal >= priceTower[2] * (defender + 1))
    //    {
    //        BuildingContainer.tower++;
    //        ResourcesContainer.food -= priceTower[0] * (defender + 1);
    //        ResourcesContainer.gold -= priceTower[1] * (defender + 1);
    //        ResourcesContainer.metall -= priceTower[2] * (defender + 1);
    //    }
    //}

    //public void CheckingCostHealer(int defender)
    //{
    //    if (ResourcesContainer.food >= priceBarracks[0] * (defender + 1) && ResourcesContainer.gold >= priceBarracks[1] * (defender + 1))
    //    {
    //        DefendersContainer.healerCount++;
    //        ResourcesContainer.food -= priceBarracks[0] * (defender + 1);
    //        ResourcesContainer.gold -= priceBarracks[1] * (defender + 1);
    //    }
    //}

    //public void CheckingCostWizzard(int defender)
    //{
    //    if (ResourcesContainer.food >= priceMarket[0] * (defender + 1) && ResourcesContainer.gold >= priceMarket[1] * (defender + 1) &&
    //        ResourcesContainer.coal >= priceMarket[2] * (defender + 1))
    //    {
    //        DefendersContainer.wizzardCount++;
    //        ResourcesContainer.food -= priceMarket[0] * (defender + 1);
    //        ResourcesContainer.gold -= priceMarket[1] * (defender + 1);
    //        ResourcesContainer.coal -= priceMarket[2] * (defender + 1);
    //    }
    //}

    //public void CheckingCostDragon(int defender)
    //{
    //    if (ResourcesContainer.food >= priceDragonCave[0] * (defender + 1) && ResourcesContainer.gold >= priceDragonCave[1] * (defender + 1) &&
    //        ResourcesContainer.metall >= priceDragonCave[2] * (defender + 1) && ResourcesContainer.coal >= priceDragonCave[3] * (defender + 1))
    //    {
    //        DefendersContainer.dragonCount++;
    //        ResourcesContainer.food -= priceDragonCave[0] * (defender + 1);
    //        ResourcesContainer.gold -= priceDragonCave[1] * (defender + 1);
    //        ResourcesContainer.metall -= priceDragonCave[2] * (defender + 1);
    //        ResourcesContainer.coal -= priceDragonCave[3] * (defender + 1);
    //    }
    //}

}

