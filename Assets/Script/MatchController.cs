using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MatchController : MonoBehaviourPunCallbacks
{
    public static MatchController Instancia {get;set;}

    
    
    [SerializeField]
    public Text txtCasa,txtFora,placarCasa,placarFora;
    [SerializeField]
    private GameObject[] jogador,ball;


    public static PhotonView photon;

    Player player;

    void Start(){

        photon = GetComponent<PhotonView>();
        photon.RPC("SetNames", RpcTarget.All);

        Inst();
    }

    

    [PunRPC]
    void SetNames(){
        if(PhotonNetwork.PlayerList[0].NickName != null){
            txtCasa.text = PhotonNetwork.PlayerList[0].NickName;
        }
        else{
            txtCasa.text = "Player"+Random.Range(0,1000).ToString();

        }
       // txtFora.text = PhotonNetwork.PlayerList[1].NickName;
    }
   
   

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        if(changedProps.ContainsKey("golCasa")){
            if(target.CustomProperties.TryGetValue("golCasa",out object golCasa)){        
                placarCasa.text = golCasa.ToString();
            }
        }
        else if(changedProps.ContainsKey("golFora")){
            if(target.CustomProperties.TryGetValue("golFora",out object golFora)){        
                placarFora.text = golFora.ToString();
            }
        }
        
    }

   

    void Inst(){

        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.Instantiate(jogador[Conn.Instancia.indexCar].name,new Vector3(-6,-16,0),Quaternion.identity);
            PhotonNetwork.Instantiate(ball[0].name,new Vector3(0,-16,0),Quaternion.identity);
        }
        else{
            PhotonNetwork.Instantiate(jogador[Conn.Instancia.indexCar].name,new Vector3(6,-16,0),Quaternion.identity);   
        }
        
    }
    

    
}
