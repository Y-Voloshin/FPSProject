using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Action3d.Level;

namespace VGF.Action3d.NPC
{   

    /// <summary>
    /// Abstract class, parent of any NPC behaviour
    /// </summary>
    public abstract class NPCController : CachedBehaviour, INPCController
    {
        protected Dictionary<NPCState, AbstractNPCStrategy> Strategies;

        [SerializeField]
        protected NPCState currentState;
        /// <summary>
        /// You can pick up first state in inspector or set it in SetFirtState method
        /// </summary>
        [SerializeField]        
        bool SetFirstStateFromCode = true;

        bool strategyForCurrentStateExists = false;
        [SerializeField]
        NPCModel model;

        // Use this for initialization
        void Start()
        {
            if (model == null)
                model = new NPCModel(this);
            else
                model.Init(this);

            CreateStrategies();
            if (SetFirstStateFromCode)
                SetFirstState();
            SwitchState(currentState);
        }
        // Update is called once per frame
        void Update()
        {
            //Caching pos in model so it's easy to get it multiple times per frame
            model.CurrentPoition = myTransform.position;

            if (strategyForCurrentStateExists)
                Strategies[currentState].Update();
            
        }

        /// <summary>
        /// Hardcode here preferable first state for an NPC
        /// </summary>
        protected virtual void SetFirstState()
        {
            currentState = NPCState.RandomWalk;
        }

        protected abstract void CreateStrategies();

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
            Strategies.Add(strategyState, AbstractNPCStrategy.CreateStrategy<T>(this, model, finishedState, failedState, deadState));
        }

        void SwitchState(NPCState state)
        {
            currentState = state;
            strategyForCurrentStateExists = Strategies != null
                && Strategies.Count > 0
                && Strategies.ContainsKey(currentState)
                && Strategies[currentState] != null;
            if (strategyForCurrentStateExists)
                Strategies[currentState].Start();
        }


       public void SwitchState(StrategyEventArgs previousStrategyEventArgs = null)
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

        public void GoToRandomPoint()
        {
            if (myNavMeshAgent)
                myNavMeshAgent.SetDestination(LevelBoundsBehaviour.GetPointWithinBounds(myTransform.position, model.RandomWalkRange));
        }

        public bool HasPath()
        {
            return myNavMeshAgent.hasPath;
        }
    }
}