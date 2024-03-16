using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initWeaponPrefabs;
    [SerializeField] Transform[] weaponSlots;
    [SerializeField] Transform defaultWeaponSlot;
    int currentWeaponIndex = -1;
    List<Weapon> weapons;
    private void Start()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        weapons = new List<Weapon>();
        foreach (Weapon weapon in initWeaponPrefabs)
        {
            Transform weaponSlot = defaultWeaponSlot;
            foreach (Transform slot in weaponSlots)
            {
                if(slot.gameObject.tag == weapon.GetAttachSlotTag())
                {
                    weaponSlot = slot;
                    Debug.Log($"Found slot {slot.gameObject.name} for weapon {weapon.gameObject.name}");
                } 
            }
            Weapon newWeapon = Instantiate(weapon, weaponSlot);
            newWeapon.Init(this.gameObject);
            weapons.Add(newWeapon);
        }
        NextWeapon();
    }

    public void NextWeapon()
    {
        int nextWeaponIndex=currentWeaponIndex+1;
        if (nextWeaponIndex >= weaponSlots.Length)
        {
            nextWeaponIndex = 0;
        }
        EquipWeapon(nextWeaponIndex);
    }

    private void EquipWeapon(int weaponIndex)
    {
        if(weaponIndex < 0 || weaponIndex >= weaponSlots.Length)
        {
            Debug.Log($"Weapon index {weaponIndex} is out of range");
            return;
        }

        if (currentWeaponIndex>= 0 && currentWeaponIndex < weaponSlots.Length)
        {
            weapons[currentWeaponIndex].Unequip();
        }
        weapons[weaponIndex].Equip();
        currentWeaponIndex = weaponIndex;
    }

    internal Weapon GetCurrentWeapon()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weaponSlots.Length)
        {
            return weapons[currentWeaponIndex];
        }
        return null;
    }
}
