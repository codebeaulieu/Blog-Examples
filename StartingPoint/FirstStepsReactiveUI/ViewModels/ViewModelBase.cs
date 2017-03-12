using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading;
using ReactiveUI;

namespace FirstStepsReactiveUI
{
	/*
		This is a drastically simplified version of Mike Stonis's (http://www.eightbot.com/) ViewModelBase implementation. 
		Many of the features have been stripped out to simplify this demonstration. Used with permission.
	*/
	public abstract class ViewModelBase<T> : ReactiveObject, IDisposable where T : ReactiveObject, IDisposable
	{  
		protected readonly Lazy<CompositeDisposable> ViewModelBindings = new Lazy<CompositeDisposable>(() => new CompositeDisposable());  

		public bool IsDisposed { get; private set; }

		protected ViewModelBase()
		{
			Initialize();

			RegisterObservables(); 
		}

		protected virtual void Initialize() { }

		protected abstract void RegisterObservables(); 

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

				if (ViewModelBindings != null)
				{
					ViewModelBindings.Value.Dispose();
				}
			}
		}
		#endregion
	}

