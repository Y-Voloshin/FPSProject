using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Action3d.NPC.Common;
using VGF.Action3d.Weapon;

namespace VGF.Action3d.NPC.Enemy
{
    public class EnemyController : NPCController
    {
        [SerializeField]
        WeaponControllerUnity Weapon;
        protected override void InitNPCModel()
        {
            base.InitNPCModel();
            Weapon.Init();
        }

        public override void InteractWithTarget()
        {
            throw new NotImplementedException();
        }

        protected override void CreateStrategies()
        {
            CreateStrategy<RandomWalkStrategy>(NPCState.RandomWalk);
        }

        protected override void SetFirstState()
        {
            currentState = NPCState.RandomWalk;
        }

        protected override void Die()
        {
            currentState = NPCState.Dead;
            Debug.Log("Die");
            Stop();
        }
    }
}