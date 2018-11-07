using System;
using System.Runtime.CompilerServices;
using UnityEngine.ResourceManagement;
using Object = UnityEngine.Object;

public static class IAsyncOperationExtensions {
 
    public static AsyncOperationAwaiter GetAwaiter(this IAsyncOperation operation) {
        return new AsyncOperationAwaiter(operation);
    }
 
    public static AsyncOperationAwaiter<T> GetAwaiter<T>(this IAsyncOperation<T> operation) where T : Object {
        return new AsyncOperationAwaiter<T>(operation);
    }
 
    public readonly struct AsyncOperationAwaiter : INotifyCompletion {
 
        readonly IAsyncOperation _operation;
 
        public AsyncOperationAwaiter(IAsyncOperation operation) {
            _operation = operation;
        }
 
        public bool IsCompleted => _operation.Status != AsyncOperationStatus.None;
 
        public void OnCompleted(Action continuation) => _operation.Completed += (op) => continuation?.Invoke();
 
        public object GetResult() => _operation.Result;
 
    }
 
    public readonly struct AsyncOperationAwaiter<T> : INotifyCompletion where T : Object {
 
        readonly IAsyncOperation<T> _operation;
 
        public AsyncOperationAwaiter(IAsyncOperation<T> operation) {
            _operation = operation;
        }
 
        public bool IsCompleted => _operation.Status != AsyncOperationStatus.None;
 
        public void OnCompleted(Action continuation) => _operation.Completed += (op) => continuation?.Invoke();
 
        public T GetResult() => _operation.Result;
 
    }
 
}