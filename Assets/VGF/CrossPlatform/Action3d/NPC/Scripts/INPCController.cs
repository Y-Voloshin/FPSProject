using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.NPC
{
    //This interface allows to write engine-independent strategies.
    //All the pathfinding, hide/destroy/respawn, Vector3 and any other engine-driven code can be hiddent from strategy.
    /// <summary>
    /// Interface that an abstract NPC controller should implement.
    /// </summary>
    public interface INPCController
    {
        //void Init();
        void GoToRandomPoint();
        void SwitchState(StrategyEventArgs previousStrategyEventArgs);
        bool HasPath();
        void Stop();
        void InteractWithTarget();
        bool SeeTarget();
        //bool Die();
    }
}
