using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class RoomManager_Office : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI OccupancyRateText_ForCommonLobby;
    SpawnManager avatar;
   
    private string mapType;

    [SerializeField]
    GameObject GenericVRPlayerPrefab;

    public Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        avatar = FindObjectOfType<SpawnManager>();
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnectedAndReady)
        {

            PhotonNetwork.ConnectUsingSettings();

        }
        else 
        {
            PhotonNetwork.JoinLobby();
        
        }

        

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region UI Callback Method
    public void JoinRandomRoom()
    {

        Debug.Log("Here is the Room name to chekc before joining: "+PlayerPrefs.GetString("RoomID"));
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Office()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };


        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_Office;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
       // PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("RoomID"), roomOptions,TypedLobby.Default);
       // 

    }

    public void OnEnterButtonClicked_Commonlobby()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_common_lobby;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
        

    }

    #endregion

    #region Photom Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //Debug.Log(message + "hello its working");
        //CreateAndJoinRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to servers Again");
        PhotonNetwork.JoinLobby();
         Invoke("OnEnterButtonClicked_Office", 1);
      
     }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);
       // Debug.Log("tHE pHOTON sTATUS IS " + PhotonNetwork.IsConnectedAndReady);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The Local player: " + PhotonNetwork.NickName + " Joined to" + PhotonNetwork.CurrentRoom.Name + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {

            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType)) ;
            {
                Debug.Log("Joined room with the map: " + (string)mapType);
                if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_Office)
                {
                    PhotonNetwork.LoadLevel("Office");
                   // Debug.Log("tHE pHOTON sTATUS IS " + PhotonNetwork.IsConnectedAndReady);
                    //Invoke("callavatar", 3);
                }
                else if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_common_lobby)
                {
                    PhotonNetwork.LoadLevel("common_lobby");
                }

            }

        }

        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("tHE pHOTON sTATUS IS " + PhotonNetwork.IsConnectedAndReady);

            PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
        }
        //Debug.Log("tHE pHOTON sTATUS IS " + PhotonNetwork.IsConnectedAndReady);
    }

    public void callavatar()
    {
       


    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined to " + "Player count " + PhotonNetwork.CurrentRoom.PlayerCount);
       // Debug.Log("tHE pHOTON sTATUS IS " + PhotonNetwork.IsConnectedAndReady);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            OccupancyRateText_ForCommonLobby.text = 0 + " / " + 20;

        }
        int count = 1;
        
        Debug.Log("tHE ROOM LIST COUNT IS:  "+ roomList.Count);
        foreach (RoomInfo room in roomList)
        {
           // Debug.Log("Joined THE ROOMCOUNT");
            Debug.Log(room.Name);
            if (PlayerPrefs.GetString("RoomID") == room.Name) 
            {
              //  PhotonNetwork.JoinRoom(PlayerPrefs.GetString("RoomID"));
               // avatar.avaload();
                count = 0;
                break;
            }

            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_Office))
            {
                Debug.Log("Room is a school map. Player count is: " + room.PlayerCount);
                OccupancyRateText_ForCommonLobby.text = room.PlayerCount + " / " + 20;

            }
            Debug.Log("tHE pHOTON sTATUS IS " + PhotonNetwork.IsConnectedAndReady);
        }
       // if (count == 1)
        //CreateAndJoinRoom();
       
        //  Invoke("OnEnterButtonClicked_Office", 1);
        //Invoke("OnEnterButtonClicked_Office", 1);

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby");
      //  Debug.Log("tHE pHOTON sTATUS IS " + PhotonNetwork.IsConnectedAndReady);

    }

    #endregion

    #region private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = PlayerPrefs.GetString("RoomID");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };


        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

      //  Debug.Log("tHE pHOTON sTATUS IS " + PhotonNetwork.IsConnectedAndReady);
    }



    #endregion
}
