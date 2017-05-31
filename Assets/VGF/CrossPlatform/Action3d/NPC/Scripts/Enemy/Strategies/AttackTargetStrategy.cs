using UnityEngine;

namespace VGF.Action3d.NPC.Common
{
    public class AttackTargetStrategy : AbstractNPCStrategy
    {
        Vector3 TargetLastSeenPosition;
        ITarget target;

        protected override void StartLogic(StrategyEventArgs previousStrategyEventArgs = null)
        {
            //TODO: think and refactor.
            //1) components in model - bad
            //2) SetDestionation is a very frequent and reusable method, better put in in controller
            //3) think about gving a strategy access to controller methods
            // which menas no prvacy? no way, event handlers can be better
            //It woul be more clear after realization of several other strategies

            if (previousStrategyEventArgs != null)
                target = previousStrategyEventArgs.objectToDealWith;
            else
                target = null;

            if (target == null)
            {
                SomeRequiredDataAreAbsent = true;
                return;
            }
            TargetLastSeenPosition = target.Position;
        }

        protected override void UpdateLogic()
        {
            if (!target.IsAvailable)
            {
                ownerNPC.SwitchState(finishedArgs);
                return;
            }
            if (!ownerNPC.SeeTarget())
            {
                failedArgs.PointOfInterest = TargetLastSeenPosition;
                ownerNPC.SwitchState(failedArgs);
                return;
            }
            ownerNPC.InteractWithTarget();
        }
    }
}