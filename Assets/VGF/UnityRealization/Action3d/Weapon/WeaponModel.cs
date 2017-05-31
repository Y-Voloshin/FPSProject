using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{
    [CreateAssetMenu(menuName = "Weapon")]
    //[System.Serializable]
    public class WeaponModel : ScriptableObject, IWeaponModel
    {
        //[SerializeField]
        public AbstractBulletProducer BulletProducer;
        public WeaponView ViewPrefab;
        public IWeaponModel Init()
        {
            var result = Instantiate(this);
            result.BulletProducer = Instantiate(BulletProducer);
            return result;
        }

        public WeaponView GetView()
        {            
            return ViewPrefab? Instantiate(ViewPrefab) : null;
        }
    }
}