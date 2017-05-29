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
        WeaponModel CurrentWeaponModel;
        //[SerializeField]
        AbstractBulletProducer BulletProducer;
        Transform OwnerTransform;


        public void Init(Transform ownerTransform)
        {
            OwnerTransform = ownerTransform;
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
                Weapons.Add(w);
            
            base.Init();
        }

        public override void TryShoot()
        {
            if (CurrentWeaponModel == null)
                return;
            if (BulletProducer == null)
                return;
            BulletProducer.Push();

        }

        protected override void CacheCurrentWeapon()
        {
            CurrentWeaponModel = CurrentWeaponInterface as WeaponModel;
            if (CurrentWeaponModel == null)
                BulletProducer = null;
            else
            {
                BulletProducer = CurrentWeaponModel.BulletProducer;
                var bpt = BulletProducer.transform;
                if (OwnerTransform != null)
                    bpt.SetParent(OwnerTransform);
                bpt.localRotation = Quaternion.identity;
                bpt.localPosition = Vector3.zero;
            }
        }
    }

}