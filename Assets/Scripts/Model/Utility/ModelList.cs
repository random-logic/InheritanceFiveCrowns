using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace InheritanceFiveCrowns
{
    public class ModelList<T> : IEnumerable
    {
        protected internal List<T> Container;

        public ModelList(int size = 0) => Container = new List<T>(size);

        protected internal Action<T> Add => Container.Add;
        protected internal Func<T, bool> Remove => Container.Remove;
        protected internal Action<int> RemoveAt => Container.RemoveAt;

        #region Getters

        public Func<T, int> IndexOf => Container.IndexOf;
        public int Count => Container.Count;
        public T At(int index) => Container[index];
        public Func<T, bool> Contains => Container.Contains;

        public IEnumerator GetEnumerator() => ((IEnumerable)Container).GetEnumerator();
        #endregion
    }
}