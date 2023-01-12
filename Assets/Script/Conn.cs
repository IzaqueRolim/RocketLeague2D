using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class Conn : MonoBehaviourPunCallbacks
{

    public static Conn Instancia {get;set;}

    [SerializeField]
    private Text nick,res;
    [SerializeField]
    private GameObject panel1,panel2;
    
    public int indexCar,indexTime;
    
    
    void Awake(){
        //
        if (PlayerPrefs.HasKey("nome")){
            PhotonNetwork.NickName = PlayerPrefs.GetString("nome");
            panel2.SetActive(true);
        }
        else{
            panel1.SetActive(true);
        }
        
        indexCar = 0;
        indexTime = 0;

        Instancia = this;
        DontDestroyOnLoad(gameObject);

    }


    public void Play(Button btn){
        btn.interactable = false;

        PhotonNetwork.NickName = PlayerPrefs.GetString("nome");;
        PhotonNetwork.ConnectUsingSettings();

    }

    public void DeleteName(){
        PlayerPrefs.DeleteAll();
        panel2.SetActive(false);
        panel1.SetActive(true);
    }

    public void SaveName(){
        PlayerPrefs.SetString("nome",nick.text);
        

        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    


    //metodo para verificar se conectou ao servidor, é chamado quando se conecta
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinLobby();
        
        res.text = res.text+ "Conectado a rede\nProcurando partida...";
    }   


    //é chamado quando se desconecta
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause);
        res.text = "erro:"+cause;
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnJoinRandomFailed (short returnCode, string message){
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom("Room", roomOptions);
        

    }

    //é chamado quando entra na sala
    public async override void OnJoinedRoom(){
        if(PhotonNetwork.CurrentRoom.PlayerCount ==2){      
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("MudaTexto", RpcTarget.All);    
            await Task.Delay(1500);
            photonView.RPC("ComecaJogo", RpcTarget.All);    
        }
        else{
            res.text = res.text+ "\nEsperando outro jogador...";
        }
        
    }

    //Escolha do time e o carro
    public void SetIndex(int value){
        indexCar = value;   
    }
    public void SetIndexTime(int value){
        indexTime = value;   
    }

    [PunRPC]
    public void MudaTexto(){
        res.text = "Partida Encontrada!!!\nVai ja comecar";
    }

    [PunRPC]
    public void ComecaJogo(){
        PhotonNetwork.LoadLevel("Game");
    }
}
