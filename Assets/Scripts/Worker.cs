using System.Collections;
using Bases;
using UnityEngine;
public class Worker : Person
{
    private void Awake()
    {
        name = "Worker";
        _money = 0;
        ID = 10;
        
    }

    protected override void ActionInWayPoint(Transform target)
    {
        House targetHouse = target.GetComponent<House>();
        if (targetHouse.GetBalance() > targetHouse.GetMinimumMoney())
        {
            int m = _transferMoneyAmount;
            if (_transferMoneyAmount > targetHouse.GetBalance() - targetHouse.GetMinimumMoney())
                m = targetHouse.GetBalance() - targetHouse.GetMinimumMoney();
            IncreaseMoney(m); 
            targetHouse.DecreaseMoney(m);
        }


    }

    public override void UpdateWayPoints()
    {
        _wayPoints = listManager.GetHouses();
    }


}
