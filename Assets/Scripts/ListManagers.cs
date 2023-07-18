using System;
using System.Collections.Generic;
using UnityEngine;

public class ListManagers : MonoBehaviour
{
    [SerializeField] private Transform housesGO;
    [SerializeField] private Transform MinesGO;
    [SerializeField] private Transform PeopleGO;

    private List<Transform> Houses = new List<Transform>();
    private List<Transform> Mines = new List<Transform>();
    private List<Transform> DeadPeople = new List<Transform>();
    private List<Transform> AlivePeople = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        int peopleCount = PeopleGO.transform.childCount;
        int houseCount = housesGO.transform.childCount;
        int minesCount = MinesGO.transform.childCount;
        int minCount = Math.Min(peopleCount, houseCount);
        minCount = Math.Min(minCount,minesCount);
        int i, j, k;
        
        
        for( i = 0, j =0, k = 0; i < minCount;i++,j++,k++)
        {
            AlivePeople.Add(PeopleGO.transform.GetChild(i));
            Houses.Add(housesGO.transform.GetChild(j));
            Mines.Add(MinesGO.transform.GetChild(k));
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
}
