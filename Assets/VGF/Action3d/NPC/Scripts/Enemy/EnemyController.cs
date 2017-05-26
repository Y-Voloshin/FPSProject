using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Action3d.NPC.Common;

namespace VGF.Action3d.NPC.Enemy
{
    public class EnemyController : NPCController
    {
        protected override void CreateStrategies()
        {
            CreateStrategy<RandomWalkStrategy>(NPCState.RandomWalk);
        }

        protected override void SetFirstState()
        {
            currentState = NPCState.RandomWalk;
        }
    }
}