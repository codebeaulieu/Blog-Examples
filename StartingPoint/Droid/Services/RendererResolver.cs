using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace FirstStepsReactiveUI.Droid.Services
{ 
    public class RendererResolver : Interfaces.IRendererResolver
    {
        MethodInfo _cellRenderer;

        public RendererResolver()
        {
            _cellRenderer = typeof(CellRenderer).GetMethod("GetRenderer", BindingFlags.Static | BindingFlags.NonPublic);
        }

        public object GetCellRenderer(BindableObject element)
        {
            var result = _cellRenderer.Invoke(null, new object[] { element });
            return result;
        }

        public object GetRenderer(VisualElement element)
        {
            return Platform.GetRenderer(element);
        }

        public bool HasCellRenderer(BindableObject element)
        {
            return GetCellRenderer(element) != null;
        }

        public bool HasRenderer(VisualElement element)
        {
            return GetRenderer(element) != null;
        }
    }
}
