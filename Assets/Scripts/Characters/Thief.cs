
using System.Linq;
using Bases;
using UnityEngine;

public class Thief : Person
{
    private bool _isHouse = false;
    [SerializeField]
    private Transform areaCenter;

    [SerializeField] private int radius = 100;
    private void Awake()
    {
        name = "Thief";
        _money = 0;
        ID = 11;
        speed = 20f;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform != target)
        {
            if (other.transform.GetComponent<Worker>() != null || other.transform.GetComponent<Investor>() != null || other.transform.GetComponent<Miner>() != null)
            {
                // other.transform.GetComponent<Worker>().
                ContractAction(other.transform);
            }
        }
    }

    protected override void ActionInWayPoint(Transform target)
    {
        if (_isHouse)
        {
            waitingTime = 5f;
            Contractable moneyTarget = target.GetComponent<Contractable>();
            if (moneyTarget.GetBalance() > moneyTarget.GetMinimumMoney())
            {
                int m = _transferMoneyAmount;
                if (_transferMoneyAmount > moneyTarget.GetBalance() - moneyTarget.GetMinimumMoney())
                    m = moneyTarget.GetBalance() - moneyTarget.GetMinimumMoney();
                IncreaseMoney(m); 
                moneyTarget.DecreaseMoney(m);
            }
        }
        else
        {
            Contractable moneyTarget = target.GetComponent<Contractable>();
            if (!target.GetComponent<Person>().GetRubberyStatus() && moneyTarget.GetBalance()  > moneyTarget.GetMinimumMoney())
            {
                int m = _transferMoneyAmount;
                if (_transferMoneyAmount > moneyTarget.GetBalance() - moneyTarget.GetMinimumMoney())
                    m = moneyTarget.GetBalance() - moneyTarget.GetMinimumMoney();
                IncreaseMoney(m); 
                moneyTarget.DecreaseMoney(m);
                if (target.GetComponent<Person>() != null)
                {
                    target.GetComponent<Person>().JustBeenRubbed(transform);

                }
                
            }
            
        }
        
    }

    public override void UpdateWayPoints()
    {
        _wayPoints = listManager.GetHouses().Union(listManager.GetAlivePeople()).ToList();
        foreach (Transform wayPoint in _wayPoints)
        {
            if (!InArea(wayPoint))
            {
                _wayPoints.Remove(wayPoint);
            }
        }
    }

    protected override void SetWaitingTime()
    {
        if (target.GetComponent<House>() != null)
        {
            _isHouse = true;
            waitingTime = 1f;
        }
        else
        {
            _isHouse = false;
            waitingTime = 0.1f;
        }
    }

    private bool InArea(Transform target)
    {
        if (target.position.x > areaCenter.position.x + radius || target.position.x < areaCenter.position.x - radius)
            return false;
        if (target.position.z > areaCenter.position.z + radius || target.position.z < areaCenter.position.z - radius)
            return false;
        return true;
    }
    public override void Kill()
    {
        _isDead = true;
        StopAllCoroutines();
        UpdateTag();
        agent.isStopped = true;
        activeMoving = false;
        listManager.AddToDeadThieves(transform);
    }
    public override void Heal()
    {
        _isDead = false;
        UpdateTag();
        resetPerson();
        listManager.AddToAliveThieves(transform);
    }
}
