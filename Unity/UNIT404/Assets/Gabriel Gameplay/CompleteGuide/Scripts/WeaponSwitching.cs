
using UnityEngine;
using Photon.Pun;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    PhotonView view;
    public bool Allactive;

    // Start is called before the first frame update
    void Start()
    {

            view = this.gameObject.GetComponent<PhotonView>();
            if (view.IsMine)
            {
            view.RPC("SelectWeapon", RpcTarget.All, selectedWeapon);

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (view != null && view.IsMine)
        {
            int previousSelectedWeapon = selectedWeapon;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                selectedWeapon = 0;
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
                selectedWeapon = 1;
            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
                selectedWeapon = 2;
            if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
                selectedWeapon = 3;
            //if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
            //selectedWeapon = 4;

            if (previousSelectedWeapon != selectedWeapon)
            {
                Debug.Log(selectedWeapon);
                view.RPC("SelectWeapon", RpcTarget.All, selectedWeapon);
                Debug.Log(selectedWeapon);
            }
        } 
    }
    [PunRPC]
    void SelectWeapon(int selected)
    {
        if (Allactive)
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
              weapon.gameObject.SetActive(true);
              weapon.GetComponent<PhotonTransformViewClassic>().enabled = true;

                i++;
            }

        }
        else 
        {

            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selected)
                {
                    Debug.Log(weapon.gameObject.name);

                    weapon.gameObject.SetActive(true);
                    weapon.GetComponent<PhotonTransformViewClassic>().enabled = true;


                }
                else
                {

                    weapon.GetComponent<PhotonTransformViewClassic>().enabled = false;
                    weapon.gameObject.SetActive(false);
                }
                   
                i++;
            } 
        }
                
    }
}

