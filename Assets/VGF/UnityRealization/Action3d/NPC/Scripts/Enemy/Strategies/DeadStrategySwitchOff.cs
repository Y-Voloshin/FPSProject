using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.NPC.Common
{
    public class DeadStrategySwitchOff : AbstractNPCStrategy
    {

        protected override void StartLogic(StrategyEventArgs previousStrategyEventArgs = null)
        {
            (ownerNPC as MonoBehaviour).gameObject.SetActive(false);
        }

        protected override void UpdateLogic()
        {
            //(ownerNPC as MonoBehaviour).gameObject.SetActive(false);
            Debug.Log("Dead strategy update instead of switching off");
        }
    }
}
