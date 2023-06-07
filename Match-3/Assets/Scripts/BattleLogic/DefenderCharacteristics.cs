using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefenderCharacteristics
{
    private static int warriorHealth = 5, warriorAttack = 2;
    private static int knightHealth = 10, knightAttack = 3;
    private static int heroHealth = 20, heroAttack = 5;
    private static int healerHealth = 5, healerAttack = 2;
    private static int wizzardHealth = 10 , wizzardAttack = 10;
    private static int dragonHealth = 50, dragonAttack = 20;

    public static int WarriorHealth
    {
        get { return warriorHealth; }
    }

    public static int WarriorAttack
    {
        get { return warriorAttack; }
    }

    public static int KnightHealth
    {
        get { return knightHealth; }
    }

    public static int KnightAttack
    {
        get { return knightAttack; }
    }

    public static int HeroHealth
    {
        get { return heroHealth; }
    }

    public static int HeroAttack
    {
        get { return heroAttack; }
    }

    public static int HealerHealth
    {
        get { return healerHealth; }
    }

    public static int HealerAttack
    {
        get { return healerAttack; }
    }

    public static int WizzardHealth
    {
        get { return wizzardHealth; }
    }

    public static int WizzardAttack
    {
        get { return wizzardAttack; }
    }

    public static int DragonHealth
    {
        get { return dragonHealth; }
    }

    public static int DragonAttack
    {
        get { return dragonAttack; }
    }

    public static int GetAttackDefender(int defenderAttack, int build)
    {
        return defenderAttack + build;
    }

    public static int GetHealthDefender(int defenderHealth, int build)
    {
        return defenderHealth + build;
    }
}
