using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMethods : MonoBehaviour
{
    [SerializeField] private NumbersOfEnemies enemyCount;
    [SerializeField] private EnemiesContainer enemies;
    [SerializeField] private DefendersContainer defenders;
    [SerializeField] private BuildingContainer buildings;
    [SerializeField] private ResourceCounter rc;

    public void BattleAttackPers<T>(List<T> attackingPers, List<T> alttackedPers) where T : Character
    {       
        for (int i = 0; i < attackingPers.Count; i++)
        {            
            for (int j = 0; j < alttackedPers.Count; j++)
            {
                if (alttackedPers[j].Health > 0 && attackingPers[i].Health > 0)
                {
                    alttackedPers[j].Health -= attackingPers[i].Attack;                   
                    break;
                }
            }
        }
    }

    public void DoctorsAttack<T> (List <T> doctors, ref List<Character> pers) where T : Character
    {        
        for (int i = 0; i < doctors.Count; i++)
        {            
            for (int j = 0; j < pers.Count; j++)
            {                
                if (pers[j].Health > 0 & pers[j].Health < pers[j].HealthMax && doctors[i].Health > 0)
                {
                    pers[j].Health =+ doctors[i].Attack;                    
                    break;
                }
            }
        }
    } 

    public void LootingResources (List<Character> pers)
    {
        for (int i = 0; i < pers.Count; i++)
        {
            pers[i].LootingAfterBattle();
        }

        rc.OutputtingResourcesToIinterface();
    }
}

    
