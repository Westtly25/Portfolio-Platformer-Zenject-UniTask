using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Utilities.Disposables;

namespace DataSakura.Runtime.Utilities
{
    public interface IDisposableLoadUnit : ILoadUnit, IDisposable { }

    public interface ILoadUnit
    {
        UniTask Load();
    }

    public interface ILoadUnit<in T>
    {
        UniTask Load(T param);
    }

    public interface IDisposableLoadUnit<in T> : ILoadUnit<T>, IDisposable { }

    public sealed class LoadingService
    {
        public readonly CompositeDisposable disposables = new();

        private void OnLoadingBegin(object unit)
        {
            Debug.Log($"{unit.GetType().Name} loading is started");
        }

        private async UniTask OnLoadingFinish(object unit, bool isError)
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            int mainThreadId = PlayerLoopHelper.MainThreadId;

            if (mainThreadId != currentThreadId) {
                Debug.Log($"[THREAD] start switching from '{currentThreadId}' thread to main thread '{mainThreadId}'");
                await UniTask.SwitchToMainThread();
            }
        }

        public async UniTask BeginLoading(ILoadUnit loadUnit, bool skipExceptionThrow = false)
        {
            bool isError = true;

            try
            {
                OnLoadingBegin(loadUnit);
                await loadUnit.Load();
                isError = false;
            }
            catch (Exception e)
            {
                Debug.Log(e);

                if (!skipExceptionThrow)
                    throw;
            }
            finally {
                await OnLoadingFinish(loadUnit, isError);
            }
        }

        public async UniTask BeginLoading(IDisposableLoadUnit unit, bool skipExceptionThrow = false)
        {
            disposables.Retain(unit);
            await BeginLoading((ILoadUnit)unit, skipExceptionThrow);
        }

        public async UniTask BeginLoading<T>(ILoadUnit<T> loadUnit, T param, bool skipExceptionThrow = false)
        {
            var isError = true;

            try
            {
                OnLoadingBegin(loadUnit);
                await loadUnit.Load(param);
                isError = false;
            }
            catch (Exception e)
            {
                Debug.Log(e);

                if (!skipExceptionThrow)
                    throw;
            }
            finally
            {
                await OnLoadingFinish(loadUnit, isError);
            }
        }

        public async UniTask BeginLoading<T>(IDisposableLoadUnit<T> unit, T param, bool skipExceptionThrow = false)
        {
            disposables.Retain(unit);
            await BeginLoading((ILoadUnit<T>)unit, param, skipExceptionThrow);
        }

        public async UniTask BeginLoading(bool skipExceptionThrow = false, params ILoadUnit[] units)
        {
            foreach (ILoadUnit loadUnit in units)
                await BeginLoading(loadUnit, skipExceptionThrow);
        }

        public async UniTask BeginLoadingParallel(string logName, bool skipExceptionThrow = false, params ILoadUnit[] units)
        {
            var isError = true;

            try
            {
                OnLoadingBegin(logName);
                var t = UniTask.WhenAll(units.Select(e => e.Load()));
                await t;
                isError = false;
            }
            catch (Exception e) {
                Debug.Log(e);

                if (!skipExceptionThrow)
                    throw;
            }
            finally
            {
                await OnLoadingFinish(logName, isError);
            }
        }
    }
}