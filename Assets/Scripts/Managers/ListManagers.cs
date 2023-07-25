using System;
using System.Collections.Generic;
using System.Linq;
using Bases;
using UnityEngine;

public class ListManagers : MonoBehaviour
{
    [SerializeField] private Transform housesGO;
    [SerializeField] private Transform MinesGO;
    [SerializeField] private Transform PeopleGO;
    [SerializeField] private Transform ThievesGO;
    [SerializeField] private Transform AssassinsGo;
    [SerializeField] private Transform HealersGO;


    private List<Transform> Houses = new List<Transform>();

    private List<Transform> Mines = new List<Transform>();
    private List<Transform> DeadPeople = new List<Transform>();
    private List<Transform> AlivePeople = new List<Transform>();
    private List<Transform> Thieves = new List<Transform>();
    private List<Transform> Assassins = new List<Transform>();
    // private List<Transform> recentlyRubbed = new List<Transform>();
    // private List<Transform> freeToRub = new List<Transform>();
    private Dictionary<Transform, List<Transform>> recentlyRubbed = new Dictionary<Transform, List<Transform>>();
    private Dictionary<Transform, List<Transform>> freeToRub = new Dictionary<Transform, List<Transform>>();
    
    private List<Transform> DeadThieves = new List<Transform>();
    private List<Transform> DeadHealers = new List<Transform>();
    
    private List<Transform> AssassinTargets = new List<Transform>();
    private List<Transform> Healers = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        int peopleCount = PeopleGO.transform.childCount;
        int houseCount = housesGO.transform.childCount;
        int minesCount = MinesGO.transform.childCount;
        int thievesCount = ThievesGO.transform.childCount;
        int assassinsCount = AssassinsGo.transform.childCount;
        int healersCount = HealersGO.transform.childCount;

        int minCount = Mathf.Min(peopleCount, houseCount,minesCount,thievesCount,assassinsCount,healersCount);
        int i, j, k,l,a,h;
        
        
        for( i = 0, j =0, k = 0,l=0,a=0,h=0; i < minCount;i++,j++,k++,l++,a++,h++)
        {
            AlivePeople.Add(PeopleGO.transform.GetChild(i));
            Houses.Add(housesGO.transform.GetChild(j));
            Mines.Add(MinesGO.transform.GetChild(k));
            Thieves.Add(ThievesGO.transform.GetChild(l));
            Assassins.Add(AssassinsGo.transform.GetChild(a));
            Healers.Add(HealersGO.transform.GetChild(h));
        }

        while (i < peopleCount)
        {
            AlivePeople.Add(PeopleGO.transform.GetChild(i));
            i++;
        }
        while (j < houseCount)
        {
            Houses.Add(housesGO.transform.GetChild(j));
            j++;
        }       
        while (k < minesCount)
        {
            Mines.Add(MinesGO.transform.GetChild(k));
            k++;
        }
        while (l < thievesCount)
        {
            Thieves.Add(ThievesGO.transform.GetChild(l));
            l++;
        }
        while (a < assassinsCount)
        {
            Assassins.Add(AssassinsGo.transform.GetChild(a));
            a++;
        }
        while (h < healersCount)
        {
            Healers.Add(HealersGO.transform.GetChild(h));
            h++;
        }

        AssassinTargets = Thieves.Union(AlivePeople).ToList().Union(Healers).ToList();
        
        foreach (Transform t in Thieves)
        {
            freeToRub.Add(t,GetAlivePeople().Union(Healers).ToList());
            recentlyRubbed.Add(t,new List<Transform>());
        }
    }

    public void AddToDeadPeople(Transform p)
    {
        DeadPeople.Add(p);
        // if (freeToRub.Contains(p))
        //     freeToRub.Remove(p);
        foreach (Transform t in Thieves)
        {
            freeToRub.TryGetValue(t, out List<Transform> retrievedList1);
            if(retrievedList1.Contains(p))
                retrievedList1.Remove(p);
        }
        if (AssassinTargets.Contains(p))
            AssassinTargets.Remove(p);
        AlivePeople.Remove(p);
        noticeHealers();
    }
    public void AddToAlivePeople(Transform p)
    {
        AlivePeople.Add(p);
        // freeToRub.Add(p);
        foreach (Transform t in Thieves)
        {
            freeToRub.TryGetValue(t, out List<Transform> retrievedList1);
            retrievedList1.Add(p);
        }
        AssassinTargets.Add(p);
        DeadPeople.Remove(p);
    }
    public void AddToDeadThieves(Transform t)
    {
        DeadThieves.Add(t);
        if (AssassinTargets.Contains(t))
            AssassinTargets.Remove(t);
        Thieves.Remove(t);
        noticeHealers();
    }
    public void AddToAliveThieves(Transform t)
    {
        Thieves.Add(t);
        AssassinTargets.Add(t);
        DeadThieves.Remove(t);
    }
    public void AddToDeadHealers(Transform h)
    {
        DeadHealers.Add(h);
        if (AssassinTargets.Contains(h))
            AssassinTargets.Remove(h);
        foreach (Transform t in Thieves)
        {
            freeToRub.TryGetValue(t, out List<Transform> retrievedList1);
            if(retrievedList1.Contains(h))
                retrievedList1.Remove(h);
        }
        Healers.Remove(h);
        noticeHealers();
    }
    public void AddToAliveHealers(Transform h)
    {
        Healers.Add(h);
        AssassinTargets.Add(h);
        foreach (Transform t in Thieves)
        {
            freeToRub.TryGetValue(t, out List<Transform> retrievedList1);
            retrievedList1.Add(h);
        }
        DeadHealers.Remove(h);
    }
    public void RemoveFromMines(Transform m)
    {
        Mines.Remove(m);
    }

    public void RemoveTheHouse(Transform h)
    {
        Houses.Remove(h);
    }


    public List<Transform> GetAlivePeople()
    {
        return AlivePeople;
    }
    public List<Transform> GetAllDeadPeople()
    {
        return DeadHealers.Union(DeadPeople).ToList().Union(DeadThieves).ToList();
    }
    public List<Transform> GetHouses()
    {
        return Houses;
    }

    public List<Transform> GetMines()
    {
        return Mines;
    }

    public List<Transform> GetFreeToRub(Transform thief)
    {
        freeToRub.TryGetValue(thief, out List<Transform> retrievedList1);
        return retrievedList1;
    }
    public List<Transform> GetAssassinTargets()
    {
        return AssassinTargets;
    }

    public void AddToRubberyList(Transform r,Transform thief)
    {

        freeToRub.TryGetValue(thief, out List<Transform> retrievedList1);
        retrievedList1.Remove(r);

        recentlyRubbed.TryGetValue(thief, out List<Transform> retrievedList2);
        retrievedList2.Add(r);

        // freeToRub.Remove(r);
        // recentlyRubbed.Add(r);
    }
    public void RemoveFromRubbedPeople(Transform r,Transform thief)
    {
        freeToRub.TryGetValue(thief, out List<Transform> retrievedList1);
        retrievedList1.Add(r);

        recentlyRubbed.TryGetValue(thief, out List<Transform> retrievedList2);
        retrievedList2.Remove(r);
        
        // recentlyRubbed.Remove(r);
        // freeToRub.Add(r);
    }

    private void noticeHealers()
    {
        foreach (var healer in Healers)
        {
            healer.GetComponent<Person>().UpdateWayPoints();
        }
    }

    public void resetFromRobberyLists(Transform p)
    {
        foreach (Transform t in Thieves)
        {
            freeToRub.TryGetValue(t, out List<Transform> retrievedList1);
            if(!retrievedList1.Contains(p))
                RemoveFromRubbedPeople(p,t);
        }
    }
}
