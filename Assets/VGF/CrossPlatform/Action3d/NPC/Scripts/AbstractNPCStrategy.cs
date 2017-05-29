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
        //public event Action<StrategyEventArgs> OnFinished;        

        /// <summary>
        /// args of successful task complete.
        /// </summary>
        protected StrategyEventArgs finishedArgs = StrategyEventArgs.SimpleIdle;
        /// <summary>
        /// args of task fail. For example, actor tries to attack a target which is lost or dead.
        /// </summary>
        protected StrategyEventArgs failedArgs = StrategyEventArgs.SimpleIdle;
        /// <summary>
        /// args of actor's death
        /// </summary>
        protected StrategyEventArgs deadArgs = StrategyEventArgs.SimpleDead;

        protected INPCController ownerNPC;
        protected bool SomeRequiredDataAreAbsent = false;

        public AbstractNPCStrategy()
        {

        }

        public AbstractNPCStrategy(INPCController controller, NPCModel actorModel,
            NPCState finishedState = NPCState.Idle, NPCState failedState = NPCState.Idle, NPCState deadState = NPCState.Dead)
        {
            Init(controller, actorModel, finishedState, failedState, deadState);
        }

        public static AbstractNPCStrategy CreateStrategy<T>
            (INPCController controller, NPCModel actorModel,
            NPCState finishedState = NPCState.Idle, NPCState failedState = NPCState.Idle, NPCState deadState = NPCState.Dead) 
            where T: AbstractNPCStrategy, new ()
        {
            var result = new T();
            result.Init(controller, actorModel, finishedState, failedState, deadState);
            return result;
        }

        

        /// <summary>
        /// Init function should be used in constructor
        /// </summary>
        /// <param name="actorModel"></param>
        /// <param name="actorEventHandler"></param>
        /// <param name="finishedState"></param>
        /// <param name="failedState"></param>
        /// <param name="deadState"></param>
        protected void Init(INPCController controller, NPCModel actorModel,
            NPCState finishedState = NPCState.Idle, NPCState failedState = NPCState.Idle, NPCState deadState = NPCState.Dead)
        {
            //No necessarry to create new StrategyEventArgs instancess for same values.
            // So override only if desired value are special
            // DANGER!!! don't forget to replace with new

            //if (finishedState != NPCState.Idle)
            ownerNPC = controller;
            if (ownerNPC == null)
                SomeRequiredDataAreAbsent = true;

            finishedArgs = new StrategyEventArgs { NextState = finishedState };

            failedArgs = new StrategyEventArgs { NextState = failedState };
            deadArgs = new StrategyEventArgs { NextState = deadState };
            
            SpecifiedInit(actorModel);
        }
        
        public void Start(StrategyEventArgs previousStrategyEventArgs = null)
        {
            if (SomeRequiredDataAreAbsent)
                StartFailLogic(previousStrategyEventArgs);
            else
                StartLogic(previousStrategyEventArgs);
        }

        public void Update()
        {
            if (SomeRequiredDataAreAbsent)
                UpdateFailLogic();
            else
                UpdateLogic();
        }

        protected abstract void SpecifiedInit(NPCModel actorModel);        
        protected abstract void StartLogic(StrategyEventArgs previousStrategyEventArgs = null);
        protected abstract void UpdateLogic();
        protected virtual void StartFailLogic(StrategyEventArgs previousStrategyEventArgs = null)
        {
            ownerNPC.SwitchState(StrategyEventArgs.SimpleIdle);
        }
        protected virtual void UpdateFailLogic()
        {
            ownerNPC.SwitchState(StrategyEventArgs.SimpleIdle);
        }

        protected void CallOnFinished_Finished()
        {
            ownerNPC.SwitchState(finishedArgs);
        }

        protected void CallOnFinished_Dead()
        {
            ownerNPC.SwitchState(deadArgs);
        }

        protected void CallOnFinished_Failed()
        {
            ownerNPC.SwitchState(failedArgs);
        }
    }

    public class StrategyEventArgs
    {
        public static readonly StrategyEventArgs SimpleIdle = new StrategyEventArgs() { NextState = NPCState.Idle };
        public static readonly StrategyEventArgs SimpleDead = new StrategyEventArgs() { NextState = NPCState.Dead };

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
