using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurpriseAttack : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private BattleMethods bm;
    [SerializeField] private EnemiesContainer enemies;
    

    private void Start()
    {
        button.gameObject.SetActive(false);        
        button.transform.position = new Vector2(3, -3);
    }

    public void SetPositionInActive()
    {
        float posX, posY;
        posX = Random.Range(-4.1f, 4.1f);
        posY = Random.Range(-3.1f, 3.1f);
        button.transform.position = new Vector2 (posX, posY);
        button.gameObject.SetActive(true);
    }

    public void SetActiveButtonOff()
    {
        button.gameObject.SetActive(false);
    }

    public void SurpriseAttackOn()
    {
        for (int i = 0; i < enemies.AllEnemies.Count; i++)
        {
            if (enemies.AllEnemies[i].Health > 0)
            {
                enemies.AllEnemies[i].Health -= 3;
            }
        }

        Debug.Log("Attac Surprise");

        button.gameObject.SetActive(false);
    }




}
