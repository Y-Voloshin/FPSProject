using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d
{
    public interface IAlive 
    {
        void TakeDamage(int damage);
        void Respawn();
    }
}