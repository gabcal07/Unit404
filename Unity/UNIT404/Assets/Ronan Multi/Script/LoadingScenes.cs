using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class LoadingScenes : MonoBehaviour
{
    public GameObject LoadingUi;
    public TMP_Text lod;
    private bool doo = true;
    // Start is called before the first frame update
    void Start()
    {
        lod.text = "Loading";
    }

    // Update is called once per frame
    void Update()
    {
        if (doo)
        {
            lod.text = next(lod.text);
            doo = false;
            StartCoroutine(wait());

        }
        
    }

    public string next(string s)
    {
        if (s == "Loading")
        {
            return "Loading.";
        }
        if(s== "Loading.")
        {
            return "Loading..";

        }
        if (s == "Loading..")
        {
            return "Loading...";
        }
        else
        {
            return "Loading";
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        doo = true;
    }
}
