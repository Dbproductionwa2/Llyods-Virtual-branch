using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class VirtualWorldManager : MonoBehaviourPunCallbacks
{

    public static VirtualWorldManager Instance;
    public TMP_InputField PlayerName_InputName;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this.gameObject);
            return;
        
        }
        Instance= this;
    }
    public void LeaveRoomAndLoadScene() 
    {
        PlayerPrefs.SetString("RoomID", PlayerName_InputName.text);
        Debug.Log("Player pref Room ID is =" + PlayerPrefs.GetString("RoomID"));
        PhotonNetwork.LeaveRoom();
    
    }


    #region Photon Callback Methods
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined to" + "Player count" + PhotonNetwork.CurrentRoom.PlayerCount);
    }


    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("Office");
    }
    #endregion
}
