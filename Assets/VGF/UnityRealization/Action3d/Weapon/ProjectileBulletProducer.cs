using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    public class ProjectileBulletProducer : AbstractBulletProducer
    {
        static Transform ProjectileStorage;
        List<ProjectileBullet> ActiveBullets = new List<ProjectileBullet>();
        Queue<ProjectileBullet> InactiveBullets = new Queue<ProjectileBullet>();

        [SerializeField]
        ProjectileBullet BulletPrefab;
        ProjectileBullet curBullet;

        // Use this for initialization
        void Start()
        {
            if(BulletPrefab == null)
            {
                Debug.Log("No bullet prefab in this bullet producer. Impossible to continue execution");
                Destroy(this);
                return;
            }

            if (ProjectileStorage == null)
            {
                ProjectileStorage = new GameObject("ProjectileStorage").transform;
            }
            //myTransform.SetParent(ProjectileStorage)
        }

        // Update is called once per frame
        void Update()
        {
            for(int i = ActiveBullets.Count -1; i >-1; i--)
                if (!ActiveBullets[i].IsActive())
                {
                    InactiveBullets.Enqueue(ActiveBullets[i]);
                    ActiveBullets.RemoveAt(i);
                }
        }

        public ProjectileBullet CreateBullet()
        {
            return Instantiate(BulletPrefab, ProjectileStorage);
        }

        public override void Push()
        {
            ProjectileBullet b = null;
            while (b == null && InactiveBullets.Count > 0)
                b = InactiveBullets.Dequeue();
            if (b == null)
                b = CreateBullet();

            ActiveBullets.Add(b);
            b.Push(myTransform.position, myTransform.rotation);
        }
    }
}