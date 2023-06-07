using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountAttackContainer : MonoBehaviour
{
    private int attackCount = 0;
    private  int betweenAttacksCount;

    public int AttackCount
    {
        get => attackCount;
        set { attackCount = value; }
    }

    public int BetweenAttacksCount
    {
        get => betweenAttacksCount;
        set { betweenAttacksCount = value; }
    }

    public void AttackCountUp()
    {
        if (betweenAttacksCount == 5)
        {
            attackCount++;
        }

        betweenAttacksCount = 0;
    }

    public void BeetwenAttackCountUp ()
    {
        betweenAttacksCount++;
        
        AttackCountUp();
    }
}
