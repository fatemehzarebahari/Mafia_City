using Bases;
using UnityEngine;

public class Assassin : Person
{
    
    private void Awake()
    {
        name = "Assassin";
        _money = 0;
        ID = 13;
        speed = 10f;
        waitingTime = 0.1f;
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform != target)
        {
            if (other.transform.GetComponent<Person>() != null && !other.transform.GetComponent<Person>().IsInJail())
            {
                if (other.transform.GetComponent<Worker>() != null ||
                    other.transform.GetComponent<Investor>() != null || other.transform.GetComponent<Miner>() != null ||
                    other.transform.GetComponent<Thief>() != null || other.transform.GetComponent<Healer>() != null)
                {
                    ContractAction(other.transform);
                }
            }
        }
    }
    protected override void ActionInWayPoint(Transform target)
    {

        Contractable assassinTarget = target.GetComponent<Contractable>();
        assassinTarget.Kill();

    }

    public override void UpdateWayPoints()
    {
        _wayPoints = listManager.GetAssassinTargets();
    }

    public override void Kill()
    {
        
    }
    public override void Heal()
    {
        
    }
}
