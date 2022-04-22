using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera FirstCam;
    [SerializeField] private CinemachineVirtualCamera SecondCam;

    [SerializeField] private bool skyMode; //par default setté à False
    public GameObject[] frame;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        FirstCam.enabled = !skyMode; //true
        //SecondCam.enabled = skyMode; //false
        frame[0].SetActive(!skyMode);
        frame[1].SetActive(skyMode);
    }

    // Update is called once per frame
    async void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && i!=0)
        {
            skyMode = !skyMode;
            FirstCam.gameObject.SetActive(!skyMode);
            SecondCam.gameObject.SetActive(skyMode);

            frame[1].SetActive(skyMode);
            await Task.Delay(2000);
            frame[0].SetActive(!skyMode);
            i -= 1;
        }
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && i ==0)
        {
            skyMode = !skyMode;
            FirstCam.gameObject.SetActive(!skyMode);
            SecondCam.gameObject.SetActive(skyMode);
            
            frame[0].SetActive(!skyMode);
            await Task.Delay(2000);
            frame[1].SetActive(skyMode);
            i += 1;

        }
    }
}
