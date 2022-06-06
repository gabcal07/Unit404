using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBarManager : MonoBehaviour
{
    public GameObject ennemyToGet;
    public float percentage;
    public Slider slid;
    public PhotonView view;
    float currH = 0;
    float prevH = 0;
    // Start is called before the first frame update
    void Start()
    {
        view = this.gameObject.GetComponent<PhotonView>();
        currH = ennemyToGet.GetComponent<Target>().health;
        prevH = ennemyToGet.GetComponent<Target>().health;
    }

    // Update is called once per frame
    void Update()
    {
        currH= ennemyToGet.GetComponent<Target>().health;
        if (currH != prevH)
        {
            modifperc();
            prevH = currH;
        }

    }

    public void modifperc()
    {

        float he = ennemyToGet.GetComponent<Target>().health;
        float maxHealth = ennemyToGet.GetComponent<Target>().maxHp;
        percentage = he / maxHealth;
        slid.value = percentage;
    }
}
