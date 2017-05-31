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
        List<WeaponView> WeaponViews = new List<WeaponView>();
        WeaponModel CurrentWeaponModel;
        WeaponView CurrentWeaponView;
        //[SerializeField]
        AbstractBulletProducer BulletProducer;
        Transform OwnerTransform,
            ViewOwnerTransform;
        [SerializeField]
        bool UnlimitedShootSpeed;

        public void Init(Transform ownerTransform, Transform viewOwnerTransform)
        {
            OwnerTransform = ownerTransform;
            ViewOwnerTransform = viewOwnerTransform;
            Init();
        }


        /// <summary>
        /// Don't use this, use Init(Transform ownerTransform) instead
        /// </summary>
        public override void Init()
        {
            if (WeaponsForInspector != null)
                Weapons = new List<IWeaponModel>();
            foreach (var w in WeaponsForInspector)
            {
                Weapons.Add(w);
                var v = w.GetView();
                if (v)
                {
                    WeaponViews.Add(v);
                    if (ViewOwnerTransform)
                    {
                        v.OrientAndAttach(ViewOwnerTransform);
                    }
                    v.Hide();
                }
            }
            
            base.Init();
        }

        public override void TryShoot()
        {
            if (CurrentWeaponModel == null)
                return;
            if (BulletProducer == null)
                return;

            if (UnlimitedShootSpeed ||
                (Time.realtimeSinceStartup - CurrentWeaponModel.LastShotTime >= CurrentWeaponModel.ShootCooldown))
                Shoot();
        }

        void Shoot()
        {
            BulletProducer.Push();
            CurrentWeaponModel.LastShotTime = Time.realtimeSinceStartup;
            if (CurrentWeaponView)
                CurrentWeaponView.Shoot();
        }

        protected override void CacheCurrentWeapon()
        {
            CurrentWeaponModel = CurrentWeaponInterface as WeaponModel;            
            if (CurrentWeaponModel == null)
                BulletProducer = null;
            else
            {
                BulletProducer = CurrentWeaponModel.BulletProducer;
                BulletProducer.OrientAndAttach(OwnerTransform);
            }

            //TODO: I use it every time, refactor
            if (CurrentWeaponView)
                CurrentWeaponView.Hide();
            if (WeaponViews != null && WeaponViews.Count > currentWeaponId)
                CurrentWeaponView = WeaponViews[currentWeaponId];
            if (CurrentWeaponView)
                CurrentWeaponView.Show();
        }
    }

}