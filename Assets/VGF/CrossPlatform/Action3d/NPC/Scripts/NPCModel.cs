using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.NPC
{
    [System.Serializable]
    public class NPCModel
    {
        public float RandomWalkRange = 30;
        
        public float DetectionDistance = 25;
        public float DetectionAngle = 50;
        public float DetectAnywayDistance = 10;
        #region
        [HideInInspector]
        public Vector3 CurrentPosition;
        #endregion

        public NPCModel() { }
        
        public NPCModel(MonoBehaviour controller)
        {
            Init(controller);
        }

        public void Init(MonoBehaviour controller)
        {
            CurrentPosition = controller.GetComponent<Transform>().position;
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