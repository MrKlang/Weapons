using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataLibrary", menuName = "ScriptableObjects/WeaponDataLibrary", order = 1)]
public class WeaponsDataLibrary : ScriptableObject
{
    [SerializeField] private List<RangedWeaponData> _rangedWeaponsDataList;
    [SerializeField] private List<MeleeWeaponData> _meleeWeaponsDataList;

    public bool IsWeaponRanged(Guid id, string name = null)
    {
        if (id.Equals(Guid.Empty))
        {
            return _rangedWeaponsDataList.Any(e => e.name.Equals(name));
        }
        else
        {
            return _rangedWeaponsDataList.Any(e => e.weaponId.Equals(id));
        }
    }

    public WeaponData GetMeleeWeaponData(Guid id, string name = null)
    {
        if (id.Equals(Guid.Empty))
        {
            return _meleeWeaponsDataList.First(e => e.name.Equals(name));
        }
        else
        {
            return _meleeWeaponsDataList.First(e => e.weaponId.Equals(id));
        }
    }
    public WeaponData GetRangedWeaponData(Guid id, string name = null)
    {
        if (id.Equals(Guid.Empty))
        {
            return _rangedWeaponsDataList.First(e => e.name.Equals(name));
        }
        else
        {
            return _rangedWeaponsDataList.First(e => e.weaponId.Equals(id));
        }
    }
}
