using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : Character
{
    private int boosterResource;
    

    public int BoosterResource
    {
        get => boosterResource;
    }   

    public Market()
    {
        attack = 0;
        health = 8;
        healthMax = 8;
        boosterResource = 1;        
    }
}
