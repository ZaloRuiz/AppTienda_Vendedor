using DistribuidoraVendedores.Models;
using DistribuidoraVendedores.ViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaPedidos : TabbedPage
	{
		public ListaPedidos()
		{
			InitializeComponent();
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					this.BindingContext = new ListaPedidosVM();
				}
				catch (Exception err)
				{
					await DisplayAlert("Error", "Algo salio mal, intentelo de nuevo por favor", "OK");
				}
			}
			else
			{
				await DisplayAlert("Error", "Necesitas estar conectado a internet", "OK");
			}
		}
		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AgregarVenta());
		}
		private void ToolbarItemP_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AgregarVenta());
		}
		private void ToolbarItemC_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AgregarVenta());
		}
		private async void OnItemSelectedE(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as VentasNombre;
			await Navigation.PushAsync(new MostrarVenta(detalles.id_venta, detalles.fecha, detalles.numero_factura, detalles.nombre_cliente,
														detalles.nombre_vendedor, detalles.tipo_venta, detalles.saldo, detalles.total, detalles.fecha_entrega,
														detalles.estado, detalles.observacion));
		}
		private async void OnItemSelectedP(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as VentasNombre;
			await Navigation.PushAsync(new MostrarVentaPendiente(detalles.id_venta, detalles.fecha, detalles.numero_factura, detalles.nombre_cliente,
														detalles.nombre_vendedor, detalles.tipo_venta, detalles.saldo, detalles.total, detalles.fecha_entrega,
														detalles.estado, detalles.observacion));
		}
		private async void OnItemSelectedC(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as VentasNombre;
			await Navigation.PushAsync(new MostrarVenta(detalles.id_venta, detalles.fecha, detalles.numero_factura, detalles.nombre_cliente,
														detalles.nombre_vendedor, detalles.tipo_venta, detalles.saldo, detalles.total, detalles.fecha_entrega,
														detalles.estado, detalles.observacion));
		}
		protected override bool OnBackButtonPressed()
		{
			Device.BeginInvokeOnMainThread(async () =>
			{
				var result = await this.DisplayAlert("SALIR", "Quiere salir de la aplicacion?", "SI", "NO");
				if (result) await this.Navigation.PopModalAsync();
			});
			return true;
		}
	}
}