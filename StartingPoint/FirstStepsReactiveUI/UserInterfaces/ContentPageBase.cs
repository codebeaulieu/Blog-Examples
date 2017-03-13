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
	public abstract class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : ViewModelBase<TViewModel> // 1
	{   
		// 2
		protected Lazy<CompositeDisposable> ControlBindings = new Lazy<CompositeDisposable>(() => new CompositeDisposable()); 

		// 3 
		protected abstract void SetupUserInterface();
		protected abstract void BindControls();

		// 4
		protected ContentPageBase() : base()
		{   
			SetupUserInterface(); 
			BindControls();  
		}

		// 5
		protected override void OnDisappearing()
		{
			base.OnDisappearing(); 

			UnbindControls(); 
		} 


		protected void UnbindControls()
		{  
			if (ControlBindings == null) return;

			ControlBindings.Value.Clear();
		} 
	}
}
