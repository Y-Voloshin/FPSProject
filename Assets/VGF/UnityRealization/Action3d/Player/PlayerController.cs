using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Action3d.Weapon;

namespace VGF.Action3d
{
    public class PlayerController : AbstractAliveController
    {        
        [SerializeField]
        WeaponControllerUnity Weapon;
        [SerializeField]
        Transform BulletProducerPivot,
            WeaponViewPivot;

        // Use this for initialization
        void Start()
        {
            if (Weapon != null)
                Weapon.Init(BulletProducerPivot, WeaponViewPivot);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                if (Weapon != null)
                    Weapon.TryShoot();
        }

        protected override void Die()
        {
            Debug.Log("Die : " + name);
        }
    }
}