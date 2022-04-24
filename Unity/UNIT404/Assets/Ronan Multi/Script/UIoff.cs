using Photon.Pun;
using UnityEngine;

public class UIoff : MonoBehaviour
{
    public GameObject UI;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = this.gameObject.GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            UI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
