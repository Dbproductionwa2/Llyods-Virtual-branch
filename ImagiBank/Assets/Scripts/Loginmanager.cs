using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
public class Loginmanager : MonoBehaviourPunCallbacks
{
    public TMP_InputField email;
    // Start is called before the first frame update

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnecttoPhotonServer()
    {
        if(email !=null)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.NickName = email.text;
        }
        
    }


    public override void OnConnected()
    {
        Debug.Log("Server is Available");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To the Master " + PhotonNetwork.NickName);
        //SceneManager.LoadScene("Office");

        JoinRoom();
    }

    private void JoinRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(PhotonNetwork.NickName, roomOptions, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("New Room is created successfully"+PhotonNetwork.CurrentRoom.Name);
        
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Creation Failed");
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Office");
        Debug.Log("joined in the Existing Room"+PhotonNetwork.CurrentRoom.Name);
    }
}
