using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    public class ProjectileBullet : AbstractBullet
    {
        [SerializeField]
        float Speed = 4,
            Lifetime = 5;

        Vector3 currentForward;
        bool active;
        float curLifeTime;

        // Update is called once per frame
        void Update()
        {
            if (!active)
                return;
            if (curLifeTime > Lifetime)
            {
                Deactivate();
                return;
            }
            float dt = Time.deltaTime;
            myTransform.Translate(currentForward * dt);
            curLifeTime += dt;
        }

        void OnCollisionEnter(Collision collision)
        {
            TryGiveDamage(collision.transform);
            Deactivate();
        }

        void Deactivate()
        {
            active = false;
            myGO.SetActive(false);
        }

        public override void Push(Vector3 startPosition, Quaternion startRotation)
        {
            myTransform.position = startPosition;
            myTransform.rotation = startRotation;
            currentForward = myTransform.forward * Speed;
            active = true;
            curLifeTime = 0;
            myGO.SetActive(true);
        }

        public bool IsActive()
        {
            return active;
        }
    }
}