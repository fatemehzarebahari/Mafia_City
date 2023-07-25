using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

public class Investor : Person
{
    private bool _isGatheringMoney = false;
    private int _increasePercent = 2;
    
    private void Awake()
    {
        name = "Investor";
        _minMoney = 100;
        _money = _minMoney;
        ID = 12;
        waitingTime = 3;
    }
    protected override void  Start()
    {
        UpdateTag();
    }
    protected override void Update()
    {
        if (!_isDead ){
            if (!_isGatheringMoney)
            {
                StartCoroutine(WaitAndCall());
            }
        }

    }

    protected override IEnumerator WaitAndCall()
    {
        _isGatheringMoney = true;
        yield return new WaitForSeconds(waitingTime);
        _transferMoneyAmount = (int)((_money * _increasePercent) / 100);
        IncreaseMoney(_transferMoneyAmount);
        _isGatheringMoney = false;

    }
}
