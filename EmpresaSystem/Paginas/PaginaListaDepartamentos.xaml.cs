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

using EmpresaSystem.Modelos;
using EmpresaSystem.Servicios;
using System.Collections.ObjectModel;

namespace EmpresaSystem.Paginas
{
    public sealed partial class PaginaListaDepartamentos : Page
    {
        public PaginaListaDepartamentos()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<DepartamentoDTO> Items = new ObservableCollection<DepartamentoDTO>();

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Items = new ObservableCollection<DepartamentoDTO>(
                await ServicioWebApi.Servicio.ObtenerItems<DepartamentoDTO>("Departamentos"));
            Bindings.Update();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var item = new DepartamentoDTO();
            Frame.Navigate(typeof(PaginaDetalleDepartamento), item);
        }

        private async void BtnDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (lsvDepartamentos.SelectedItem != null)
            {
                var departamento = lsvDepartamentos.SelectedItem as DepartamentoDTO;
                var item = await ServicioWebApi.Servicio.ObtenerItem<DepartamentoDTO>("Departamentos", departamento.Id);
                Frame.Navigate(typeof(PaginaDetalleDepartamento), item);
            }
        }
    }
}
