
using System.Linq;
using Bases;
using UnityEngine;

public class Thief : Person
{
    private void Awake()
    {
        name = "Thief";
        _money = 0;
        ID = 11;
        speed = 20f;
        waitingTime = 0.1f;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform != target)
        {
            if (other.transform.GetComponent<Worker>() != null)
            {
                // other.transform.GetComponent<Worker>().
                ContractAction(other.transform);
            }
        }
    }

    protected override void ActionInWayPoint(Transform target)
    {
        if (target.GetComponent<House>() != null)
        {
            waitingTime = 5f;
            Contractable moneyTarget = target.GetComponent<Contractable>();
            if (moneyTarget.GetBalance() >= _transferMoneyAmount)
            {
                IncreaseMoney();
                moneyTarget.DecreaseMoney();
            }
        }
        else
        {
            waitingTime = 0.1f;
            // how is it working??????????? 
            Contractable moneyTarget = target.GetComponent<Contractable>();
            if (!target.GetComponent<Person>().GetRubberyStatus() && moneyTarget.GetBalance() - _transferMoneyAmount >= moneyTarget.GetMinimumMoney())
            {
                IncreaseMoney();
                moneyTarget.DecreaseMoney();
                if (target.GetComponent<Person>() != null)
                {
                    target.GetComponent<Person>().JustBeenRubbed();

                }
                
            }
            
        }



    }
    
    protected override void UpdateWayPoints()
    {
        _wayPoints = listManager.GetHouses().Union(listManager.GetAlivePeople()).ToList();
    }
}
