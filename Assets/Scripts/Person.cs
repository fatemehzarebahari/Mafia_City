using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    private protected bool _isDead = false;
    private protected bool _inJail = false;
    private protected int _money = 0;
    private protected int _minMoney = 0;
    private protected int _transferMoneyAmount = 20;
    private protected int _currentWayPointIndex = 0;
    private protected string name = "Person";
    protected Transform target ;
    
    private protected List<Transform> _wayPoints;
    private int moveMentDuration = 5;
    private bool activeMoving = true;

    [SerializeField] private TextMeshProUGUI tag;
    [SerializeField] private NavMeshAgent agent;
    private void Start()
    {
        UpdateTag();
        UpdateWayPoints();
        MoveToWayPoint();
        agent.speed = 10f;
        agent.acceleration = 20f; 
    }

    private void Update()
    {
        if (_wayPoints.Count > 0){
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 10 && activeMoving)
            {
                // transform.DOKill();
                agent.isStopped = true;
                activeMoving = false;
                StartCoroutine(WaitAndCall());
            }
            else if (activeMoving)
            {
                // transform.DOMove(targetDestination,5f);
                agent.SetDestination(target.position);
            } 
        }

}

    public void IncreaseMoney()
    {
        _money += _transferMoneyAmount;
        UpdateTag();

    }
    
    public void DecreaseMoney(){
        if(_money > _minMoney){
            _money -= _transferMoneyAmount;
        }
        UpdateTag();
    }

    protected virtual void MoveToWayPoint()
    {
        target = _wayPoints[_currentWayPointIndex];
        _currentWayPointIndex++;
        _currentWayPointIndex %= _wayPoints.Count;

    }

    private void UpdateTag()
    {
        tag.text = name + "\n Money: " + _money+"\n alive:"+ !_isDead;
    }
    protected virtual IEnumerator WaitAndCall()
    {
        yield return new WaitForSeconds(5f);
        activeMoving = true;
        agent.isStopped = false;
        ActionInWayPoint();
        UpdateWayPoints();
        MoveToWayPoint();
        agent.speed = 10f;
        agent.acceleration = 10f; 



    }
    protected virtual void ActionInWayPoint()
    {
        // what to do in each way point
    }

    protected virtual void UpdateWayPoints()
    {
        // update wayPoints with new ones
    }

}



