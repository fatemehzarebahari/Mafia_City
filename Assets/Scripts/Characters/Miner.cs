
using Bases;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Miner : Person
{
    [SerializeField] private int index;
    private void Awake()
    {
        name = "Miner";
        _money = 0;
        ID = 11;
    }
    protected override void  Start()
    {
        UpdateTag();
        UpdateWayPoints();
        _currentWayPointIndex = index;
        MoveToWayPoint();
        agent.speed = speed;
        agent.acceleration = accelaration; 
    }
    protected override void ActionInWayPoint(Transform target)
    {
        Mine targetMine = target.GetComponent<Mine>();
        if (targetMine.GetBalance() > targetMine.GetMinimumMoney())
        {
            int m = _transferMoneyAmount;
            if (_transferMoneyAmount > targetMine.GetBalance() - targetMine.GetMinimumMoney())
                m = targetMine.GetBalance() - targetMine.GetMinimumMoney();
            IncreaseMoney(m); 
            targetMine.DecreaseMoney(m);
        }
        



    }

    public override void UpdateWayPoints()
    {
        _wayPoints = listManager.GetMines();
    }
}
