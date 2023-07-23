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
    private void Awake()
    {
        name = "Police";
        _money = 0;
        waitingTime = 0.1f;
        ID = 16;
        speed = 20f;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Person>() != null && !other.transform.GetComponent<Person>().IsInJail() && !other.transform.GetComponent<Person>().IsDead())
        {
            if (other.transform.GetComponent<BriberThief>() != null)
            {
                if(!BriberyAction(other.transform) && !criminals.Contains(other.transform))
                    criminals.Add(other.transform);
            }
            else if (other.transform.GetComponent<Assassin>() != null || other.transform.GetComponent<Thief>() != null)
            {
                if (!criminals.Contains(other.transform))
                {
                    criminals.Add(other.transform);
                }
            }

        }

    }

    protected override void ActionInWayPoint(Transform target)
    {
        if (criminals.Contains(target))
        {
            target.GetComponent<Person>().SendToJail();
            criminals.Remove(target);
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
