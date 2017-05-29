using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    public abstract class AbstractAliveController : GenericModelBehaviour<AliveModelTransform>, IAlive
    {
        //TODO: create separate unity implementation where put all the [SerializeField] attributes
        [SerializeField]
        bool Immortal;
        [SerializeField]
        int HealthBasic,
            HealthCurrent;

        static Dictionary<Transform, AbstractAliveController> All = new Dictionary<Transform, AbstractAliveController>();
        public static bool GetAliveControllerForTransform(Transform tr, out AbstractAliveController aliveController)
        {
            return All.TryGetValue(tr, out aliveController);
        }

        DamageableController[] BodyParts;
        

        protected override void Init()
        {
            InitModel.Position = myTransform.position;
            InitModel.Rotation = myTransform.rotation;
            base.Init();

            All.Add(myTransform, this);
            BodyParts = GetComponentsInChildren<DamageableController>();
            foreach (var bp in BodyParts)
                bp.OnDamageTaken += TakeDamage;
        }

        protected override void Save()
        {
            CurrentModel.Position = myTransform.position;
            CurrentModel.Rotation = myTransform.rotation;
            base.Save();
        }

        protected override void Load()
        {
            base.Load();
            LoadTransform();
        }

        protected override void LoadInit()
        {
            base.LoadInit();
            LoadTransform();
        }

        void LoadTransform()
        {
            myTransform.position = CurrentModel.Position;
            myTransform.rotation = CurrentModel.Rotation;
        }

        public void Respawn()
        {
            LoadInit();
        }

        public void TakeDamage(int damage)
        {
            if (Immortal)
                return;
            CurrentModel.HealthCurrent -= damage;
            if (CurrentModel.HealthCurrent <= 0)
                Die();
        }

        protected abstract void Die();

    }

    public static class AliveControllerExtensions
    {
        /* This method can be realized different ways.
         * 1) call transform.getcomponent
         * 
         * 2) my way:
         * - create static dictionary AliveController.All <Transform, AliveController>
         * - check if this dict contains hitted transform
        */
        public static bool TryGetAliveComponent (this Transform tr, out AbstractAliveController alive)
        {
            return AbstractAliveController.GetAliveControllerForTransform(tr, out alive);
        }
    }
}