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
        if (targetHouse.GetBalance() - _transferMoneyAmount >= targetHouse.GetMinimumMoney())
        {
            IncreaseMoney();
            targetHouse.DecreaseMoney();
        }


    }
    
    protected override void UpdateWayPoints()
    {
        _wayPoints = listManager.GetHouses();
    }

}
