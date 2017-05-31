namespace VGF.Action3d.NPC
{
    //This interface allows to write engine-independent strategies.
    //All the pathfinding, hide/destroy/respawn, Vector3 and any other engine-driven code can be hiddent from strategy.
    /// <summary>
    /// Interface that an abstract NPC controller should implement.
    /// </summary>
    public 
        interface INPCController
    {
        //void Init();
        void GoToRandomPoint();
        void GoToRandomPointNextToTarget();
        void GoToPoint(UnityEngine.Vector3 point);
        void LookAtTarget();
        void SwitchState(StrategyEventArgs previousStrategyEventArgs);
        bool HasPath();
        void Stop();
        void InteractWithTarget();
        bool SeeTarget();
        //bool TargetIsAvailable { get; }
        //bool Die();
    }
}
