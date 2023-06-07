using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Character
{
    public Healer()
    {
        health = 5;
        attack = 2;
        healthMax = 5;
    }

    public override void AttackInBattle(ref int healthFriend)
    {
        healthFriend += attack;
    }
}
