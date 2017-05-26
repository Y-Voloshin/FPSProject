using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.NPC
{
    [System.Serializable]
    public class NPCModel
    {
        public float RandomWalkRange = 30;
        
        public float DetectionDistance = 10;
        public float DetectionAngle = 45;
        #region
        //public Transform myTransform;
        [HideInInspector]
        public UnityEngine.AI.NavMeshAgent NavMeshAgent;
        [HideInInspector]
        public Vector3 CurrentPoition;
        #endregion

        
        public NPCModel(MonoBehaviour controller)
        {
            Init(controller);
        }

        public void Init(MonoBehaviour controller)
        {
            NavMeshAgent = controller.GetComponent<UnityEngine.AI.NavMeshAgent>();
            //myTransform = controller.GetComponent<Transform>();
            CurrentPoition = controller.GetComponent<Transform>().position;
        }
    }

    /// <summary>
    /// State of NPC for strategy selection
    /// </summary>
    public enum NPCState
    {
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
}