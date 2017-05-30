using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.NPC.Common
{
    public class DeadStrategySwitchOff : AbstractNPCStrategy
    {
        protected override void SpecifiedInit(NPCModel actorModel)
        {
            //throw new NotImplementedException();
        }

        protected override void StartLogic(StrategyEventArgs previousStrategyEventArgs = null)
        {
            //throw new NotImplementedException();
        }

        protected override void UpdateLogic()
        {
            (ownerNPC as MonoBehaviour).gameObject.SetActive(false);
        }
    }
}
