using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI OccupancyRateText_ForCommonLobby;
    
    private string mapType;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsConnectedAndReady) 
        {

            PhotonNetwork.JoinLobby();

        }

        //Invoke("OnEnterButtonClicked_Commonlobby", 3);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Method
    public void JoinRandomRoom() 
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Office() 
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_Office;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);

        

      

    }

    public void OnEnterButtonClicked_Commonlobby()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_common_lobby;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties,0);
        Debug.Log("Entered");
    }

    #endregion

    #region Photom Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message+"hello its working");
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The Local player: "+ PhotonNetwork.NickName+ " Joined to"+ PhotonNetwork.CurrentRoom.Name+ " Player count"+ PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY)) 
        {
            
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType));
            {
                Debug.Log("Joined room with the map: " + (string)mapType);
                if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_Office)
                {
                    PhotonNetwork.LoadLevel("Office");
                }
                else if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_common_lobby) 
                {
                    PhotonNetwork.LoadLevel("common_lobby");
                }

            }
        
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined to" + "Player count" + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count ==0) 
        {
            OccupancyRateText_ForCommonLobby.text = 0 + " / " + 20;
        
        }

        foreach (RoomInfo room in roomList) 
        {
            Debug.Log("Joined THE ROOMCOUNT");
            Debug.Log(room.Name);

            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_common_lobby)) 
            {
                Debug.Log("Room is a school map. Player count is: " + room.PlayerCount);
                OccupancyRateText_ForCommonLobby.text = room.PlayerCount + " / " + 20;
            
            }
        
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby");
    }

    #endregion

    #region private Methods
    private void CreateAndJoinRoom() 
    {
        string randomRoomName = "Common_"+ mapType + Random.Range(0,10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };


        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    
    }



    #endregion
}
