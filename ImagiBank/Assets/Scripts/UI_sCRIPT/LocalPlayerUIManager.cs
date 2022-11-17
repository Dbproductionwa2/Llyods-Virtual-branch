using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject GoOffice_Button;

    private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("Authenticator (1)/Panel");
       
       
        GoOffice_Button = GameObject.Find("/Authenticator (1)/Panel/otpButton");
        GoOffice_Button.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadScene);


        //  GameObject panel = UICanvasGameobject.transform.GetChild(0).gameObject;
        Invoke("deactivate", 4);
    }

    public void deactivate() 
    {
        panel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
