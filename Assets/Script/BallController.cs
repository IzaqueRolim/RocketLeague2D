using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviourPunCallbacks
{    
    int golCasa,golFora;

    public bool casaGanhando = false;

    PhotonView pv = new PhotonView();

    AudioController audio = new AudioController();
    

    void Start(){
        golCasa = 0;
        golFora = 0;
        pv = GetComponent<PhotonView>();
        
        
    }

    private async void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "trave1"){
            this.GetComponent<CircleCollider2D>().enabled = false;
            
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            this.transform.position = new Vector3(0,-16,0);
            this.GetComponent<CircleCollider2D>().enabled = true;
            pv.RPC("GoalFora",RpcTarget.MasterClient);
        }
        else if(collider.gameObject.tag == "trave2"){
            collider.gameObject.transform.parent.GetComponent<AudioController>().golTorcida();
            await Task.Delay(3000);
            this.GetComponent<CircleCollider2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            this.transform.position = new Vector3(0,-16,0);
            this.GetComponent<CircleCollider2D>().enabled = true;
            pv.RPC("GoalCasa",RpcTarget.MasterClient);
        }
        else if(collider.gameObject.tag == "grandeArea"){
            print("OI");
            collider.gameObject.transform.parent.GetComponent<AudioController>().tensaoTorcida();
        }
    }

    [PunRPC]
    void GoalCasa(){
        golCasa++;
        print(golCasa);
            
        Hashtable hash = new Hashtable();
        hash.Add("golCasa",golCasa);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
    [PunRPC]
    void GoalFora(){
        golFora++;
            
        Hashtable hash = new Hashtable();
        hash.Add("golFora",golFora);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    
    
}
