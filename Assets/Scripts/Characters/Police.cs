using System.Collections;
using System.Collections.Generic;
using Bases;
using Unity.VisualScripting;
using UnityEngine;

public class Police : Person
{
    [SerializeField] private List<Transform> patrolWayPoints = new List<Transform>();
    private List<Transform> criminals = new List<Transform>();
    private int bribe = 20;
    private Vector3 lastPosition = new Vector3();
    private int raycastDistance = 100;
    private void Awake()
    {
        name = "Police";
        _money = 0;
        waitingTime = 0.1f;
        ID = 16;
        speed = 20f;
    }

    protected override void Update()
    {
        if (!_isDead && _wayPoints.Count > 0 &&!_inJail){
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 10 && activeMoving)
            {
                ContractAction(target);
                MoveToWayPoint();

            }
            else if (activeMoving)
            {
                agent.SetDestination(target.position);
            } 
        }

        Vector3 raycastOrigin = lastPosition;

        Vector3 raycastDirection = (transform.position - lastPosition).normalized;

        Ray ray = new Ray(raycastOrigin, raycastDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.transform.GetComponent<Person>() != null && !hit.transform.GetComponent<Person>().IsInJail() && !hit.transform.GetComponent<Person>().IsDead())
            {
                if (hit.transform.GetComponent<Assassin>() != null || hit.transform.GetComponent<BriberThief>() != null|| hit.transform.GetComponent<Thief>() != null)
                {
                    if (!criminals.Contains(hit.transform))
                    {
                        criminals.Add(hit.transform);
                    }
                }
    
            } 
        }

        lastPosition = transform.position;
        Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.red, 0.1f);

    }


    protected override void ActionInWayPoint(Transform target)
    {
        if (criminals.Contains(target))
        {
            if (target.GetComponent<BriberThief>() != null && BriberyAction(target))
            {
                criminals.Remove(target);
            }
            else if(!target.GetComponent<Person>().IsDead())
            {
                target.GetComponent<Person>().SendToJail();
                criminals.Remove(target);
            }
        }
    }
    public override void UpdateWayPoints()
    {
        if (criminals.Count == 0)
        {
            _wayPoints = patrolWayPoints;
        }
        else
        {
            _wayPoints = criminals;
        }
    }
    
    private bool BriberyAction(Transform briberThief){
        if (briberThief.GetComponent<Person>().GetBalance() >= bribe)
        {
            briberThief.GetComponent<Person>().DecreaseMoney(bribe);
            IncreaseMoney(bribe);
            return true;
        }

        return false;
    }
}
