using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitching : MonoBehaviour {

    public int selectedMainWeapon = 0;
	// Use this for initialization
	void Start () {
        selectWeapon();
	}
	
	// Update is called once per frame
	void Update () {
        int prevWeapon = selectedMainWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedMainWeapon >= transform.childCount - 1)
            {
                selectedMainWeapon = 0;
            }
            else
            {
                selectedMainWeapon++;
            }

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedMainWeapon <= 0)
            {
                selectedMainWeapon = transform.childCount - 1;
            }
            else
            {
                selectedMainWeapon--;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            selectedMainWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedMainWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedMainWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedMainWeapon = 3;
        }
        if (prevWeapon != selectedMainWeapon)
        {
            selectWeapon();
        }
    }
    public void selectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedMainWeapon)
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
