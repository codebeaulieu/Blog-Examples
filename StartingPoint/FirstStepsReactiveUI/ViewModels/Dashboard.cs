﻿using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using ReactiveUI;

namespace FirstStepsReactiveUI.ViewModels
{
	public class Dashboard : ViewModelBase<Dashboard>
	{  
        [DataMember]
        public string Title
        {
            get { return "My Dashboard"; }
        }
 
		string _statusMessage;

		[DataMember]
		public string StatusMessage
		{
			get { return _statusMessage; }
			set { this.RaiseAndSetIfChanged(ref _statusMessage, value); }
		}

		string _currentImage;

		[DataMember]
		public string CurrentImage
		{
			get { return _currentImage; }
			set { this.RaiseAndSetIfChanged(ref _currentImage, value); }
		}

		[DataMember]
		public List<string> ImageList { get; set; }

		protected override void Initialize()
		{
            base.Initialize();
            ImageList = new List<string>();
		}
 
		ReactiveCommand<Unit, Unit> _initializeCommand;
        [DataMember]
        public ReactiveCommand<Unit, Unit> InitializeCommand
        {
            get { return _initializeCommand; }
            private set
            {
                this.RaiseAndSetIfChanged(ref _initializeCommand, value);
            }
        }
 
        protected override void RegisterObservables()
        {
            
            InitializeCommand = ReactiveCommand.CreateFromTask(async _ => 
			{
				// initialization logic goes here 
				StatusMessage = "Initializing...";

				// maybe we're getting images from a server 
				await Task.Delay(1500); // dont use Task.Delay in real life plz

				StatusMessage = "Downloading...";

				// simulate a lengthy server response
                await Task.Delay(2500); // dont use Task.Delay in real life plz

				StatusMessage = "Go-Go Random Logos!";

				ImageList.Add("xamagon.png");
				ImageList.Add("eightbot.png");
				ImageList.Add("reactivelogo.png");
				ImageList.Add("888.png");
				ImageList.Add("Rx_Logo_512.png");

			})
            .DisposeWith(ViewModelBindings);

	 
			Observable
				.Interval(TimeSpan.FromMilliseconds(500))
                .SubscribeOn(RxApp.TaskpoolScheduler)
				.Select(_ =>
				{
					if (ImageList.Count == 0) 
						return "reactivelogo.png";

					Random random = new Random();
					int number = random.Next(0, ImageList.Count);

					return ImageList[number];  
    			})
                .ObserveOn(RxApp.MainThreadScheduler)
                .BindTo(this, x => x.CurrentImage)
                .DisposeWith(ViewModelBindings);
 
		}
	}
}
