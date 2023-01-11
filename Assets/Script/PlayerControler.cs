using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerControler : MonoBehaviourPunCallbacks
{

    public static PlayerControler Instancia{get;set;}

    float speed = 5f;
    float turbo = 10;

    private Transform ball;
    private FixedJoystick joy;
    public PhotonView pv;

    public bool canPlay;
    public int MyGol;

  
    void Awake(){
        canPlay = true;
        joy = GameObject.FindWithTag("joy").GetComponent<FixedJoystick>();
        pv = GetComponent<PhotonView>();
    }

    void FixedUpdate(){
        if(canPlay){
            if(Input.GetMouseButton(0)){
                Rotate(joy.Horizontal,joy.Vertical);
            }
            Move();
        }  
    }

    void Move(){
        if(pv.IsMine){
            float X = joy.Horizontal;// + Input.GetAxis("Horizontal");
            float Y = joy.Vertical;//+ Input.GetAxis("Vertical");
          
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(X*speed,Y*speed);  
        }
    }

    void Rotate(float x, float y){
        if(pv.IsMine){

            float Z = (Mathf.Atan2(x,y) * Mathf.Rad2Deg);
            this.transform.eulerAngles = new Vector3(0,0,-Z);
        
        }
    }

    public void Turbo(){
        if(turbo > 0){
            speed = 8f;
        }
    }


}