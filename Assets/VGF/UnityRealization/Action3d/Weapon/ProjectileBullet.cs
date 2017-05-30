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

        Vector3 TranslateVector;
        bool active;
        float curLifeTime;

        private void Start()
        {
            //GetComponent<Collider>().isTrigger = true;
        }

        // Update is called once per frame
        void Update()
        {
            //MoveIfActive();
        }

        void MoveIfActive()
        {
            if (!active)
                return;
            if (curLifeTime > Lifetime)
            {
                Deactivate();
                return;
            }
            float dt = Time.deltaTime;
            myTransform.position += myTransform.forward * Speed * dt;
            //myTransform.Translate(TranslateVector * dt);
            curLifeTime += dt;
        }

        
        void OnCollisionEnter(Collision collision)
        {
            TryGiveDamage(collision.transform);
            Deactivate();
        }
        

        private void OnTriggerEnter(Collider other)
        {
            TryGiveDamage(other.transform);
            Deactivate();
        }

        void Deactivate()
        {
            //myRigidbody.velocity = new Vector3()
            active = false;
            myGO.SetActive(false);
        }

        public override void Push(Vector3 startPosition, Quaternion startRotation)
        {
            myTransform.rotation = startRotation;
            myTransform.position = startPosition;
            
            myRigidbody.velocity = myTransform.forward * Speed;
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