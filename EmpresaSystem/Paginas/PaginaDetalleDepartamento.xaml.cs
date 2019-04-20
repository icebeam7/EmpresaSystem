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

namespace EmpresaSystem.Paginas
{
    public sealed partial class PaginaDetalleDepartamento : Page
    {
        public PaginaDetalleDepartamento()
        {
            this.InitializeComponent();
        }

        DepartamentoDTO Item;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var item = e.Parameter as DepartamentoDTO;
            DataContext = item;
            Item = item;

            if (item.Id > 0)
                btnEliminar.Visibility = Visibility.Visible;

            Bindings.Update();
        }

        private async void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var op = false;

            if (Item.Id > 0)
            {
                var update = await ServicioWebApi.Servicio.ActualizarItem<DepartamentoDTO>("Departamentos", Item, Item.Id);
                op = update;
            }
            else
            {
                var add = await ServicioWebApi.Servicio.AgregarItem<DepartamentoDTO>("Departamentos", Item);
                op = (add != null);
            }

            await MostrarResultado(op);

            if (op)
                Frame.GoBack();
        }

        async Task MostrarResultado(bool op)
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
                PrimaryButtonText = "Si",
                Content = "¿Estás seguro que deseas eliminar este registro?"
            };

            var resultado = await dialog.ShowAsync();

            if (resultado == ContentDialogResult.Primary)
            {
                var op = await ServicioWebApi.Servicio.EliminarItem("Departamentos", Item.Id);
                await MostrarResultado(op);

                if (op)
                    Frame.GoBack();
            }
        }
    }
}
