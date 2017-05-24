/// <summary>
/// Voloshin Game Framework: basic scripts supposed to be reusable
/// </summary>
namespace VGF
{    
    [System.Serializable]
    public abstract class AbstractModel<T> where T : AbstractModel<T>, new()
    {
        /// <summary>
        /// Copy fields from target
        /// </summary>
        /// <param name="model">Source model</param>
        public abstract void SetValues(T model);
            
    }

    public static class AbstratModelMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">Target model, can be null</param>
        /// <param name="source">Source model</param>
        public static void InitializeWith<T>(this T model, T source) where T: AbstractModel<T>, new ()
        {
            //model = new T();
            if (source == null)
                return;
            model.SetValues(source);
        }
    }
}