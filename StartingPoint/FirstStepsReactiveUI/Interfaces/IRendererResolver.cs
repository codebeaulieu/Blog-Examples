using System;
using Xamarin.Forms;

namespace FirstStepsReactiveUI.Interfaces
{
    public interface IRendererResolver
    {
        object GetRenderer(VisualElement element);

        bool HasRenderer(VisualElement element);

        object GetCellRenderer(BindableObject element);

        bool HasCellRenderer(BindableObject element);
    }
}


