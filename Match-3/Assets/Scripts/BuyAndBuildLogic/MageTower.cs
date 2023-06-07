using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Character
{
    private int boosterHealth;
    private int boosterAttack;

    public int BoosterAttack
    {
        get => boosterAttack;
    }

    public int BoosterHealth
    {
        get => boosterHealth;
    }

    public MageTower()
    {
        attack = 0;
        health = 10;
        healthMax = 10;
        boosterHealth = 1;
        boosterAttack = 1;
    }

    public void ApplyBooster<T>(List<T> defender) where T : Character
    {
        for (int i = 0; i < defender.Count; i++)
        {
            defender[i].HealthMax += boosterHealth;
            defender[i].Health += boosterHealth;
            defender[i].Attack += boosterAttack;
        }
    }

    public void CancelBooster<T>(List<T> defender) where T : Character
    {
        for (int i = 0; i < defender.Count; i++)
        {
            defender[i].HealthMax -= boosterHealth;
            if (defender[i].HealthMax < defender[i].Health) defender[i].Health -= boosterHealth;
            defender[i].Attack -= boosterAttack;
        }
    }
}
