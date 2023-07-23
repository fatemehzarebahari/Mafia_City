using System.Collections;
using System.Collections.Generic;
using Bases;
using DG.Tweening;
using UnityEngine;

public class Healer : Person
{
    private void Awake()
    {
        name = "Healer";
        _money = 0;
        ID = 14;
        speed = 10f;
        waitingTime = 5f;
    }
    protected override void ActionInWayPoint(Transform target)
    {
        Person targetPerson = target.GetComponent<Person>();
        if (targetPerson.IsDead())
        {
            targetPerson.Heal();
            if (targetPerson.GetBalance() > targetPerson.GetMinimumMoney())
            {
                int m = _transferMoneyAmount;
                if (_transferMoneyAmount > targetPerson.GetBalance() - targetPerson.GetMinimumMoney())
                    m = targetPerson.GetBalance() - targetPerson.GetMinimumMoney();
                IncreaseMoney(m);
                targetPerson.DecreaseMoney(m);
            }
        }
    }
    
    protected override void MoveToWayPoint()
    {
        if (_wayPoints.Count > 0)
        {
            _currentWayPointIndex = Random.Range(0, _wayPoints.Count); 
            _currentWayPointIndex %= _wayPoints.Count;
            target = _wayPoints[_currentWayPointIndex];
        }
            
    }
    public override void UpdateWayPoints()
    {
        _wayPoints = listManager.GetAllDeadPeople();
        if (target == null)
        {
            MoveToWayPoint();
        }
    }

    public override void Kill()
    {
        _isDead = true;
        StopAllCoroutines();
        UpdateTag();
        agent.isStopped = true;
        activeMoving = false;
        listManager.AddToDeadHealers(transform);
    }
    public override void Heal()
    {
        _isDead = false;
        UpdateTag();
        resetPerson();
        listManager.AddToAliveHealers(transform);
    }

}
