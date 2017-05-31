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
        [SerializeField]
        Transform WeaponViewPivot;

        protected override void InitNPCModel()
        {
            base.InitNPCModel();
            if (Weapon != null)
                Weapon.Init(myTransform, WeaponViewPivot);
        }

        public override void InteractWithTarget()
        {
            throw new NotImplementedException();
        }

        protected override void CreateStrategies()
        {
            CreateStrategy<RandomWalkStrategy>(NPCState.RandomWalk);
            CreateStrategy<DeadStrategySwitchOff>(NPCState.Dead);
        }

        protected override void SetFirstState()
        {
            currentState = NPCState.RandomWalk;
        }

        protected override void Die()
        {
            currentState = NPCState.Dead;
            //Debug.Log("Die");
            //Stop();
        }
    }
}