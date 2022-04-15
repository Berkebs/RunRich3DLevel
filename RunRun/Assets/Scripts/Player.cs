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
    int totalReqMoney;
    public string[] LevelName;


    public TextMeshProUGUI LevelNametxt,MoneyText;
    public Image MoneyImage;

    public Animator Playeranim;
    Vector3 Charactergo;

    public GameObject Model;
    float rot=0;
    void Start()
    {
        Playeranim.SetTrigger("Run");   
        cc = this.gameObject.GetComponent<CharacterController>();

        LevelNametxt.text = LevelName[Level];
        Charactergo = Vector3.forward;
        foreach (int item in ReqMoney)
        {
            totalReqMoney += item;
        }
        MoneyImage.fillAmount = (float)Money / (float)totalReqMoney;
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
            LevelUp();
        }
        else if (Money<ReqMoney[Level])
        {
            LevelDown();
        }
        MoneyText.text = Money.ToString();
        MoneyImage.fillAmount = (float)Money / (float)totalReqMoney;
        LevelNametxt.text = LevelName[Level];
    }


    void LevelUp()
    {
        if (Level<ReqMoney.Length)
        {
            Debug.Log(Level + "     " + ReqMoney.Length);
            Level++;
            Playeranim.SetTrigger("Jump");

        }
        else
        {
            Debug.Log("MaxLevel");
        }
    }
    void LevelDown() {
        if (Level>0)
        {
            Level--;
        }
        else
        {
            Playeranim.SetTrigger("Die");
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
            rot += 90;
            ForwardController(rot);
            //transform.DOLocalRotate(new Vector3(transform.rotation.x,transform.rotation.y+90,transform.rotation.z),1);
            transform.DORotate(new Vector3(transform.rotation.x, rot, transform.rotation.z), 1);
        }
        else if (other.gameObject.name=="LeftCol")
        {
            rot -= 90;
            ForwardController(rot);
            transform.DOLocalRotate(new Vector3(transform.rotation.x, rot, transform.rotation.z), 1);
        }
        else if (other.gameObject.name=="Finish")
        {
            verticalspeed = 0;
            Playeranim.SetTrigger("Dance");
        }
    }

}
