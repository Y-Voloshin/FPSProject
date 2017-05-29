using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    public class RaycastBulletProducer : AbstractBulletProducer
    {
        [SerializeField]
        RaycastBullet BulletPrefab;

        private void Start()
        {
            if (BulletPrefab == null)
            {
                Debug.Log("No bullet prefab in this bullet producer. Impossible to continue execution");
                Destroy(this);
                return;
            }

            BulletPrefab = Instantiate(BulletPrefab);
        }

        public override void Push()
        {
            BulletPrefab.Push(myTransform.position, myTransform.rotation);
        }
    }
}