using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : Character
{
    private int boosterAttack;

    public int BoosterAttack
    {
        get => boosterAttack;
    }

    public Forge()
    {
        attack = 0;
        health = 8;
        healthMax = 8;
        boosterAttack = 1;
    }

    public void ApplyBooster<T>(List<T> defender) where T : Character
    {
        for (int i = 0; i < defender.Count; i++)
        {
            defender[i].Attack += boosterAttack;
        }
    }

    public void CancelBooster<T>(List<T> defender) where T : Character
    {
        for (int i = 0; i < defender.Count; i++)
        {
            defender[i].Attack -= boosterAttack;
        }
    }
}
