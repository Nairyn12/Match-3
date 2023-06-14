using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountAttackContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _randomCountBetweenAttacksText;
    [SerializeField] private TMP_Text _attackCountText;
    [SerializeField] private TMP_Text _betweenAttacksCountText;

    [SerializeField] private GameManager _gm;

    private int _randomCountBetweenAttacks;
    private int attackCount;
    private int betweenAttacksCount;

    public int AttackCount
    {
        get => attackCount;
        
    }

    private void Awake()
    {
        _gm.loadingEvent += LoadingCountsAttack;
        _gm.savingEvent += SavingCountsAttack;
    }

    private void Update()
    {

        //////ÄËß ÒÅÑÒÀ
        _randomCountBetweenAttacksText.text = _randomCountBetweenAttacks.ToString();
        _attackCountText.text = attackCount.ToString();
        _betweenAttacksCountText.text = betweenAttacksCount.ToString();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _randomCountBetweenAttacks = 0;
            attackCount = 0;
            betweenAttacksCount = 0;
        }
    }

    public void AttackCountUp()
    {
        if (betweenAttacksCount >= _randomCountBetweenAttacks)
        {
            attackCount++;            
            betweenAttacksCount = 0;           
            SetRandomAttackCount();           
        }        
    }

    public void BeetwenAttackCountUp ()
    {
        betweenAttacksCount++;
        _gm.BetweenCountOfAttacks = betweenAttacksCount;
        AttackCountUp();
    }

    public int SetRandomAttackCount()
    {
        _randomCountBetweenAttacks = Random.Range(4, 8);
        return _randomCountBetweenAttacks;
    }

    private void LoadingCountsAttack ()
    {
        attackCount = _gm.CountOfAttacks;
        betweenAttacksCount = _gm.BetweenCountOfAttacks;
        if (_gm.RandomBetweenCountOfAttacks == 0) _randomCountBetweenAttacks = SetRandomAttackCount();
        else _randomCountBetweenAttacks = _gm.RandomBetweenCountOfAttacks;
    }

    private void SavingCountsAttack()
    {
        _gm.CountOfAttacks = attackCount;
        _gm.BetweenCountOfAttacks = betweenAttacksCount;
        _gm.RandomBetweenCountOfAttacks = _randomCountBetweenAttacks;
    }


}
