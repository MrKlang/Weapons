using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataLibrary", menuName = "ScriptableObjects/WeaponDataLibrary", order = 1)]
public class WeaponsDataLibrary : ScriptableObject
{
    [SerializeField] private List<RangedWeaponData> _rangedWeaponsDataList;
    [SerializeField] private List<MeleeWeaponData> _meleeWeaponsDataList;

    public bool IsWeaponRanged(Guid id)
    {
        return _rangedWeaponsDataList.Any(e => e.weaponId.Equals(id));
    }

    public WeaponData GetMeleeWeaponData(Guid id)
    {
        return _meleeWeaponsDataList.First(e => e.weaponId.Equals(id));
    }
    public WeaponData GetRangedWeaponData(Guid id)
    {
        return _rangedWeaponsDataList.First(e => e.weaponId.Equals(id));
    }
}
