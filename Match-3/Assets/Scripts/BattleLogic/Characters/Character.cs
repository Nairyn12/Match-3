using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{    
    protected int attack;
    protected int health;
    protected int healthMax;
    protected int looting;

    public int Attack
    {
        get => attack;
        set { attack = value; }
    }

    public int Health
    {
        get => health;
        set 
        {
            if (value <= healthMax && value >= 0) health = value;
            else if (value < 0) health = 0;
            else health = healthMax;
        }
    }

    public int HealthMax
    {
        get => healthMax;
        set { healthMax = value; }
    }

    public Character()
    {

    }

    public virtual void AttackInBattle (ref int healthEnemy)
    {
        healthEnemy -= attack;
    }

    public void LootingAfterBattle ()
    {
        int typeOfResources = Random.Range(1,6);

        if (typeOfResources == 1) DeterminingStealResources(ref ResourcesContainer.gold, ref typeOfResources); 
        if (typeOfResources == 2) DeterminingStealResources(ref ResourcesContainer.coal, ref typeOfResources);
        if (typeOfResources == 3) DeterminingStealResources(ref ResourcesContainer.metall, ref typeOfResources);
        if (typeOfResources == 4) DeterminingStealResources(ref ResourcesContainer.food, ref typeOfResources);
        if (typeOfResources == 5) DeterminingStealResources(ref ResourcesContainer.wood, ref typeOfResources);
        if (typeOfResources == 6) DeterminingStealResources(ref ResourcesContainer.know, ref typeOfResources);
    }

    protected void DeterminingStealResources (ref int typeRes, ref int res)
    {
        if (typeRes > looting) typeRes -= looting;
        else if (typeRes < looting && typeRes > 0) typeRes = 0;
        else res++;
        Debug.Log(typeRes + " " + res);
    }








}
