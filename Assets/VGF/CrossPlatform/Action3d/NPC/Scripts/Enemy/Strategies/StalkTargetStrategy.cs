using UnityEngine;

namespace VGF.Action3d.NPC.Common
{
    public class StalkTargetStrategy : AbstractNPCStrategy
    {
        Vector3 TargetLastSeenPosition;
        Transform target;
        AbstractAliveController targetAlive;

        protected override void StartLogic(StrategyEventArgs previousStrategyEventArgs = null)
        {
            //TODO: think and refactor.
            //1) components in model - bad
            //2) SetDestionation is a very frequent and reusable method, better put in in controller
            //3) think about gving a strategy access to controller methods
            // which menas no prvacy? no way, event handlers can be better
            //It woul be more clear after realization of several other strategies

            if (previousStrategyEventArgs != null)
                ownerNPC.GoToPoint(previousStrategyEventArgs.PointOfInterest);
            else
            {
                SomeRequiredDataAreAbsent = true;
                ownerNPC.SwitchState(failedArgs);
                return;
            }
        }

        protected override void UpdateLogic()
        {
            /*
            if (!targetAlive.IsAlive) //Target is dead, stop searching
            {
                //failedArgs.NextState 
                ownerNPC.SwitchState(failedArgs);
                return;
            }
            */
            if (ownerNPC.SeeTarget()) //Target detected, start interacting
            {
                ownerNPC.SwitchState(finishedArgs);
                return;
            }
            if (!ownerNPC.HasPath()) //Target not found, stop searching
                ownerNPC.SwitchState(failedArgs);
        }
    }
}