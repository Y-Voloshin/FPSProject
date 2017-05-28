using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    [Serializable]
    public class WeaponController: IWeaponController
    {
        bool HasWeapon = false;

        [SerializeField]
        List<IWeaponModel> Weapons;
        IWeaponModel CurrentWeapon;//Cache current weapon for not asking the list every time
        int currentWeaponId;

        public void Init()
        {
            HasWeapon = Weapons != null && Weapons.Count > 0 && Weapons[0] != null;

            if (HasWeapon)
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapons[i] = Weapons[i].Init();
                }

            SwitchNext();
        }

        public void Add(IWeaponModel model, bool setCurrent = true)
        {
            if (!HasWeapon)
            {
                setCurrent = true;
                HasWeapon = true;
            }
        }

        /// <summary>
        /// We do it on game start
        /// </summary>
        void SwitchFirst()
        {
            if (!HasWeapon)
                return;
            currentWeaponId = 0;
            CurrentWeapon = Weapons[currentWeaponId];
        }

        public void SwitchNext()
        {
            if (!HasWeapon)
                return;
            currentWeaponId++;
            if (currentWeaponId >= Weapons.Count)
                currentWeaponId = 0;
            CurrentWeapon = Weapons[currentWeaponId];
        }

        public void TryShoot()
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            Weapons.Clear();
            CurrentWeapon = null;
            HasWeapon = false;
        }

        public void RemoveCurrent()
        {
            throw new NotImplementedException();
        }
    }
}