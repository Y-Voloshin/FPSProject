using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.Action3d.Weapon
{

    public class WeaponView : CachedBehaviour
    {
        AudioSource myAudio;
        [SerializeField]
        AudioClip ShotSound;
        
        // Use this for initialization
        void Start()
        {
            myAudio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Shoot()
        {
            PlaySound(ShotSound);
        }

        void PlaySound (AudioClip clip)
        {
            if (myAudio && clip != null)
                myAudio.PlayOneShot(clip);
        }
    }
}