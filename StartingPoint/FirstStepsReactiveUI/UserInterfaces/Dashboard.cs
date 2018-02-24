using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using FirstStepsReactiveUI;
using ReactiveUI;
using Xamarin.Forms;

namespace FirstStepsReactiveUI.UserInterface.Pages
{
	public class Dashboard : ContentPageBase<ViewModels.Dashboard>
	{
		// 1
		Image _images;

		Label _status;

		//2
		public Dashboard()
		{
			ViewModel = new ViewModels.Dashboard();
			ViewModel.InitializeCommand.Execute();
		}

		//3
		protected override void SetupUserInterface()
		{ 
			_status = new Label
			{ 
				FontSize = 20,
				FontFamily = "AvenirNext-Medium",
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.Center,
				Margin = new Thickness(0,40,0,0)
			};

			_images = new Image {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Aspect = Aspect.AspectFit, 
				HeightRequest = 350 
			};

			Content = new StackLayout 
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = 20,
				Children = { 
					_status,
					_images
				}
			};
		}

		//4
		protected override void BindControls()
		{
			this.OneWayBind(ViewModel, vm => vm.Title, c => c.Title)
			    .DisposeWith(ControlBindings);

			this.Bind(ViewModel, x => x.StatusMessage, c => c._status.Text)
			    .DisposeWith(ControlBindings);

			this.WhenAnyValue(x => x.ViewModel.CurrentImage)
                .ObserveOn(RxApp.MainThreadScheduler)
				.BindTo(this, x => x._images.Source)
			    .DisposeWith(ControlBindings);  
		} 
	}
}
