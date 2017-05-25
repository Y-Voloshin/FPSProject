using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.NPC
{
    public abstract class AbstractNPCStrategy
    {
        /// <summary>
        /// Event of strategy execution finished. Args describe reason of finishing and consequences of execution
        /// </summary>
        public event Action<StrategyEventArgs> OnFinished;

        /// <summary>
        /// args of successful task complete.
        /// </summary>
        StrategyEventArgs finishedArgs;
        /// <summary>
        /// args of task fail. For example, actor tries to attack a target which is lost or dead.
        /// </summary>
        StrategyEventArgs failedArgs;
        /// <summary>
        /// args of actor's death
        /// </summary>
        StrategyEventArgs deadArgs;

        public void Init(NPCModel actorModel, Action<StrategyEventArgs> actorEventHandler,
            NPCState finishedState = NPCState.Idle, NPCState failedState = NPCState.Idle, NPCState deadState = NPCState.Dead)
        {
            finishedArgs = new StrategyEventArgs { NextState = finishedState };
            failedArgs = new StrategyEventArgs { NextState = failedState };
            deadArgs = new StrategyEventArgs { NextState = deadState };

            OnFinished += actorEventHandler;

            SpecifiedInit(actorModel);
        }

        protected abstract void SpecifiedInit(NPCModel actorModel);
        protected abstract void Start(StrategyEventArgs previousStrategyEventArgs = null);
        protected abstract void UpdateExecute();
    }

    public class StrategyEventArgs
    {
        /// <summary>
        /// State to switch the actor to
        /// </summary>
        public NPCState NextState;
        /// <summary>
        /// Point in world used for next strategy (if needed)
        /// </summary>
        public Vector3 PointOfInterest;
        /// <summary>
        /// gameobct transform used for next strategy (if needed)
        /// </summary>
        public Transform objectToDealWith;
    }
}
