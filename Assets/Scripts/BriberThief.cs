using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

public class BriberThief : Thief
{
    private void Awake()
    {
        name = "BriberThief";
        _money = 0;
        ID = 15;
        speed = 20f;
    }
}
