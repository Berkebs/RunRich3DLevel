using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

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
    public Image MoneyImage;
    Vector3 Charactergo;
    void Start()
    {
        DOTween.Init();
        cc = this.gameObject.GetComponent<CharacterController>();
        MoneyImage.fillAmount = (float)Money/ (float)ReqMoney[Level];
        LevelNametxt.text = LevelName[Level];
        Charactergo = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0)
        {

            touch = Input.GetTouch(0);

            if (touch.phase==TouchPhase.Moved)
            {

                if (Charactergo==Vector3.forward|| Charactergo == Vector3.back)
                {
                    move.x = touch.deltaPosition.x;
                }
                else
                {
                    move.z = -touch.deltaPosition.x;
                }

                cc.Move(move * horizontalspeed);
            }

        }
        cc.Move(Charactergo * Time.deltaTime * verticalspeed);
    }

    void ForwardController(float rotate) 
    {
        switch (rotate)
        {
            case 0:
                Charactergo = Vector3.forward;
                break;
            case 90:
                Charactergo = Vector3.right;
                break;
            case -90:
                Charactergo = Vector3.left;
                break;
            case -180:
                Charactergo = Vector3.back;
                break;
        }
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
        MoneyImage.fillAmount = (float)Money / (float)ReqMoney[Level];
        LevelNametxt.text = LevelName[Level];
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Object")
        {
            PickingObject(other.gameObject.GetComponent<Objectcs>());
        }
        else if (other.gameObject.name=="RightCol")
        {
            ForwardController(transform.rotation.y + 90);
            transform.DOLocalRotate(new Vector3(transform.rotation.x,transform.rotation.y+90,transform.rotation.z),1);
        }
        else if (other.gameObject.name=="LeftCol")
        {
            ForwardController(transform.rotation.y - 90);
            transform.DOLocalRotate(new Vector3(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z), 1);
        }
    }

}
