using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    public interface IWeaponController
    {
        void Init();
        void TryShoot();
        void Add(IWeaponModel model, bool setCurrent);
        //void Remove(IWeaponModel model);
        void RemoveCurrent();
        void RemoveAll();
        void SwitchNext();
    }
    
    //TODO: remake it as abstract class
    //[System.Serializable]
    public interface IWeaponModel
    {

        /// <summary>
        /// In Unity a WeaponModel is a ScriptableObject and needs to be instantiated.
        /// Maybe in other game engines there's something similar.
        /// So if you need some additional manipulations in the beginning, use this function
        /// </summary>
        /// <returns>Initialized weapon model</returns>
        IWeaponModel Init();
    }

    public interface IBulletProducer
    {

    }

    //public 
}