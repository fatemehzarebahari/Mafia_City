using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

public class Mine : Building
{
    private void Awake()
    {
        _money = 1000;
        name = "Mine";
        ID = 2;
    }
}
