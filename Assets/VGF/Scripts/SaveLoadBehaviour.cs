using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF
{
    /* Why abstract class instead of interface?
     * 1) Incapsulate all save, load, init, loadinit functions inside class, make them protected, mnot public
     * 2) Create static ALL collection and static ALL methods
     * */
     //TODO: create a similar abstract class for non-mono classes. For example, PlayerController needs not to be a MonoBehaviour
    /// <summary>
    /// Abstract class for all MonoBehaiour classes that support save and load
    /// </summary>
    public abstract class SaveLoadBehaviour : CachedBehaviour
    {
        /// <summary>
        /// Collection that stores all SaveLoad classes in purpose of providing auto registration and collective save and load
        /// </summary>
        static List<SaveLoadBehaviour> All = new List<SaveLoadBehaviour>();

        protected override void Awake()
        {
            base.Awake();
            Add(this);
        }

        static void Add(SaveLoadBehaviour item)
        {
            if (All.Contains(item))
            {
                Debug.LogError(item + "  element is already in All list");
            }
            else
                All.Add(item);
        }

        public static void LoadAll()
        {
            foreach (var item in All)
            {
                if (item == null)
                {
                    Debug.LogError("empty element in All list");
                    continue;
                }
                else
                    item.Load();
            }
        }

        public static void SaveAll()
        {
            Debug.Log(All.Count);
            foreach (var item in All)
            {
                if (item == null)
                {
                    Debug.LogError("empty element in All list");
                    continue;
                }
                else
                    item.Save();
            }
        }

        public static void LoadInitAll()
        {
            foreach (var item in All)
            {
                if (item == null)
                {
                    Debug.LogError("empty element in All list");
                    continue;
                }
                else
                    item.LoadInit();
            }
        }

        protected abstract void Save();

        protected abstract void Load();

        protected abstract void Init();

        protected abstract void LoadInit();
    }
}

