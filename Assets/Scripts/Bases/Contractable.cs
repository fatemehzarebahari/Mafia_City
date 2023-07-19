using System.Collections;
using TMPro;
using UnityEngine;

public class Contractable : MonoBehaviour
{
    protected  bool _isDead = false;
    protected int _money = 100;
    protected int _minMoney = 0;
    protected int rubberyTime = 20;
    protected Transform target ;

    protected  int _transferMoneyAmount = 20;
    protected  string name = "Contractable";

    protected float waitingTime = 5f;

    protected int ID = -1;
    
    [SerializeField] private TextMeshProUGUI tag;
    protected virtual void Start()
    {
        UpdateTag();
    }


    public void IncreaseMoney(){
        _money += _transferMoneyAmount;
        UpdateTag();
    }
    
    public virtual void DecreaseMoney(){
        if(_money > _minMoney){
            _money -= _transferMoneyAmount;
        }

        UpdateTag();
    }
    protected void UpdateTag()
    {
        tag.text = name + "\n Money: " + _money+"\n alive:"+ !_isDead;
    }

    public int GetID()
    {
        return ID;
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public int GetBalance()
    {
        return _money;
    }

    public int GetMinimumMoney()
    {
        return _minMoney;
    }
    public void ContractAction(Transform target)
    {
        StartCoroutine(WaitAndCall(target));
    }
    
    public virtual IEnumerator WaitAndCall(Transform target)
    {
        yield return new WaitForSeconds(waitingTime);

    }
    public void Kill()
    {
        _isDead = true;
    }


}
