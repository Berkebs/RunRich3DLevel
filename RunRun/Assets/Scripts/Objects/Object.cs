using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Object : ScriptableObject
{
    public string ObjectName;
    public int MaxMoney;
    public int MinMoney;

    public int EarningMoney() 
    {
        int _money = Random.Range(MinMoney,MaxMoney);
        return _money;
    }
    
}
