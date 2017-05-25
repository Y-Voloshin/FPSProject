using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.NPC
{
    /// <summary>
    /// State of NPC for strategy selection
    /// </summary>
    public enum NPCState {
        /// <summary>
        /// NPC has nothing to do
        /// </summary>
        Idle,
        /// <summary>
        /// 
        /// </summary>
        RandomWalk,
        /// <summary>
        /// 
        /// </summary>
        Patrol,
        /// <summary>
        /// 
        /// </summary>
        GoToPoint,
        /// <summary>
        /// 
        /// </summary>
        SearchForTarget,        
        /// <summary>
        /// 
        /// </summary>
        InteractWithTarget,
        /// <summary>
        /// 
        /// </summary>
        Dead
    }

    public abstract class NPCController : CachedBehaviour
    {
        protected Dictionary<NPCState, AbstractNPCStrategy> Strategies;
        NPCState currentState;
        bool strategyForCurrentStateExists = false;
        NPCModel model;

        // Use this for initialization
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {
            if (strategyForCurrentStateExists)
                Strategies[currentState].UpdateExecute();
        }

        protected void CreateStrategy<T>(NPCState strategyState,
            NPCState finishedState = NPCState.Idle,
            NPCState failedState = NPCState.Idle,
            NPCState deadState = NPCState.Dead)
            where T: AbstractNPCStrategy, new ()
        {
            if (Strategies == null)
                Strategies = new Dictionary<NPCState, AbstractNPCStrategy>();
            else
            {
                if (Strategies.ContainsKey(strategyState))
                {
                    Debug.LogError("A strategy for this state already exists. No multiple strategies for a state allowed.");
                    return;
                }
            }
            Strategies.Add(strategyState, AbstractNPCStrategy.CreateStrategy<T>(model, SwitchState, finishedState, failedState, deadState));
        }

        void SwitchState(NPCState state)
        {
            currentState = state;
            strategyForCurrentStateExists = Strategies != null
                && Strategies.Count > 0
                && Strategies.ContainsKey(currentState)
                && Strategies[currentState] != null;
            if (strategyForCurrentStateExists)
                Strategies[currentState].Start(null);
        }


       void SwitchState(StrategyEventArgs previousStrategyEventArgs = null)
        {
            currentState = previousStrategyEventArgs == null ?
                NPCState.Idle : previousStrategyEventArgs.NextState;
            strategyForCurrentStateExists = Strategies != null
                && Strategies.Count > 0
                && Strategies.ContainsKey(currentState)
                && Strategies[currentState] != null;
            if (strategyForCurrentStateExists)
                Strategies[currentState].Start(previousStrategyEventArgs);
        }
    }
}