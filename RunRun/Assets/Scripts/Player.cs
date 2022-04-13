using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Touch touch;
    public float horizontalspeed,verticalspeed;
    CharacterController cc;
    Vector3 move = Vector3.zero;

    //        verticalspeed = 15;
    //        horizontalspeed = 0.003f;


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
   
}
