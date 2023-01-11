using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // public GameObject[] players;
    // GameObject playerProx;
    
    // float disMin = 100;
    // int cont =0;
    
    // void Start(){
    //     players = GameObject.FindGameObjectsWithTag("Player");
    //     this.GetComponent<PlayerControler>().canPlay = true;
    // }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Tab)){
    //         ChangePlayer();
    //     }
        

    // }

    // public void ChangePlayer(){
    //        for(int i = 0; i < players.Length; i++){
    //             if(players[i].transform.position != this.transform.position){
    //                 cont++;
    //                 float disAtual = Vector2.Distance(players[i].transform.position,this.transform.position);
    //                 if(disAtual<disMin){    
    //                         playerProx = players[i];
    //                         disMin = disAtual;
    //                 }
                    
    //                 if(cont == players.Length-1){
    //                     playerProx.GetComponent<PlayerControler>().canPlay = true;
    //                     playerProx.GetComponent<PlayerManager>().enabled = true;
    //                     this.GetComponent<PlayerControler>().canPlay = false;
    //                     this.GetComponent<PlayerManager>().enabled = false;
    //                     cont = 0;
    //                     //this.GetComponent<PlayerManager>().enabled = false;
                        
    //                 }
                    
    //             }    
    //         }
    // }
}
