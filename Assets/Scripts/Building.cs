using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building : MonoBehaviour
{
    private  bool _isDestroyed = false;
    protected int _money = 100;
    private  int _transferMoneyAmount = 20;
    protected  string name = "Building";

    [SerializeField] private TextMeshProUGUI tag;
    
    private void Start()
    {
        UpdateTag();
    }


    public void IncreaseMoney(){
        _money += _transferMoneyAmount;
        UpdateTag();
    }
    
    public void DecreaseMoney(){
        if(_money > 0){
            _money -= _transferMoneyAmount;
        }

        if (_money < 0)
        {
            _isDestroyed = true;
            // actions according to becoming a destroyed building
        }

        UpdateTag();
    }
    private void UpdateTag()
    {
        tag.text = name + "\n Money: " + _money+"\n alive:"+ !_isDestroyed;
    }

}
