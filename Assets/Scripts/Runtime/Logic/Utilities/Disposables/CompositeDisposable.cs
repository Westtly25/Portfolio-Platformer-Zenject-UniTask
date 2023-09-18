using System;
using System.Collections.Generic;

namespace Scripts.Utilities.Disposables
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> disposables = new();

        public void Retain(IDisposable disposable) =>
            disposables.Add(disposable);

        public void Dispose()
        {
            for (int i = 0; i < disposables.Count; i++)
            {
                IDisposable disposable = disposables[i];
                disposable.Dispose();
            }

            disposables.Clear();
        }
    }
}