using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeaponIndex = 0;
    public AudioSource weaponSource;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isPause) { return; }

        int previousSelectedWeapon = selectedWeaponIndex;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeaponIndex >= transform.childCount - 1)
            {
                selectedWeaponIndex = 0;
            }
            else
                selectedWeaponIndex++;

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeaponIndex <= 0)
            {
                selectedWeaponIndex = transform.childCount-1;
            }
            else
                selectedWeaponIndex--;

        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
        {
            selectedWeaponIndex= 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeaponIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeaponIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeaponIndex = 3;
        }


        if (previousSelectedWeapon != selectedWeaponIndex)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        weaponSource.Play();
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeaponIndex)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
