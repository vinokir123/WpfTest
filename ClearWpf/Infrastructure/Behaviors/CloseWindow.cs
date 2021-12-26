using ClearWpf.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace ClearWpf.Infrastructure.Behaviors
{
    public class CloseWindow : Behavior<Button>
    {
        protected override void OnAttached() => AssociatedObject.Click += OnButtonClick;

        protected override void OnDetaching() => AssociatedObject.Click -= OnButtonClick;

        private void OnButtonClick(object Sender, RoutedEventArgs E) =>
            (AssociatedObject.FindVisualRoot() as Window)?.Close();
    }
}
