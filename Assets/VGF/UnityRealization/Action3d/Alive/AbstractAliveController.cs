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
        static Dictionary<Transform, AbstractAliveController> All = new Dictionary<Transform, AbstractAliveController>();
        public static bool GetAliveControllerForTransform(Transform tr, out AbstractAliveController aliveController)
        {
            return All.TryGetValue(tr, out aliveController);
        }

        DamageableController[] BodyParts;
        
        public bool IsAlive { get { return Immortal || CurrentModel.HealthCurrent > 0; } }
        public bool IsAvailable { get { return IsAlive && myGO.activeSelf; } }
        public virtual Vector3 Position { get { return myTransform.position; } }

        public static event Action<AbstractAliveController> OnDead;
        /// <summary>
        /// Sends the current health of this alive controller
        /// </summary>
        public event Action<int> OnDamaged;

        //TODO: create 2 inits
        protected override void Awake()
        {
            base.Awake();
            All.Add(myTransform, this);            
        }

        protected override void Init()
        {
            InitModel.Position = myTransform.position;
            InitModel.Rotation = myTransform.rotation;
            base.Init();

            
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
            myGO.SetActive(true);
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
            OnDamaged.CallEventIfNotNull(CurrentModel.HealthCurrent);
            if (CurrentModel.HealthCurrent <= 0)
            {
                OnDead.CallEventIfNotNull(this);
                Die();
            }
        }

        public int CurrentHealth
        {
            get { return CurrentModel.HealthCurrent; }
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