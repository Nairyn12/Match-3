using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceDefenders : MonoBehaviour
{
    [SerializeField] private List<int> priceWarrior = new List<int>(6);
    [SerializeField] private List<int> priceKnight = new List<int>(6);
    [SerializeField] private List<int> priceHero = new List<int>(6);
    [SerializeField] private List<int> priceHealer = new List<int>(6);
    [SerializeField] private List<int> priceWizzard = new List<int>(6);
    [SerializeField] private List<int> priceDragon = new List<int>(6);

    public List<int> PriceWarrior
    {
        get => priceWarrior;
    }

    public List<int> PriceKnight
    {
        get => priceKnight;
    }

    public List<int> PriceHero
    {
        get => priceHero;
    }

    public List<int> PriceHealer
    {
        get => priceHealer;
    }

    public List<int> PriceWizzard
    {
        get => priceWizzard;
    }

    public List<int> PriceDragon
    {
        get => priceDragon;
    }
}
