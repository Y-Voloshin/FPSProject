using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    public class WeaponModel : ScriptableObject, IWeaponModel
    {
        public IWeaponModel Init()
        {
            return Instantiate(this);
        }
    }
}