using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonConnection : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnected()
    {
        Debug.Log("Connected to the server");
    }
}
