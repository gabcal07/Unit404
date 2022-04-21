
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 3;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
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
            SelectWeapon();
            Debug.Log(selectedWeapon);
        }
        
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                Debug.Log(weapon.gameObject.name);
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
