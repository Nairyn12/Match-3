using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    private List<Bandit> bandits = new List<Bandit>();
    private List<Bully> bullies = new List<Bully>();
    private List<Fiend> fiends = new List<Fiend>();
    private List<Character> plagueDoctors = new List<Character>();
    private List<Warlock> warlocks = new List<Warlock>();
    private List<Beast> beasts = new List<Beast>();

    private List<Character> allEnemies = new List<Character>();
    private List<Character> allEnemiesWithoutPlagDoc = new List<Character>();

    [SerializeField] private TMP_Text banditsCountText, bulliesCountText, fiendsCountText, plagueDoctorsCounText, warlocksCountText, beastsCountText;

    public List<Bandit> Bandits
    {
        get => bandits;
        set { bandits = value; }
    }

    public List<Bully> Bullies
    { 
        get => bullies;
        set { bullies = value; }
    }

    public List<Fiend> Fiends
    {
        get => fiends;
        set { fiends = value; }
    }

    public List<Character> PlagueDoctors
    {
        get => plagueDoctors;
        set { plagueDoctors = value; }
    }

    public List<Warlock> Warlocks
    {
        get => warlocks;
        set { warlocks = value; }
    }

    public List<Beast> Beasts
    {
        get => beasts;
        set { beasts = value; }
    }

    public List<Character> AllEnemies
    {
        get => allEnemies;
        set { allEnemies = value; }
    }

    public List<Character> AllEnemiesWithoutPlagDoc
    {
        get => allEnemiesWithoutPlagDoc;
        set { allEnemiesWithoutPlagDoc = value; }
    }

    public void AddAllPersWithoutPlagDoc()
    {
        AddAllTypePers(allEnemiesWithoutPlagDoc, bandits);
        AddAllTypePers(allEnemiesWithoutPlagDoc, bullies);
        AddAllTypePers(allEnemiesWithoutPlagDoc, fiends);        
        AddAllTypePers(allEnemiesWithoutPlagDoc, warlocks);
        AddAllTypePers(allEnemiesWithoutPlagDoc, beasts);
    }

    public void AddAllPers()
    {
        AddAllTypePers(allEnemies, bandits);
        AddAllTypePers(allEnemies, bullies);
        AddAllTypePers(allEnemies, fiends);
        AddAllTypePers(allEnemies, plagueDoctors);
        AddAllTypePers(allEnemies, warlocks);
        AddAllTypePers(allEnemies, beasts);
    }

    private void AddAllTypePers<T>(List<Character> allPers, List<T> typeOfPers) where T : new()
    {
        for (int i = 0; i < typeOfPers.Count; i++)
        {
            allPers.Add(typeOfPers[i] as Character);
        }
    }

    public void RemovePers(Character pers)
    {
        allEnemies.Remove(pers);

        if (pers is Bandit)
        {
            bandits.Remove(pers as Bandit);
            allEnemiesWithoutPlagDoc.Remove(pers as Bandit);
        }
        else if (pers is Fiend)
        {
            fiends.Remove(pers as Fiend);
            allEnemiesWithoutPlagDoc.Remove(pers as Fiend);
        }
        else if (pers is Bully)
        {
            bullies.Remove(pers as Bully);
            allEnemiesWithoutPlagDoc.Remove(pers as Bully);
        }
        else if (pers is PlagueDoctor) plagueDoctors.Remove(pers as PlagueDoctor);
        else if (pers is Warlock)
        {
            warlocks.Remove(pers as Warlock);
            allEnemiesWithoutPlagDoc.Remove(pers as Wizzard);
        }
        else if (pers is Beast)
        {
            beasts.Remove(pers as Beast);
            allEnemiesWithoutPlagDoc.Remove(pers as Beast);
        }
    }

    public void PrintCountPers()
    {
        banditsCountText.text = bandits.Count.ToString();
        bulliesCountText.text = bullies.Count.ToString();
        fiendsCountText.text = fiends.Count.ToString();
        plagueDoctorsCounText.text = plagueDoctors.Count.ToString();
        warlocksCountText.text = warlocks.Count.ToString();
        beastsCountText.text = beasts.Count.ToString();
    }


}
