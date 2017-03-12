using System;
using Xamarin.Forms;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;
using System.Reactive;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace FirstStepsReactiveUI
{
	/*
		This is a drastically simplified version of Mike Stonis's ContentPageBase implementation. 
		Many of the features have been stripped out to simplify this demonstration. Used with permission.
		http://www.eightbot.com
	*/
	public abstract class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel>, ICanActivate where TViewModel : ViewModelBase<TViewModel>
	{ 
		// 1
		Subject<Unit> activated = new Subject<Unit>();
		public IObservable<Unit> Activated { get { return activated.AsObservable(); } }
		Subject<Unit> deactivated = new Subject<Unit>();
		public IObservable<Unit> Deactivated { get { return deactivated.AsObservable(); } } 

		// 2
		protected Lazy<CompositeDisposable> ControlBindings = new Lazy<CompositeDisposable>(() => new CompositeDisposable()); 

		// 3
		protected ContentPageBase() : base()
		{  
			Initialize();
			SetupUserInterface(); 
			BindControls();  
		}

		// 4
		protected override void OnAppearing()
		{  
		    BindControls(); 

			activated.OnNext(Unit.Default);

			base.OnAppearing();
		}

		// 5
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
 
			deactivated.OnNext(Unit.Default);

			UnbindControls(); 
		} 

		// 6
		protected virtual void Initialize() { }
		protected abstract void SetupUserInterface();
		protected abstract void BindControls(); 

		// 7
		protected void UnbindControls()
		{  
			if (ControlBindings == null) return;

			ControlBindings.Value.Clear();
		} 
	}
}
