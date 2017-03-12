using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;

namespace FirstStepsReactiveUI.ViewModels
{
	public class Dashboard : ViewModelBase<Dashboard>
	{ 

		ImageSource _currentImage;

		[DataMember]
		public ImageSource CurrentImage
		{
			get { return _currentImage; }
			set { this.RaiseAndSetIfChanged(ref _currentImage, value); }
		}

		[DataMember]
		public List<string> ImageList { get; set; } = new List<string>();
 
		[DataMember]
		public string Title
		{
			get { return "My Dashboard"; }
		}

		public ReactiveCommand<Unit, Unit> InitializeCommand { get; private set; }

		protected override void RegisterObservables()
		{
			InitializeCommand = ReactiveCommand.CreateFromTask(async _ => 
			{
				// initialization logic goes here

				// maybe we're getting images from a server
				await Task.Delay(4000); // simulate a lengthy server response
				ImageList.Add("xamagon.png");
				ImageList.Add("eightbot.png");
				ImageList.Add("reactivelogo.png");
				ImageList.Add("codebeaulieu.png");
				ImageList.Add("Rx_Logo_512.png");
				await Task.FromResult(Unit.Default);

			}).DisposeWith(ViewModelBindings.Value);

			Observable
				.Interval(TimeSpan.FromSeconds(1))
				.ObserveOn(RxApp.MainThreadScheduler)
				.Select(_ =>
				{
					if (ImageList.Count == 0) return ImageSource.FromFile("reactivelogo.png");

					Random random = new Random();
					int number = random.Next(0, ImageList.Count);

					return ImageSource.FromFile(ImageList[number]); 
					 
			}).BindTo(this, x => x.CurrentImage);
 
		}
	}
}
