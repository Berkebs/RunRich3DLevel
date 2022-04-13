using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Objectcs : MonoBehaviour
{
    public Object myObject;
    public int EarningMoney()
    {
        transform.DOScale(0, 1);
        int _money = Random.Range(myObject.MinMoney, myObject.MaxMoney);
        return _money;
    }
}
