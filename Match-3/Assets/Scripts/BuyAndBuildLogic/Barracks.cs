using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Character
{
    private int boosterHealth;

    public int BoosterHealth
    {
        get => boosterHealth;
    }

    public Barracks()
    {
        attack = 0;
        health = 7;
        healthMax = 7;
        boosterHealth = 1;
    }

    public void ApplyBooster<T>(List<T> defender) where T : Character
    {
        for (int i = 0; i < defender.Count; i++)
        {
            defender[i].HealthMax += boosterHealth;
            defender[i].Health += boosterHealth;
        }
    }

    public void CancelBooster<T>(List<T> defender) where T : Character
    {
        for (int i = 0; i < defender.Count; i++)
        {
            defender[i].HealthMax -= boosterHealth;
            if (defender[i].HealthMax < defender[i].Health) defender[i].Health -= boosterHealth;
        }
    }
}
