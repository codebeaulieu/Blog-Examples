using System;
using System.Reactive.Linq; 
using ReactiveUI;
using Xamarin.Forms;

namespace FirstStepsReactiveUI.UserInterface.Pages
{ 
    public class Dashboard : ContentPageBase<ViewModels.Dashboard>
    { 
        Grid _mainLayout;

        Image _images;

        Label _status;

        public Dashboard()
        {
            ViewModel = new ViewModels.Dashboard();
            ViewModel?.InitializeCommand.Execute();
        }

        protected override void SetupUserInterface()
        {

            NavigationPage.SetBackButtonTitle(this, "Back");

            _mainLayout = new Grid
            {
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(16, 24, 16, 8), 
                ColumnDefinitions = new ColumnDefinitionCollection {
                    new ColumnDefinition { Width = GridLength.Star },
                },
                RowDefinitions = new RowDefinitionCollection {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                }
            };

            _status = new Label
            {
                FontSize = 20,
                FontFamily = "AvenirNext-Medium",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand 
            };

            _images = new Image
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.AspectFit,
                HeightRequest = 350
            };

            _mainLayout.Children.Add(_status, 0, 0);

            _mainLayout.Children.Add(_images, 0, 1);

            Content = _mainLayout;
        }

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
