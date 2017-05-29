using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    public abstract class AbstractBulletProducer : CachedBehaviour
    {
        public abstract void Push();
    }
}