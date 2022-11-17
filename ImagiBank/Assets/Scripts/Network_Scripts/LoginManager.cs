using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{

    string[] maleNames = { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };
    string[] femaleNames = { "abby", "abigail", "adele", "adrian" };
    string[] lastNames = { "abbott", "acosta", "adams", "adkins", "aguilar" };
    public TMP_InputField PlayerName_InputName;
    public int count = 0;
    SpawnManager loadava;
    RoomManager Joine;
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Joine = FindObjectOfType<RoomManager>();
        loadava = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetroomId() 
    {
        
        PlayerPrefs.SetString("RoomID", PlayerName_InputName.text);
        Debug.Log("Player pref Room ID is ="+PlayerPrefs.GetString("RoomID"));
    
    }
    #endregion

    #region UI Callback Methods
    public void ConnectAnonymously()
    {
     //   PhotonNetwork.ConnectUsingSettings();


    }

    public void ConnectToPhotonServer() 
    {
        if (PlayerName_InputName != null) 
        {
            PhotonNetwork.NickName = PlayerName_InputName.text;
            //  Debug.Log(PhotonNetwork.NickName);
            //PhotonNetwork.LoadLevel("Office");
           // PhotonNetwork.ConnectUsingSettings();
          

        }
    
    }
    #endregion
    #region Photon Callback Methods


    public override void OnConnected()
    {
        Debug.Log("OnConnected is called.The server is available");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.NickName = maleNames[Random.Range(0,maleNames.Length-1)];
       Debug.Log("Connected to Master Server!:User is connect to photon" + PhotonNetwork.NickName);
        
             
       // PhotonNetwork.LoadLevel("common_lobby");
        Joine.OnEnterButtonClicked_Commonlobby();
        //loadava.avaload();
    }
    #endregion
}
