using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    public abstract class AbstractAliveController : GenericModelBehaviour<AliveModelTransform>, IAlive
    {
        [SerializeField]
        bool Immortal;
        [SerializeField]
        int HealthBasic,
            HealthCurrent;

        protected override void Init()
        {
            InitModel.Position = myTransform.position;
            InitModel.Rotation = myTransform.rotation;
            base.Init();
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
}