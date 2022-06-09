using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitTheload : MonoBehaviour
{
    private bool loading = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (loading)
        {
            StartCoroutine(w());
        }
    }

    IEnumerator w()
    {
        yield return new WaitForSecondsRealtime(5f);
        this.gameObject.GetComponent<GameManager>().enabled = true;
        loading = !loading;
    }
}
