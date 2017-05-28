using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    [Serializable]
    public class AliveModelBasic : AbstractModel<AliveModelBasic>
    {
        public int HealthMax,
            HealthCurrent;

        public void SetValues(AliveModelBasic model)
        {
            HealthMax = model.HealthMax;
            HealthCurrent = model.HealthCurrent;
        }
    }
}
