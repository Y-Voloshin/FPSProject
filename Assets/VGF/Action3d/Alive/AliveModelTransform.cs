using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    [System.Serializable]
    public class AliveModelTransform : AliveModelBasic, AbstractModel<AliveModelTransform>
    {
        [HideInInspector]
        public Vector3 Position;
        [HideInInspector]
        public Quaternion Rotation;

        public void SetValues(AliveModelTransform model)
        {
            Position = model.Position;
            Rotation = model.Rotation;
            base.SetValues(model);
        }
    }
}