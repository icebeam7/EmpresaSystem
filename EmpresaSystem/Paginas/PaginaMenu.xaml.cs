using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Reflection;

namespace EmpresaSystem.Paginas
{
    public sealed partial class PaginaMenu : Page
    {
        public PaginaMenu()
        {
            this.InitializeComponent();
        }

        private NavigationViewItem item;

        private bool NavigateToView(string view)
        {
            var clickedView = Assembly.GetExecutingAssembly().GetType($"EmpresaSystem.Paginas.{view}");

            if (string.IsNullOrWhiteSpace(view) || clickedView == null)
                return false;

            ContentFrame.Navigate(clickedView, null, null);
            return true;
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var view = args.InvokedItemContainer as NavigationViewItem;

            if (view == null || view == item)
                return;

            var clickedView = view.Tag?.ToString();

            if (!NavigateToView(clickedView))
                return;

            item = view;
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
                ContentFrame.GoBack();
        }
    }
}
