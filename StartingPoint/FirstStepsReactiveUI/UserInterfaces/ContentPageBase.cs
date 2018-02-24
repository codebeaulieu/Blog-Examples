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
 
        bool _controlsBound = false;

		protected abstract void SetupUserInterface();

		protected abstract void BindControls();
 
		protected ContentPageBase() : base()
		{   
			SetupUserInterface();
		}
 
        const string RendererPropertyName = "Renderer";

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals(RendererPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                var rendererResolver = DependencyService.Get<Interfaces.IRendererResolver>();

                if (rendererResolver == null)
                    throw new NullReferenceException("The renderer resolver was not initialized properly");

                if (rendererResolver.HasRenderer(this))
                    RegisterBindings();
                else
                    UnregisterBindings();
            } 
        }

		protected override void OnDisappearing()
		{
			base.OnDisappearing();  
		}  

        protected void RegisterBindings()
        {
            if (_controlsBound)
                return;
 
            ViewModel?.RegisterBindings();
            BindControls();
            _controlsBound = true;
        }

        protected void UnregisterBindings()
        {
            _controlsBound = false;
 
            ViewModel?.UnregisterBindings();

            if (!ControlBindings.IsValueCreated) return;

            ControlBindings?.Value?.Clear();
        }
    } 
}
