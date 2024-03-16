using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerWeaponEquipment : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _playerInputs;
    [SerializeField] private string _weaponCollisionTag;
    [SerializeField] private Transform _weaponHolder; // (right hand)

    private int _currentlyEquippedWeaponIndex = 0;
    private List<WeaponData> _weaponsInEquipment = new List<WeaponData>();

    public void Start()
    {
        MainController.OnUpdate += CheckForPlayerInput;
    }

    private void OnDestroy()
    {
        MainController.OnUpdate -= CheckForPlayerInput;
    }

    private void CheckForPlayerInput()
    {
        if(_weaponsInEquipment.Count <= 0)
        {
            return;
        }

        if (_playerInputs.useWeapon)
        {
            _playerInputs.useWeapon = false; //ensure we only attack ONCE and not X times (where X is number of frames per second)
            Attack();
        }

        if (_playerInputs.changeWeapon)
        {
            ChangeActiveWeapon();
            Debug.Log($"Changing weapon to {_weaponsInEquipment[_currentlyEquippedWeaponIndex].name}");
            MainController.GetInstance().UpdateWeaponUI(_weaponsInEquipment[_currentlyEquippedWeaponIndex]);
        }
    }

    private void ChangeActiveWeapon()
    {
        GameObject currentlyUsedObject = _weaponHolder.GetChild(_currentlyEquippedWeaponIndex).gameObject;
        currentlyUsedObject.SetActive(false);
        _currentlyEquippedWeaponIndex = _currentlyEquippedWeaponIndex + 1 < _weaponsInEquipment.Count ? _currentlyEquippedWeaponIndex + 1 : 0; //looping incrementation
        currentlyUsedObject = _weaponHolder.GetChild(_currentlyEquippedWeaponIndex).gameObject;
        currentlyUsedObject.SetActive(true);
        currentlyUsedObject.transform.localPosition = Vector3.zero;
        _playerInputs.changeWeapon = false; //ensure we only change weapon ONCE and not X times (where X is number of frames per second)
    }

    private void Attack()
    {
        GameObject currentlyUsedObject = _weaponHolder.GetChild(_currentlyEquippedWeaponIndex).gameObject;
        if (!currentlyUsedObject.activeInHierarchy)
        {
            currentlyUsedObject.SetActive(true);
        }

        if (MainController.GetInstance().WeaponsLibrary.IsWeaponRanged(_weaponsInEquipment[_currentlyEquippedWeaponIndex].weaponId))
        {
            AttackWithRangedWeapon();
        }
        else
        {
            AttackWithMeleeWeapon();
        }
    }

    private void AttackWithMeleeWeapon()
    {
        Debug.Log("Using melee weapon");
    }

    private void AttackWithRangedWeapon()
    {
        Debug.Log("Using ranged weapon");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_weaponCollisionTag))
        {
            Debug.Log($"Picked up {other.name}");
            if (MainController.GetInstance().WeaponsLibrary.IsWeaponRanged(Guid.Empty, other.name))
            {
                WeaponData currentlyCollectedWeapon = MainController.GetInstance().WeaponsLibrary.GetRangedWeaponData(Guid.Empty, other.name);
                _weaponsInEquipment.Add(currentlyCollectedWeapon);

                if (_weaponsInEquipment.Count.Equals(1)) //Only if this is a first weapon
                {
                    AsyncOperationHandle<GameObject> weaponHandler = currentlyCollectedWeapon.weaponPrefab.InstantiateAsync(Vector3.zero, Quaternion.identity, _weaponHolder);
                    weaponHandler.Completed += OnFirstWeaponLoaded;
                }
                else
                {
                    currentlyCollectedWeapon.weaponPrefab.InstantiateAsync(Vector3.zero, Quaternion.identity, _weaponHolder);
                }
            }
            else
            {
                WeaponData currentlyCollectedWeapon = MainController.GetInstance().WeaponsLibrary.GetMeleeWeaponData(Guid.Empty, other.name);
                _weaponsInEquipment.Add(currentlyCollectedWeapon);

                if (_weaponsInEquipment.Count.Equals(1)) //Only if this is a first weapon
                {
                    AsyncOperationHandle<GameObject> weaponHandler = currentlyCollectedWeapon.weaponPrefab.InstantiateAsync(Vector3.zero, Quaternion.identity, _weaponHolder);
                    weaponHandler.Completed += OnFirstWeaponLoaded;
                }
                else
                {
                    currentlyCollectedWeapon.weaponPrefab.InstantiateAsync(Vector3.zero, Quaternion.identity, _weaponHolder);
                }
            }

            Destroy(other.gameObject);
        }
    }

    private void OnFirstWeaponLoaded(AsyncOperationHandle<GameObject> weaponHandler)
    {
        GameObject currentlyUsedObject = _weaponHolder.GetChild(_currentlyEquippedWeaponIndex).gameObject;
        currentlyUsedObject.SetActive(true);
        currentlyUsedObject.transform.localPosition = Vector3.zero;

        MainController.GetInstance().UpdateWeaponUI(_weaponsInEquipment[_currentlyEquippedWeaponIndex]);
    }

}
