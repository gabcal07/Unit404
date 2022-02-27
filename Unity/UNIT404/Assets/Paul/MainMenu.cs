using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera FirstCam;
    [SerializeField] private CinemachineVirtualCamera SecondCam;

    [SerializeField] private bool skyMode; //par default setté à False
    public GameObject[] frame;

    // Start is called before the first frame update
    void Start()
    {
        FirstCam.enabled = !skyMode; //true
        //SecondCam.enabled = skyMode; //false
        frame[0].SetActive(!skyMode);
        frame[1].SetActive(skyMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            skyMode = !skyMode;
            Debug.Log("switch cam Mode " + skyMode);
            FirstCam.gameObject.SetActive(!skyMode);
            SecondCam.gameObject.SetActive(skyMode);
            frame[0].SetActive(!skyMode);
            frame[1].SetActive(skyMode);

        }
    }
}
