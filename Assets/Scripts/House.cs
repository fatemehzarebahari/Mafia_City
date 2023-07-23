using System;
using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

public class House : Building
{
    private void Awake()
    {
        _money = 500;
        name = "House";
        ID = 1;
    }
    public override void Kill()
    {
        _isDead = true;
        listManager.RemoveFromHouses(transform);
    }
    

}
