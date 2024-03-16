using System;
using UnityEngine;

public class MainController : GenericSingleton<MainController>
{
    [SerializeField] private WeaponsDataLibrary _weaponsLibrary;
    [SerializeField] private MainUIController _mainUIController;

    public static Action OnUpdate;

    public WeaponsDataLibrary WeaponsLibrary => _weaponsLibrary;

    private void Awake()
    {
        OnAwake();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    public void UpdateWeaponUI(WeaponData weapon)
    {
        _mainUIController.UpdateWeaponsPanel(weapon.name, weapon.weaponIcon);
    }
}
