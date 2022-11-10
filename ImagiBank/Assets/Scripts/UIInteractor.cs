using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInteractor : MonoBehaviour
{
    private bool isActive = false;
    [SerializeField] private GameObject authenticator,horse;
    [SerializeField] private Button OtpAuth;

    private void Start()
    {
       // OtpAuth.onClick.AddListener(OtpAuthVerify);
    }
    public void OnEnable()
    {
        Debug.Log("Inside");
        if(!isActive)
        {
            Debug.Log(!isActive);
            authenticator.SetActive(false);
            isActive = true;
        }
        else
        {
            authenticator.SetActive(true);
;           isActive = false;
        }
    }
    public void OTPRequest()
    {
        
    }
    private void OtpAuthVerify()
    {
        horse.SetActive(false);
    }
}
