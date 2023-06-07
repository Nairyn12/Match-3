using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleScript : MonoBehaviour
{
    [SerializeField] BattleMethods bm;
    [SerializeField] NumbersOfEnemies noe;
    [SerializeField] GameObject battlePanel;
    [SerializeField] GameObject battleButton;
    [SerializeField] GameObject closeButton;
    [SerializeField] private TMP_Text resultBattle;
    [SerializeField] EnemiesContainer enemies;
    [SerializeField] DefendersContainer defenders;
    [SerializeField] BuildingContainer buildings;
    [SerializeField] private bool roundOfDefender;
    [SerializeField] private SurpriseAttack sa;

    [SerializeField] private TMP_Text warriorsCountText, knightsCountText, heroesCountText, healersCounText, wizzardsCountText, dragonsCountText;

    private void Start()
    {
        battlePanel.SetActive(false);
        closeButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartBattlePanel();
        }
    }

    public void StartBattlePanel ()
    {
        //if (CountAttackContainer.BetweenAttacksCount > 2)
        //{
        //    battlePanel.SetActive(true);            
        //    noe.CountingTheNumberOfEnemies();
        //}
        
        noe.CountingTheNumberOfEnemies();
        PrintCountPers();
        UpdateBonusAttackForTypeOfDefender();
        buildings.AddAllBuild();
        enemies.PrintCountPers();
        battlePanel.SetActive(true);
    }

    public void StartBattle ()
    {       
        FightWithEnemiesOnStart();
        battleButton.SetActive(false);
        resultBattle.gameObject.SetActive(true);
    }

    public void FightWithEnemiesOnStart ()
    {        
        Debug.Log("Уровень заборов: " + buildings.Stockades.Count);
        if (buildings.Stockades.Count > 0)
        {
           roundOfDefender = true; 
           StartCoroutine(Move(defenders.AllDefendersWithoutHealer, enemies.AllEnemies, defenders.Healers));
            
        }
        else
        {
            roundOfDefender = false;
            StartCoroutine(Move(enemies.AllEnemiesWithoutPlagDoc, defenders.AllDefenders, enemies.PlagueDoctors));           
        }
    }

    public void FightWithEnemies()
    {
        if (roundOfDefender)
        {
            StartCoroutine(Move(defenders.AllDefendersWithoutHealer, enemies.AllEnemies, defenders.Healers));
        }
        else 
        {
            StartCoroutine(Move(enemies.AllEnemiesWithoutPlagDoc, defenders.AllDefenders, enemies.PlagueDoctors));
        }
    }

    public void UpdateListPers<T>(ref List<T> pers) where T : Character
    {
        for (int i = 0; i < pers.Count; i++)
        {
            if (pers[i].Health <= 0)
            {
                if (pers[i] is Warrior) defenders.Warriors.Remove(pers[i] as Warrior);
                else if (pers[i] is Knight) defenders.Knights.Remove(pers[i] as Knight);
                else if (pers[i] is Hero) defenders.Heroes.Remove(pers[i] as Hero);
                else if (pers[i] is Healer) defenders.Healers.Remove(pers[i] as Healer);
                else if (pers[i] is Wizzard) defenders.Wizzards.Remove(pers[i] as Wizzard);
                else if (pers[i] is Dragon) defenders.Dragons.Remove(pers[i] as Dragon);
                else if (pers[i] is Bandit) enemies.Bandits.Remove(pers[i] as Bandit);
                else if (pers[i] is Bully) enemies.Bullies.Remove(pers[i] as Bully);
                else if (pers[i] is Fiend) enemies.Fiends.Remove(pers[i] as Fiend);
                else if (pers[i] is PlagueDoctor) enemies.PlagueDoctors.Remove(pers[i] as PlagueDoctor);
                else if (pers[i] is Warlock) enemies.Warlocks.Remove(pers[i] as Warlock);
                else if (pers[i] is Beast) enemies.Beasts.Remove(pers[i] as Beast);
                else if (pers[i] is Stockade) buildings.Stockades.Remove(pers[i] as Stockade);
                else if (pers[i] is Forge) buildings.Forges.Remove(pers[i] as Forge);
                else if (pers[i] is Barracks) buildings.Barracks.Remove(pers[i] as Barracks);
                else if (pers[i] is MageTower) buildings.MageTowers.Remove(pers[i] as MageTower);
                else if (pers[i] is Market) buildings.Markets.Remove(pers[i] as Market);
                else if (pers[i] is DragonCave) buildings.DragonCaves.Remove(pers[i] as DragonCave);

                //defenders.RemovePers(pers as Character);
                //enemies.RemovePers(pers as Character);
                //buildings.RemoveBuildings(pers as Character);
                //Debug.Log("Удалили из списка: " + pers[i]);
                pers.Remove(pers[i]);
                //Debug.Log("Количество воинов: " + pers.Count);
            }            
        }        
    }

    public void PrintCountPers()
    {
        warriorsCountText.text = defenders.Warriors.Count.ToString();
        knightsCountText.text = defenders.Knights.Count.ToString();
        heroesCountText.text = defenders.Heroes.Count.ToString();
        healersCounText.text = defenders.Healers.Count.ToString();
        wizzardsCountText.text = defenders.Wizzards.Count.ToString();
        dragonsCountText.text = defenders.Dragons.Count.ToString();
    }  

    private void ShowResultsOfBattle(List<Character> attacking)
    {
        if (attacking[0] is Warrior || attacking[0] is Knight || attacking[0] is Hero || attacking[0] is Healer ||
            attacking[0] is Wizzard || attacking[0] is Dragon)
        {
            resultBattle.text = "Вы отбились от врагов!";
        }
        else
        {
            resultBattle.text = "Враги убили защитников и разорили деревню!";
            bm.BattleAttackPers<Character>(enemies.AllEnemies, buildings.AllBuilding);
            bm.LootingResources(enemies.AllEnemies);
            buildings.RemoveBuildings();            
            buildings.PrintCountBuildings();
        }        
    }

    public void ClosePanel ()
    {
        battlePanel.SetActive(false);
        closeButton.SetActive(false);
        battleButton.SetActive(true);
        resultBattle.gameObject.SetActive(false);
    }

    private void UpdateBonusAttackForTypeOfDefender()
    {
        for (int i = 0; i < buildings.Forges.Count; i++)
        {
            buildings.Forges[i].ApplyBooster(defenders.Warriors);
            buildings.Forges[i].ApplyBooster(defenders.Knights);
            buildings.Forges[i].ApplyBooster(defenders.Heroes);
        }

        for (int i = 0; i < buildings.Barracks.Count; i++)
        {
            buildings.Barracks[i].ApplyBooster(defenders.Warriors);
            buildings.Barracks[i].ApplyBooster(defenders.Knights);
            buildings.Barracks[i].ApplyBooster(defenders.Heroes);
        }

        for (int i = 0; i < buildings.MageTowers.Count; i++)
        {
            buildings.MageTowers[i].ApplyBooster(defenders.Healers);
            buildings.MageTowers[i].ApplyBooster(defenders.Wizzards);
        }
    }    

    IEnumerator Move <T>(List<Character> allAtacking, List<Character> allAttacked, List<T> doctors) where T: Character
    {
        if(roundOfDefender)
        {
            resultBattle.text = "Ход защитников деревни";
        }
        else
        {
            resultBattle.text = "Ход нападающих на деревню";
        }

        yield return new WaitForSeconds(1.0f);
        if (allAtacking.Count > 0 && allAttacked.Count > 0)
        {
            bm.BattleAttackPers<Character>(allAtacking, allAttacked);            
        }
        yield return new WaitForSeconds(0.5f);
        int chance = Random.Range(0, 2);

        if (chance == 0) sa.SetPositionInActive();

        yield return new WaitForSeconds(0.7f);

        sa.SetActiveButtonOff();

        UpdateListPers(ref allAttacked);

        if (doctors.Count> 0)
        {
            //Debug.Log("Ход докторов " + doctors.Count);
            bm.DoctorsAttack(doctors, ref allAtacking);
        }

        enemies.PrintCountPers();
        defenders.PrintCountPers();
        PrintCountPers();

        roundOfDefender = !roundOfDefender;

        if (allAttacked.Count > 0)
        {            
            FightWithEnemies();
        }
        else
        {
            ShowResultsOfBattle(allAtacking);
            closeButton.SetActive(true);
        }
    }  
}
