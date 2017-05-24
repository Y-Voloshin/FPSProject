using UnityEngine;
using System;
using System.Collections.Generic;

namespace VGF
{
    /// <summary>
    /// Controller for abstract models, providing save, load, reset model
    /// </summary>
    /// <typeparam name="T">AbstractModel child type</typeparam>
    public class GenericModelBehaviour<T> : SaveLoadBehaviour where T : AbstractModel<T>, new()
    {
        [SerializeField]
        protected T InitModel;
        [SerializeField]
        protected T CurrentModel, SavedModel;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        protected override void Init()
        {
            //Debug.Log(InitModel);
            if (InitModel == null)
                return;
            //Debug.Log(gameObject.name + " : Init current model");
            if (CurrentModel == null)
                CurrentModel = new T();
            CurrentModel.InitializeWith(InitModel);
            //Debug.Log(CurrentModel);
            //Debug.Log("Init saved model");
            SavedModel = new T();
            SavedModel.InitializeWith(InitModel);
        }

        protected override void Load()
        {
            //Debug.Log(gameObject.name + "   saved");
            LoadFrom(SavedModel);
        }

        protected override void LoadInit()
        {
            LoadFrom(InitModel);
        }

        void LoadFrom(T source)
        {
            if (source == null)
                return;
            CurrentModel.SetValues(source);
        }

        protected override void Save()
        {
            //Debug.Log(gameObject.name + "   saved");
            if (CurrentModel == null)
                return;
            if (SavedModel == null)
                SavedModel.InitializeWith(CurrentModel);
            else
                SavedModel.SetValues(CurrentModel);
        }
    }
}