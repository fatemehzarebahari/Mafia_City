using System.Collections;
using Bases;
using UnityEngine;
public class Worker : Person
{
    [SerializeField] private ListManagers listManager;

    private void Awake()
    {
        name = "Worker";
        _money = 0;
    }

    protected override void ActionInWayPoint()
    {
        IncreaseMoney();
        target.GetComponent<House>().DecreaseMoney();
    }
    
    protected override void UpdateWayPoints()
    {
        _wayPoints = listManager.GetHouses();
    }

}
