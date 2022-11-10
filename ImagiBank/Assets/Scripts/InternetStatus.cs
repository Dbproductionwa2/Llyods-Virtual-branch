using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//This script is for checking Internet connection 
public class InternetStatus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckInternetStatus());
    }

    IEnumerator CheckInternetStatus()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if(request.error!=null)
        {
            Debug.Log("Connected to the internet");

        }
        else
        {
            Debug.Log("Not Connected to the internet");
        }
    }
    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            Debug.Log("Connected");
        }
    }
}
