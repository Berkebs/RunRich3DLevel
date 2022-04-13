using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    private Touch touch;
    public float horizontalspeed,verticalspeed;
    CharacterController cc;
    Vector3 move = Vector3.zero;

    //        verticalspeed = 15;
    //        horizontalspeed = 0.003f;

    public int Level = 0;
    public int Money = 0;
    public int[] ReqMoney;
    public string[] LevelName;

    public TextMeshPro LevelNametxt;
   

    void Start()
    {
        DOTween.Init();
        cc = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0)
        {

            touch = Input.GetTouch(0);

            if (touch.phase==TouchPhase.Moved)
            {
                move.x = transform.position.x + touch.deltaPosition.x;
                
                cc.Move(move * horizontalspeed);
            }

        }
        cc.Move(Vector3.forward * Time.deltaTime * verticalspeed);
    }


    void PickingObject(Objectcs go) 
    {
        Money += go.EarningMoney();
        if (Money >= ReqMoney[Level])
        {
            LevelUp(Money-ReqMoney[Level]);
        }
        else if (Money<0)
        {
            LevelDown(ReqMoney[Level]-Money);
        }
        Debug.Log("Level : " + Level + ",  Money : " + Money);
    }


    void LevelUp(int excessmoney)
    {
        Debug.Log("Level Up  Level : " + Level + " Money : " + Money);
        if (Level<ReqMoney.Length)
        {
            Level++;
            Money = excessmoney;
        }
        else
        {
            Debug.Log("MaxLevel");
        }

        LevelNametxt.text = LevelName[Level];
    }
    void LevelDown(int excessmoney) {
        Debug.Log("Level Down  Level : " + Level + " Money : " + Money);
        if (Level>0)
        {
            Level--;
            Money = excessmoney;
        }
        else
        {
            Debug.Log("GameOver");
        }

        LevelNametxt.text = LevelName[Level];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Object")
        {
            PickingObject(other.gameObject.GetComponent<Objectcs>());
        }
    }

}
