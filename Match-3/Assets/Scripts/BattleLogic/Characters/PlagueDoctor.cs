using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctor : Character
{
    public PlagueDoctor()
    {
        health = 5;
        attack = 1;
        healthMax = 5;
        looting = 2;
    }

    public override void AttackInBattle(ref int healthFriend)
    {
        healthFriend += attack;
    }
}
