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
 
        bool _bindingsRegistered;

        [DataMember]
        public bool BindingsRegistered
        {
            get { return _bindingsRegistered; }
            protected set { this.RaiseAndSetIfChanged(ref _bindingsRegistered, value); }
        }

		protected abstract void RegisterObservables();

        protected virtual void Initialize() { } 
  
		protected ViewModelBase()
		{
            Initialize();
			RegisterObservables(); 
		} 
 
        public void RegisterBindings()
        {
            if (!BindingsRegistered)
            {
                RegisterObservables();
                BindingsRegistered = true;
            }
        }

        public void UnregisterBindings()
        { 
            if (ViewModelBindings?.IsValueCreated ?? false)
                ViewModelBindings?.Value?.Clear();

            BindingsRegistered = false;
        }

		#region IDisposable implementation

		public void Dispose()
		{   
			Dispose(true);
			GC.SuppressFinalize(true); 
		}

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                IsDisposed = true;
  
                if (ViewModelBindings?.IsValueCreated ?? false)
                    ViewModelBindings?.Value?.Dispose();
            }
        }

        #endregion
	} 
}

