using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersOfEnemies : MonoBehaviour   
{
    [SerializeField] private int differenceMin, differenceMax;
    [SerializeField] private EnemiesContainer enemies;
    [SerializeField] private DefendersContainer defenders;
    [SerializeField] private BuildingContainer buildings;
    [SerializeField] private CountAttackContainer countAttack;


    public void CountingTheNumberOfEnemies()
    {
        defenders.AddAllPersWithoutHealers();
        defenders.AddAllPers();

        int countOfBuild = buildings.Barracks.Count + buildings.Forges.Count + buildings.Stockades.Count +
            buildings.Markets.Count + buildings.MageTowers.Count + buildings.DragonCaves.Count;

        int countEnemies = Random.Range((defenders.AllDefenders.Count + countOfBuild/2 - differenceMin), (defenders.AllDefenders.Count + countOfBuild/2 + differenceMax));

        if (defenders.AllDefenders.Count == 0)
        {
            int count = 2;
            CalculateDifference(ref countEnemies, enemies.Bandits, 0);
        }
        else
        {
            CalculateDifference(ref countEnemies, enemies.Bandits, 1);
            CalculateDifference(ref countEnemies, enemies.Bullies, 1);
            if (buildings.Stockades.Count != 0) CalculateDifference(ref countEnemies, enemies.Fiends, 2);
            if (countAttack.AttackCount > 5 && buildings.Barracks.Count != 0) CalculatePlagueDoctors(ref countEnemies);
            if (countAttack.AttackCount > 10 && buildings.MageTowers.Count != 0) CalculateDifference(ref countEnemies, enemies.Warlocks, 1);
            if (countAttack.AttackCount > 15 && (enemies.Warlocks.Count > 5 || buildings.DragonCaves.Count > 0)) CalculateDifference(ref countEnemies, enemies.Beasts, 1);
            if (countEnemies != 0) CalculateDifference(ref countEnemies, enemies.Bandits, 1);
        }

        enemies.AddAllPersWithoutPlagDoc();
        enemies.AddAllPers();
    }

    private void CalculateDifference<T>(ref int count, List<T> pers, int coefficient) where T : new ()
    {
        int r = Random.Range((count - count * coefficient), count);

        for (int i = 0; i < r; i++)
        {
            pers.Add(new T());
        }

        count -= r;
    }

    private void CalculatePlagueDoctors(ref int count)
    {
        int r = Random.Range((count - count * 2 / 3), count);

        for (int i = 0; i < r; i++)
        {
            enemies.PlagueDoctors.Add(new PlagueDoctor());
        }

        count -= r;
    }   

}
