using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPrice : MonoBehaviour
{
    [SerializeField] private List<int> priceStockade = new List<int>(6);
    [SerializeField] private List<int> priceForge = new List<int>(6);
    [SerializeField] private List<int> priceTower = new List<int>(6);
    [SerializeField] private List<int> priceBarracks = new List<int>(6);
    [SerializeField] private List<int> priceMarket = new List<int>(6);
    [SerializeField] private List<int> priceDragonCave = new List<int>(6);

    public List<int> PriceStockade
    {
        get => priceStockade;
    }

    public List<int> PriceForge
    {
        get => priceForge;
    }

    public List<int> PriceTower
    {
        get => priceTower;
    }

    public List<int> PriceBarracks
    {
        get => priceBarracks;
    }

    public List<int> PriceMarket
    {
        get => priceMarket;
    }

    public List<int> PriceDragonCave
    {
        get => priceDragonCave;
    }
}
