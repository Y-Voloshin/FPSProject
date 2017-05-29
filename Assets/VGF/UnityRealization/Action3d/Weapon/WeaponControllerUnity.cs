using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    [System.Serializable]
    public class WeaponControllerUnity : WeaponController
    {
        /// <summary>
        /// Add weapon models here in inspector, and in init they will be copied to Weapons
        /// </summary>
        [SerializeField]
        List<WeaponModel> WeaponsForInspector;

        public override void Init()
        {
            if (WeaponsForInspector != null)
                Weapons = new List<IWeaponModel>();
            foreach (var w in WeaponsForInspector)
                Weapons.Add(w);
            base.Init();
        }
    }

}