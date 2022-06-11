using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().playerList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
