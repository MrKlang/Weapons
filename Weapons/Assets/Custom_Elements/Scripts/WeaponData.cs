using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class WeaponData
{
    private Guid weaponGuid;
    public float damage;
    public string name;
    public AssetReferenceSprite weaponIcon;
    public AssetReferenceGameObject weaponPrefab;

    public Guid weaponId => weaponGuid;
    public WeaponData()
    {
        weaponGuid = Guid.NewGuid();
    }
}

[Serializable]
public class RangedWeaponData : WeaponData
{
    public int maxAmmoCapacity;
    public RangedWeaponTypes weaponType;
}

[Serializable]
public class MeleeWeaponData : WeaponData
{
    public int durability;
    public MeleeWeaponTypes weaponType;
}