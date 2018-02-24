using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading;
using ReactiveUI;

namespace FirstStepsReactiveUI
{
 
	public abstract class ViewModelBase<T> : ReactiveObject, IDisposable where T : ReactiveObject, IDisposable // 1
	{  
 
		protected readonly Lazy<CompositeDisposable> ViewModelBindings = new Lazy<CompositeDisposable>(() => new CompositeDisposable());
         
		public bool IsDisposed { get; private set; } 
 
		protected abstract void RegisterObservables();

        protected virtual void Initialize() { } 
		 
		protected ViewModelBase()
		{
            Initialize();
			RegisterObservables(); 
		} 
 
		#region IDisposable implementation

		public void Dispose()
		{ 
			if (!IsDisposed)
			{
				IsDisposed = true;
				Dispose(true);
				GC.SuppressFinalize(true);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing) 
            
            if (ViewModelBindings?.IsValueCreated ?? false)
                ViewModelBindings?.Value?.Dispose();
		}

        #endregion
	} 
}

