using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachSpeaker : MonoBehaviour
{
    [SerializeField]
    GameObject speaker;

    int count = 0;
    public GameObject xrrig;
    // Start is called before the first frame update
    void Start()
    {
      
        //object1 is now the child of object2
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 0)
        {
            speaker = GameObject.Find("Generic VR Player(Clone)");
            if (speaker.transform.GetChild(2).gameObject != null)
            {
                speaker = speaker.transform.GetChild(2).gameObject;
                count = 1;
                speaker.transform.parent = xrrig.transform;
            }
            Debug.Log("Speaker has been set to XRrig");
        }
    }
}
