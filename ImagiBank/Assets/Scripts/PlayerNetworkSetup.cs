using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject LocalXRRigGameObject;
    // Start is called before the first frame update
    public GameObject MainAvatarGameobject;
    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;
    void Start()
    {


        if (photonView.IsMine)
        {
            //the player is local
            LocalXRRigGameObject.SetActive(true);

            SetLayerRecursively(AvatarHeadGameObject,6);
            SetLayerRecursively(AvatarBodyGameObject,7);
            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();

            if (teleportationAreas.Length > 0) 
            {
                Debug.Log("Found " + teleportationAreas.Length + " teleportation area. ");
                foreach (var item in teleportationAreas) 
                {
                    item.teleportationProvider = LocalXRRigGameObject.GetComponent<TeleportationProvider>();

                }
            
            }
            MainAvatarGameobject.AddComponent<AudioListener>();
        }
        else 
        {
            //the player is remote
            LocalXRRigGameObject.SetActive(false);
            SetLayerRecursively(AvatarHeadGameObject, 0);
            SetLayerRecursively(AvatarBodyGameObject, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }


}
