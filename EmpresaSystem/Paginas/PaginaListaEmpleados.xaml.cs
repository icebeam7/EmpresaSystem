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

using EmpresaSystem.Servicios;
using EmpresaSystem.Modelos;
using System.Collections.ObjectModel;

namespace EmpresaSystem.Paginas
{
    public sealed partial class PaginaListaEmpleados : Page
    {
        public PaginaListaEmpleados()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<EmpleadoDTO> Items = new ObservableCollection<EmpleadoDTO>();

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Items = new ObservableCollection<EmpleadoDTO>(
                await ServicioWebApi.Servicio.ObtenerItems<EmpleadoDTO>("Empleados"));
            Bindings.Update();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var item = new EmpleadoDTO();
            Frame.Navigate(typeof(PaginaDetalleEmpleado), item);
        }

        private async void BtnDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (lsvEmpleados.SelectedItem != null)
            {
                var empleado = lsvEmpleados.SelectedItem as EmpleadoDTO;
                var item = await ServicioWebApi.Servicio.ObtenerItem<EmpleadoDTO>("Empleados", empleado.Id);
                Frame.Navigate(typeof(PaginaDetalleEmpleado), item);
            }
        }
    }
}
