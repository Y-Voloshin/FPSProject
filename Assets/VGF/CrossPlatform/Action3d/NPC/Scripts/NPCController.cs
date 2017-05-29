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
    public abstract class NPCController : AbstractAliveController, INPCController
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
        NPCModel npcModel;

        #region some settings, put them somewhere else
        float detectDistance = 15, 
            detectAngle = 50, 
            detectAnywayDistance = 5;
        #endregion
        Transform TargetTransform;

        // Use this for initialization
        void Start()
        {
            InitNPCModel();
            TargetTransform = GameObject.FindGameObjectWithTag("Player").transform;
            //Debug.Log(TargetTransform);
        }
        // Update is called once per frame
        void Update()
        {
            //Caching pos in model so it's easy to get it multiple times per frame
            npcModel.CurrentPosition = myTransform.position;

            if (strategyForCurrentStateExists)
                Strategies[currentState].Update();            
        }

        protected virtual void InitNPCModel()
        {
            if (npcModel == null)
                npcModel = new NPCModel(this);
            else
                npcModel.Init(this);

            CreateStrategies();
            if (SetFirstStateFromCode)
                SetFirstState();
            SwitchState(currentState);
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
            Strategies.Add(strategyState, AbstractNPCStrategy.CreateStrategy<T>(this, npcModel, finishedState, failedState, deadState));
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
            Debug.Log(currentState);

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
                myNavMeshAgent.SetDestination(LevelBoundsBehaviour.GetPointWithinBounds(myTransform.position, myTransform.forward, 0.5f, npcModel.RandomWalkRange));
        }

        public bool HasPath()
        {
            return myNavMeshAgent && myNavMeshAgent.hasPath;
        }

        public bool SeeTarget()
        {
            if (TargetTransform == null)
                return false;
            Vector3 tPos = TargetTransform.position;
            Vector3 vectorToTarget = tPos - npcModel.CurrentPosition;
            //tPos += vectorToTarget.normalized * 3;
            float dist = Vector3.Distance(tPos, npcModel.CurrentPosition);
            if (dist <= detectAnywayDistance
                || (dist <= detectDistance && Vector3.Angle(myTransform.forward, vectorToTarget) <= detectAngle))
            {
                RaycastHit hit;

                //Magic code: just cast ray from one unit away from center
                //TODO: fix, use LayerMask
                if (Physics.Raycast(npcModel.CurrentPosition + vectorToTarget.normalized, vectorToTarget, out hit, dist))
                {
                    if (hit.transform.name == "LegTop")
                        Debug.DrawRay(npcModel.CurrentPosition, vectorToTarget, Color.red, 20f);


                    Debug.Log(hit.transform);
                    return hit.transform == TargetTransform;
                }
            }

            return false;
        }

        public void Stop()
        {
            if (myNavMeshAgent)
                myNavMeshAgent.ResetPath();
        }

        public abstract void InteractWithTarget();
    }
}