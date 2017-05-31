using System;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    public class DamageableController : CachedBehaviour, IDamageable
    {
        static Dictionary<Transform, IDamageable> AllDamageableObjects = new Dictionary<Transform, IDamageable>();

        /// <summary>
        /// Can be used: for visual and other effects; for body parts to send damage tp the body
        /// </summary>
        public event Action<int> OnDamageTaken;

        public static bool GetAliveControllerForTransform(Transform tr, out IDamageable damageable)
        {
            return AllDamageableObjects.TryGetValue(tr, out damageable);
        }

        protected void Start()
        {
            Add(myTransform, this);
        }

        static void Add(Transform tr, IDamageable item)
        {
            if (AllDamageableObjects.ContainsKey(tr))
            {
                Debug.LogError(item + "  element is already in All list");
            }
            else
                AllDamageableObjects.Add(tr, item);
        }

        public virtual void TakeDamage(int damage)
        {
            OnDamageTaken.Invoke(damage);
        }

        
    }

    public static class DamageableControllerExtensions
    {
        /* This method can be realized different ways.
         * 1) call transform.getcomponent
         * 
         * 2) my way:
         * - create static dictionary AliveController.All <Transform, AliveController>
         * - check if this dict contains hitted transform
        */
        public static bool TryGetDamageableComponent(this Transform tr, out IDamageable damageable)
        {
            return DamageableController.GetAliveControllerForTransform(tr, out damageable);
        }
    }
}