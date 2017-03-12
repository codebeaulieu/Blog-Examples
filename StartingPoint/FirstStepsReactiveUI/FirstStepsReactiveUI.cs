using System;
using FirstStepsReactiveUI.UserInterface.Pages;
using Xamarin.Forms;

namespace FirstStepsReactiveUI
{
	public class App : Application
	{
		public App()
		{ 
			MainPage = new Dashboard();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
