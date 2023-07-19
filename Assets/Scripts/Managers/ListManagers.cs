using System;
using System.Collections.Generic;
using UnityEngine;

public class ListManagers : MonoBehaviour
{
    [SerializeField] private Transform housesGO;
    [SerializeField] private Transform MinesGO;
    [SerializeField] private Transform PeopleGO;
    [SerializeField] private Transform ThievesGO;
    // [SerializeField] private Transform AssassinsGo;
    // [SerializeField] private Transform WorkersGO;


    private List<Transform> Houses = new List<Transform>();
    private List<Transform> Mines = new List<Transform>();
    private List<Transform> DeadPeople = new List<Transform>();
    private List<Transform> AlivePeople = new List<Transform>();
    private List<Transform> Thieves = new List<Transform>();
    
    private List<Transform> recentlyRubbed = new List<Transform>();
    private List<Transform> freeToRub = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        int peopleCount = PeopleGO.transform.childCount;
        int houseCount = housesGO.transform.childCount;
        int minesCount = MinesGO.transform.childCount;
        int thievesCount = ThievesGO.transform.childCount;

        int minCount = Mathf.Min(peopleCount, houseCount,minesCount,thievesCount);
        int i, j, k,l;
        
        
        for( i = 0, j =0, k = 0,l=0; i < minCount;i++,j++,k++,l++)
        {
            AlivePeople.Add(PeopleGO.transform.GetChild(i));
            Houses.Add(housesGO.transform.GetChild(j));
            Mines.Add(MinesGO.transform.GetChild(k));
            Thieves.Add(ThievesGO.transform.GetChild(l));
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

        freeToRub = GetAlivePeople();

    }

    public void AddToDeadPeople(Transform p)
    {
        DeadPeople.Add(p);
        AlivePeople.Remove(p);
    }
    public void AddToAlivePeople(Transform p)
    {
        AlivePeople.Add(p);
        DeadPeople.Remove(p);
    }
    public void RemoveFromMines(Transform m)
    {
        Mines.Remove(m);
    }
    public void RemoveFromHouses(Transform h)
    {
        Houses.Remove(h);
    }

    public List<Transform> GetAlivePeople()
    {
        return AlivePeople;
    }
    public List<Transform> GetDeadPeople()
    {
        return DeadPeople;
    }
    public List<Transform> GetHouses()
    {
        return Houses;
    }
    public List<Transform> GetMines()
    {
        return Mines;
    }

    public List<Transform> GetFreeToRub()
    {
        return freeToRub;
    }
    public void AddToRubberyList(Transform r)
    {
        freeToRub.Remove(r);
        recentlyRubbed.Add(r);
    }
    public void RemoveFromRubbedPeople(Transform r)
    {
        
        recentlyRubbed.Remove(r);
        freeToRub.Add(r);
    }
}
