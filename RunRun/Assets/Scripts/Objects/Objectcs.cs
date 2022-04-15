using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Objectcs : MonoBehaviour
{
    public Object myObject;
    public int EarningMoney()
    {
        transform.DOScale(0, 0.1f);
        int _money = Random.Range(myObject.MinMoney, myObject.MaxMoney);
        return _money;
    }
}
