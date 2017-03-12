using System;
using System.Reactive.Disposables;
using FirstStepsReactiveUI;
using ReactiveUI;
using Xamarin.Forms;

namespace FirstStepsReactiveUI.UserInterface.Pages
{
	public class Dashboard : ContentPageBase<ViewModels.Dashboard>
	{
		Image _images;

		public Dashboard()
		{
			ViewModel = new ViewModels.Dashboard();
			ViewModel.InitializeCommand.Execute();
		}

		protected override void SetupUserInterface()
		{
			_images = new Image {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Aspect = Aspect.AspectFit,
				Source = "reactivelogo.png"
			};

			Content = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = 20,
				Children = { 
					_images
				}
			};
		}

		protected override void BindControls()
		{
			this.OneWayBind(ViewModel, vm => vm.Title, c => c.Title)
			    .DisposeWith(ControlBindings.Value);

			this.WhenAnyValue(x => x.ViewModel.CurrentImage)
				.BindTo(this, x => x._images.Source)
			    .DisposeWith(ControlBindings.Value);
		}
 
	}
}
