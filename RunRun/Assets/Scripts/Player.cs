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

    public TextMeshProUGUI LevelNametxt;
   

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
    void PickingObject(int EarningMoney) 
    {
        Money += EarningMoney;
        if (Money >= ReqMoney[Level])
        {
            LevelUp(ReqMoney[Level] - Money);
        }
        else if (Money<0)
        {
            LevelDown(Money);
        }
    }


    void LevelUp(int excessmoney) 
    {
        Level++;
        Money = excessmoney;
        LevelNametxt.text = LevelName[Level];
    }
    void LevelDown(int excessmoney) {
        Level--;
        Money=Mathf.Abs(excessmoney);
        LevelNametxt.text = LevelName[Level];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Object")
        {
            
        }
    }

}
