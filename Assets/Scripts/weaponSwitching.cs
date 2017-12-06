using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitching : MonoBehaviour {

    public int slectedWeapon = 0;
	// Use this for initialization
	void Start () {
        selectWeapon();
	}
	
	// Update is called once per frame
	void Update () {
        int prevWeapon = slectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (slectedWeapon >= transform.childCount - 1)
            {
                slectedWeapon = 0;
            }
            else
            {
                slectedWeapon++;
            }

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (slectedWeapon <= 0)
            {
                slectedWeapon = transform.childCount - 1;
            }
            else
            {
                slectedWeapon--;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            slectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            slectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            slectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            slectedWeapon = 3;
        }
        if (prevWeapon != slectedWeapon)
        {
            selectWeapon();
        }
    }
    void selectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == slectedWeapon)
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
