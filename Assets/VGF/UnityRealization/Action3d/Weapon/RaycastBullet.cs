using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    public class RaycastBullet : AbstractBullet
    {
        [SerializeField]
        float maxDistance = 50;

        public override void Push(Vector3 startPosition, Quaternion startRotation)
        {
            RaycastHit hit;
            if (Physics.Raycast(startPosition, myTransform.forward, out hit, maxDistance))
                TryGiveDamage(hit.transform);
        }
    }
}