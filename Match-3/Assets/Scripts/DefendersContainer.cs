using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefendersContainer : MonoBehaviour
{
    
    private List<Warrior> warriors = new List<Warrior>();
    private List<Knight> knights = new List<Knight>();
    private List<Hero> heroes = new List<Hero>();
    private List<Character> healers = new List<Character>();
    private List<Wizzard> wizzards = new List<Wizzard>();
    private List<Dragon> dragons = new List<Dragon>();

    private List<Character> allDefenders = new List<Character>();
    private List<Character> allDefendersWithoutHealer = new List<Character>();

    [SerializeField] private TMP_Text warriorsCountText, knightsCountText, heroesCountText, healersCounText, wizzardsCountText, dragonsCountText;

    [SerializeField] private GameManager gm;

    public List<Warrior> Warriors
    {
        get => warriors;
        set { warriors = value; }
    }

    public List<Knight> Knights
    {
        get => knights;
        set { knights = value; }
    }

    public List<Hero> Heroes
    {
        get => heroes;
        set { heroes = value; }
    }

    public List<Character> Healers
    {
        get => healers;
        set { healers = value; }
    }

    public List<Wizzard> Wizzards
    {
        get => wizzards;
        set { wizzards = value; }
    }

    public List<Dragon> Dragons
    {
        get => dragons;
        set { dragons = value; }
    }

    public List<Character> AllDefenders
    {
        get => allDefenders;
        set { allDefenders = value; }
    }

    public List<Character> AllDefendersWithoutHealer
    {
        get => allDefendersWithoutHealer;
        set { allDefendersWithoutHealer = value; }
    }

    private void Awake()
    {
        gm.loadingDefenders += AddPersLoading;
        gm.savingDefenders += AddSavingPersCount;
    }    

    public void AddListPers(int variant)
    {
        if (variant == 1) warriors.Add(new ());
        else if (variant == 2) knights.Add(new());
        else if (variant == 3) heroes.Add(new());
        else if (variant == 4) healers.Add(new Healer());
        else if (variant == 5) wizzards.Add(new());
        else if (variant == 6) dragons.Add(new());       

        PrintCountPers();
    }

    public void PrintCountPers ()
    {
        warriorsCountText.text = warriors.Count.ToString();
        knightsCountText.text = knights.Count.ToString();
        heroesCountText.text = heroes.Count.ToString();
        healersCounText.text = healers.Count.ToString();
        wizzardsCountText.text = wizzards.Count.ToString();
        dragonsCountText.text = dragons.Count.ToString();
    }

    public void AddAllPers()
    {
        AddAllTypePers(allDefenders, warriors, warriors.Count);
        AddAllTypePers(allDefenders, knights, knights.Count);
        AddAllTypePers(allDefenders, heroes, heroes.Count);
        AddAllTypePers(allDefenders, healers, healers.Count);
        AddAllTypePers(allDefenders, wizzards, wizzards.Count);
        AddAllTypePers(allDefenders, dragons, dragons.Count);
    }

    public void AddAllPersWithoutHealers()
    {
        AddAllTypePers(allDefendersWithoutHealer, warriors, warriors.Count);
        AddAllTypePers(allDefendersWithoutHealer, knights, knights.Count);
        AddAllTypePers(allDefendersWithoutHealer, heroes, heroes.Count);       
        AddAllTypePers(allDefendersWithoutHealer, wizzards, wizzards.Count);
        AddAllTypePers(allDefendersWithoutHealer, dragons, dragons.Count);
    }

    private void AddAllTypePers<T>(List<Character> allPers, List<T> typeOfPers, int count) where T : new()
    {        
        for (int i = 0; i < count; i++)
        {            
            allPers.Add(typeOfPers[i] as Character);
        }
    }

    private void AddPersForLoading<T>(List<T> pers, int count) where T : new()
    {
        for (int i = 0; i < count; i++)
        {
            pers.Add(new());
        }
    }

    private void AddPersLoading ()
    {
        AddPersForLoading(warriors, gm.WarriorsCount);
        AddPersForLoading(knights, gm.KnightsCount);
        AddPersForLoading(heroes, gm.HeroesCount);
        AddPersForLoading(healers, gm.HealersCount);
        AddPersForLoading(wizzards, gm.WizzardsCount);
        AddPersForLoading(dragons, gm.DragonsCount);

        PrintCountPers();

        Debug.Log("ÇÀÃÐÓÆÀÅÌ ÊÎËÈ×ÅÑÒÂÎ ÇÀÙÈÒÍÈÊÎÂ " + gm.WarriorsCount);
    }

    private void AddSavingPersCount ()
    {
        gm.WarriorsCount = warriors.Count;
        gm.KnightsCount = knights.Count;
        gm.HeroesCount = heroes.Count;
        gm.HealersCount = healers.Count;
        gm.WizzardsCount = wizzards.Count;
        gm.DragonsCount = dragons.Count;

        Debug.Log("ÑÎÕÐÀÍßÅÌ ÊÎËÈ×ÅÑÒÂÎ ÇÀÙÈÒÍÈÊÎÂ " + gm.WarriorsCount);
    }

    public void RemovePers(Character pers)
    {
        allDefenders.Remove(pers);

        if (pers is Warrior)
        {
            warriors.Remove(pers as Warrior);
            allDefendersWithoutHealer.Remove(pers as Warrior);
        }
        else if (pers is Knight)
        {
            knights.Remove(pers as Knight);
            allDefendersWithoutHealer.Remove(pers as Knight);
        }
        else if (pers is Hero)
        {
            heroes.Remove(pers as Hero);
            allDefendersWithoutHealer.Remove(pers as Hero);
        }
        else if (pers is Healer) healers.Remove(pers as Healer);
        else if (pers is Wizzard)
        {
            wizzards.Remove(pers as Wizzard);
            allDefendersWithoutHealer.Remove(pers as Wizzard);
        }
        else if (pers is Dragon)
        {
            dragons.Remove(pers as Dragon);
            allDefendersWithoutHealer.Remove(pers as Dragon);
        }
    }
}
