using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;

public class AudioController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private List<AudioClip> torcidas;
    
    public AudioSource source;
    public AudioSource source2;

    PhotonView pv = new PhotonView();

    int indexLocal,cont;

    void Start(){
        cont = 0;
        pv = GetComponent<PhotonView>();
        
        source.volume = 0.4f;
        pv.RPC("SortearCancao",RpcTarget.All);
        
    }

    void Update(){ 
        if(source.time >= source.clip.length){
            source.Stop();
            pv.RPC("SortearCancao",RpcTarget.All);          
        }
    }

    public void golTorcida(){
        pv.RPC("RPC_golTorcida",RpcTarget.All);   
    }

    public void tensaoTorcida(){
        pv.RPC("RPC_tensaoTorcida",RpcTarget.All);
    }

    [PunRPC]
    void SortearCancao(){
        indexLocal = Random.Range(0,4);

        source.clip = torcidas[indexLocal+Conn.Instancia.indexTime];
        source.Play();  
    }
    [PunRPC]
    void RPC_tensaoTorcida(){
        source.volume = 0.3f;
    }
    [PunRPC]
    async void RPC_golTorcida(){
        source2.Play();
        source.Stop();
        await Task.Delay(5000);
        SortearCancao();
        source.volume = 1.0f;
    }

    
    
}
