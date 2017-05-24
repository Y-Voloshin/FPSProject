using System;

namespace VGF
{
    public static class EventExtensions
    {
        public static void CallEventIfNotNull(this Action a)
        {
            if (a != null)
                a();
        }

        public static void CallEventIfNotNull<T>(this Action<T> a, T arg)
        {
            if (a != null)
                a(arg);
        }
    }
}