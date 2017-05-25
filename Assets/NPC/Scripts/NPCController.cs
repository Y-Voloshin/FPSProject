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
        protected Dictionary<NPCState, >

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}