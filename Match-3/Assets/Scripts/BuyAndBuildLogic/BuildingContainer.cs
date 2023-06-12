using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingContainer : MonoBehaviour
{    
    private List<Stockade> stockades = new List<Stockade>();
    private List<Forge> forges = new List<Forge>();
    private List<Barracks> barracks = new List<Barracks>();
    private List<MageTower> mageTowers = new List<MageTower>();
    private List<Market> markets = new List<Market>();
    private List<DragonCave> dragonCaves = new List<DragonCave>();

    private List<Character> allBuildings = new List<Character>();
    //private List<Character> allDefendersWithoutHealer = new List<Character>();

    [SerializeField] private TMP_Text stockadesCountText, forgesCountText, barracksCountText, mageTowersCounText, marketsCountText, dragonCavesCountText;

    [SerializeField] private GameManager gm;

    [SerializeField] private BuildingPosition _bp;

    public List<Stockade> Stockades
    {
        get => stockades;
        set { stockades = value; }
    }

    public List<Forge> Forges
    {
        get => forges;
        set { forges = value; }
    }

    public List<Barracks> Barracks
    {
        get => barracks;
        set { barracks = value; }
    }

    public List<MageTower> MageTowers
    {
        get => mageTowers;
        set { mageTowers = value; }
    }

    public List<Market> Markets
    {
        get => markets;
        set { markets = value; }
    }

    public List<DragonCave> DragonCaves
    {
        get => dragonCaves;
        set { dragonCaves = value; }
    }

    public List<Character> AllBuilding
    {
        get => allBuildings;
        set { allBuildings = value; }
    }

    private void Awake()
    {
        gm.loadingDefenders += AddBuildLoading;
        gm.savingDefenders += AddSavingBuildCount;
    }

    public void AddListBuild(int variant)
    {
        if (variant == 7)
        {
            stockades.Add(new());

        }
        else if (variant == 8)
        {
            forges.Add(new());
            _bp.OnBuilding(forges.Count, _bp._forgeGO);
        }
        else if (variant == 9)
        {
            barracks.Add(new());
            _bp.OnBuilding(barracks.Count, _bp._barrackGO);
        }
        else if (variant == 10)
        {
            mageTowers.Add(new());
            _bp.OnBuilding(mageTowers.Count, _bp._mageTowerGO);
        }
        else if (variant == 11)
        {
            markets.Add(new());
            _bp.OnBuilding(markets.Count, _bp._marketGO);
        }
        else if (variant == 12)
        {
            dragonCaves.Add(new());
        }

        PrintCountBuildings();
    }

    public void PrintCountBuildings()
    {
        stockadesCountText.text = "Óð. " + stockades.Count.ToString();
        forgesCountText.text = "Óð. " + forges.Count.ToString();
        barracksCountText.text = "Óð. " + barracks.Count.ToString();
        mageTowersCounText.text = "Óð. " + mageTowers.Count.ToString();
        marketsCountText.text = "Óð. " + markets.Count.ToString();
        dragonCavesCountText.text = "Óð. " + dragonCaves.Count.ToString();
    }

    public void AddAllBuild()
    {
        AddAllTypeBuild(allBuildings, stockades);
        AddAllTypeBuild(allBuildings, forges);
        AddAllTypeBuild(allBuildings, barracks);
        AddAllTypeBuild(allBuildings, mageTowers);
        AddAllTypeBuild(allBuildings, markets);
        AddAllTypeBuild(allBuildings, dragonCaves);
    }

    private void AddAllTypeBuild<T>(List<Character> allBuild, List<T> typeOfBuild) where T : new()
    {
        for (int i = 0; i < typeOfBuild.Count; i++)
        {
            allBuild.Add(typeOfBuild[i] as Character);
        }
    }

    public void RemoveBuildings()
    {
        for (int i = 0; i < allBuildings.Count; i++)
        {
            if (allBuildings[i].Health <= 0 && allBuildings[i] is Stockade)
            {
                stockades.Remove(allBuildings[i] as Stockade);
            }
            else if (allBuildings[i].Health <= 0 && allBuildings[i] is Forge)
            {
                forges.Remove(allBuildings[i] as Forge);
                _bp.OnRuinsBuilding(forges.Count, _bp._forgeGO, _bp._ruinsForgeSprite);
            }
            else if (allBuildings[i].Health <= 0 && allBuildings[i] is Barracks)
            {
                barracks.Remove(allBuildings[i] as Barracks);
                _bp.OnRuinsBuilding(barracks.Count, _bp._barrackGO, _bp._ruinsBarrackSprite);
            }
            else if (allBuildings[i].Health <= 0 && allBuildings[i] is MageTower)
            {
                mageTowers.Remove(allBuildings[i] as MageTower);
                _bp.OnRuinsBuilding(mageTowers.Count, _bp._mageTowerGO, _bp._ruinsMageTowerSprite);
            }
            else if (allBuildings[i].Health <= 0 && allBuildings[i] is Market)
            {
                markets.Remove(allBuildings[i] as Market);
                _bp.OnRuinsBuilding(markets.Count, _bp._marketGO, _bp._ruinsMarketSprite);
            }
            else if (allBuildings[i].Health <= 0 && allBuildings[i] is DragonCave)
            {
                dragonCaves.Remove(allBuildings[i] as DragonCave);
            }

        }
            AddAllBuild();
    }

    private void AddBuildForLoading<T>(List<T> pers, int count) where T : new()
    {
        for (int i = 0; i < count; i++)
        {
            pers.Add(new());
        }
    }

    private void AddBuildLoading()
    {
        AddBuildForLoading(stockades, gm.StockadesCount);
        AddBuildForLoading(forges, gm.ForgesCount);
        AddBuildForLoading(barracks, gm.BarracksCount);
        AddBuildForLoading(mageTowers, gm.MageTowersCount);
        AddBuildForLoading(markets, gm.MarketsCount);
        AddBuildForLoading(dragonCaves, gm.DragonCavesCount);

        PrintCountBuildings();

        Debug.Log("ÇÀÃÐÓÆÀÅÌ ÓÐÎÂÅÍÜ ÇÄÀÍÈÉ " + gm.WarriorsCount);
    }

    private void AddSavingBuildCount()
    {
        gm.StockadesCount = stockades.Count;
        gm.ForgesCount = forges.Count;
        gm.BarracksCount = barracks.Count;
        gm.MageTowersCount = mageTowers.Count;
        gm.MarketsCount = markets.Count;
        gm.DragonCavesCount = dragonCaves.Count;

        Debug.Log("ÑÎÕÐÀÍßÅÌ ÓÐÎÂÅÍÜ ÇÄÀÍÈÉ " + gm.StockadesCount);
    }
}
