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
        Transform WeaponViewPivot,
            BulletProducerPivot;

        /*
        AbstractAliveController targetAliveController;
        public override bool TargetIsAvailable
        {
            get
            {
                if (TargetTransform.)
            }
        }
        */
        #region strategy event args
        StrategyEventArgs InteractArgs;        
        #endregion



        protected override void InitNPCModel()
        {
            base.InitNPCModel();
            if (Weapon != null)
                Weapon.Init(BulletProducerPivot, WeaponViewPivot);
        }

        public override void InteractWithTarget()
        {
            if (TargetIsAwayFromHoldDistance())
                myNavMeshAgent.ResetPath();
            if (!myNavMeshAgent.hasPath)
                GoToRandomPointNextToTarget();
            LookAtTarget();
            Weapon.TryShoot();
        }

        protected override void Init()
        {
            TargetTransform = GameObject.FindGameObjectWithTag("Player").transform;

            AbstractAliveController targ;
            TargetTransform.TryGetAliveComponent(out targ);            
            InteractArgs = new StrategyEventArgs(NPCState.InteractWithTarget, targ);

            base.Init();

            
        }

        protected override void CreateStrategies()
        {
            CreateStrategy<RandomWalkStrategy>(NPCState.RandomWalk, InteractArgs);
            CreateStrategy<DeadStrategySwitchOff>(NPCState.Dead);
            CreateStrategy<AttackTargetStrategy>(NPCState.InteractWithTarget, StrategyEventArgs.SimpleIdle, new StrategyEventArgs(NPCState.SearchForTarget));
            CreateStrategy<StalkTargetStrategy>(NPCState.SearchForTarget, InteractArgs, new StrategyEventArgs(NPCState.RandomWalk));
        }


        protected override void SetFirstState()
        {
            currentState = NPCState.RandomWalk;
        }

        protected bool TargetIsAwayFromHoldDistance()
        {
            return npcModel.HoldTargetDistance < Vector3.Distance(npcModel.CurrentPosition, TargetTransform.position);
        }

        protected override void Die()
        {
            //currentState = NPCState.Dead;
            //Debug.Log("Die");
            //Stop();
            SwitchState(StrategyEventArgs.SimpleDead);
        }
    }
}