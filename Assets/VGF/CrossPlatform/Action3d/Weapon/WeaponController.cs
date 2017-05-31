using System;
using System.Collections.Generic;

namespace VGF.Action3d.Weapon
{
    [Serializable]
    public abstract class WeaponController: IWeaponController
    {
        protected bool HasWeapon = false;
        
        protected List<IWeaponModel> Weapons;
        protected IWeaponModel CurrentWeaponInterface;//Cache current weapon for not asking the list every time
        protected int currentWeaponId;

        public virtual void Init()
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
            //TODO: and then? if setcurrent?
        }

        /// <summary>
        /// We do it on game start
        /// </summary>
        protected void SwitchFirst()
        {
            if (!HasWeapon)
                return;
            currentWeaponId = 0;
            CurrentWeaponInterface = Weapons[currentWeaponId];
            CacheCurrentWeapon();
        }

        public virtual void SwitchNext()
        {
            if (!HasWeapon)
                return;
            currentWeaponId++;
            if (currentWeaponId >= Weapons.Count)
                currentWeaponId = 0;
            CurrentWeaponInterface = Weapons[currentWeaponId];
            CacheCurrentWeapon();
        }

        public abstract void TryShoot();
        protected abstract void CacheCurrentWeapon();

        public void RemoveAll()
        {
            Weapons.Clear();
            CurrentWeaponInterface = null;
            HasWeapon = false;
            CacheCurrentWeapon();
        }

        public void RemoveCurrent()
        {
            throw new NotImplementedException();
        }
    }
}