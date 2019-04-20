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
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace EmpresaSystem.Paginas
{
    public sealed partial class PaginaDetalleEmpleado : Page
    {
        public PaginaDetalleEmpleado()
        {
            this.InitializeComponent();
        }

        EmpleadoDTO Item;
        ObservableCollection<DepartamentoDTO> Departamentos = new ObservableCollection<DepartamentoDTO>();

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var item = e.Parameter as EmpleadoDTO;
            DataContext = item;
            Item = item;

            Departamentos = new ObservableCollection<DepartamentoDTO>(
                await ServicioWebApi.Servicio.ObtenerItems<DepartamentoDTO>("Departamentos"));

            if (item.Id > 0)
            {
                btnEliminar.Visibility = Visibility.Visible;
                Bindings.Update();

                item.FK_DepartamentoEmpleado = Departamentos.First(x => x.Id == item.IdDepartamento);
                cbxDepartamento.SelectedItem = item.FK_DepartamentoEmpleado;
            }

            Bindings.Update();
        }

        private async void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var op = false;

            if (cbxDepartamento.SelectedItem != null)
            {
                Item.IdDepartamento = (cbxDepartamento.SelectedItem as DepartamentoDTO).Id;

                if (Item.Id > 0)
                {
                    var update = await ServicioWebApi.Servicio.ActualizarItem<EmpleadoDTO>("Empleados", Item, Item.Id);
                    op = update;
                }
                else
                {
                    var add = await ServicioWebApi.Servicio.AgregarItem<EmpleadoDTO>("Empleados", Item);
                    op = (add != null);
                }

                await MostrarResultado(op);

                if (op)
                    Frame.GoBack();
            }
        }

        private async Task MostrarResultado(bool op)
        {
            var dialog = new ContentDialog
            {
                Title = "Resultado",
                CloseButtonText = "OK",
                Content = op ? "Operación realizada con éxito"
                            : "Hubo un error. Revisa los datos e intenta de nuevo"
            };

            await dialog.ShowAsync();
        }

        private async void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Confirmar operación",
                CloseButtonText = "No",
                PrimaryButtonText = "Sí",
                Content = "¿Estás seguro que deseas eliminar este registro?"
            };

            var resultado = await dialog.ShowAsync();

            if (resultado == ContentDialogResult.Primary)
            {
                var op = await ServicioWebApi.Servicio.EliminarItem("Empleados", Item.Id);
                await MostrarResultado(op);

                if (op)
                    Frame.GoBack();
            }
        }
    }
}
