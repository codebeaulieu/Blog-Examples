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
	public abstract class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : ViewModelBase<TViewModel> // 1
	{    
		protected Lazy<CompositeDisposable> ControlBindings = new Lazy<CompositeDisposable>(() => new CompositeDisposable()); 
 
		protected abstract void SetupUserInterface();

		protected abstract void BindControls();
 
		protected ContentPageBase() : base()
		{   
			SetupUserInterface(); 
			BindControls();  
		}
 
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
