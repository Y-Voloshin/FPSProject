using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    //TODO: think how to separate this from MonoBehaviour
    public abstract class AbstractBullet : CachedBehaviour
    {
        [SerializeField]
        int damage;

        public abstract void Push(Vector3 startPosition, Quaternion startRotation);


        public void Push(Vector3 startPosition, Quaternion startRotation, int damageValue)
        {
            damage = damageValue;
            Push(startPosition, startRotation);
        }


        protected void TryGiveDamage(Transform hittedTransform)
        {
            AbstractAliveController alive;
            if (hittedTransform.TryGetAliveComponent(out alive))
                alive.TakeDamage(damage);
        }

    }
}