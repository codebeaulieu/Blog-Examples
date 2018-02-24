using System;
using System.Reactive.Disposables;

namespace FirstStepsReactiveUI
{
	/*
		This extension was taken from Eight Bot's Reactive-Exampes repository with permission.
		url: https://github.com/TheEightBot/Reactive-Examples/blob/master/ReactiveExtensionExamples/Extensions/IObservableExtensions.cs
		author: Mike Stonis
		website: http://www.eightbot.com/
	*/
	public static class IObservableExtensions
	{ 
        public static TDisposable DisposeWith<TDisposable>(this TDisposable observable, CompositeDisposable disposables) where TDisposable : class, IDisposable
        {
            if (observable != null)
                disposables?.Add(observable);

            return observable;
        }

        public static TDisposable DisposeWith<TDisposable>(this TDisposable observable, Lazy<CompositeDisposable> disposables) where TDisposable : class, IDisposable
        {
            if (observable != null)
                disposables?.Value?.Add(observable);

            return observable;
        }
	}
}
