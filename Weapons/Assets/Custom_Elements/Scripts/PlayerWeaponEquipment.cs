using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponEquipment : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _playerInputs;

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
        if (_playerInputs.useWeapon)
        {
            //if (MainController.GetInstance().WeaponsLibrary.IsWeaponRanged(Guid here))
            //{
            //}
            //else
            //{
            //}
            Debug.LogError("Using weapon");
        }

        if (_playerInputs.changeWeapon)
        {
            Debug.LogError("Changing weapon");
        }
    }

}
