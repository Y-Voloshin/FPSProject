namespace VGF.Action3d.NPC.Common
{
    public class RandomWalkStrategy : AbstractNPCStrategy
    {
        protected override void StartLogic (StrategyEventArgs previousStrategyEventArgs = null)
        {
            //TODO: think and refactor.
            //1) components in model - bad
            //2) SetDestionation is a very frequent and reusable method, better put in in controller
            //3) think about gving a strategy access to controller methods
            // which menas no prvacy? no way, event handlers can be better
            //It woul be more clear after realization of several other strategies

            ownerNPC.GoToRandomPoint();
        }

        protected override void UpdateLogic()
        {
            if (ownerNPC.SeeTarget())
            {
                ownerNPC.SwitchState(finishedArgs);
                return;
            }
            //Put checck enemy visibility here
            // also a common stuff
            if (!ownerNPC.HasPath())
            {
                ownerNPC.GoToRandomPoint();
            }
        }
    }
}
