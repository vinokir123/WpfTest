using ClearWpf.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace ClearWpf.Infrastructure.Behaviors
{
    public class WindowTitleBarBehavior : Behavior<UIElement>
    {
        private Window _Window;

        protected override void OnAttached()
        {
            _Window = AssociatedObject as Window ?? AssociatedObject.FindLogicalParent<Window>();
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
            _Window = null;
        }

        private void OnMouseDown(object Sender, MouseButtonEventArgs E)
        {
            switch (E.ClickCount)
            {
                case 1:
                    DragMove();
                    break;
                default:
                    Maximize();
                    break;
            }
        }

        private void DragMove()
        {
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;
            window.DragMove();
        }

        private void Maximize()
        {
            if (!(AssociatedObject.FindVisualRoot() is Window window)) return;
            switch (window.WindowState)
            {
                case WindowState.Normal:
                    window.WindowState = WindowState.Maximized;
                    break;
                case WindowState.Maximized:
                    window.WindowState = WindowState.Normal;
                    break;
            }
        }
    }
}
