using DistribuidoraVendedores.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores.Compra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaCompra : ContentPage
	{
		public ListaCompra()
		{
			InitializeComponent();
		}
		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AgregarCompra());
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					HttpClient client = new HttpClient();
					var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/compras/listaCompraNombre.php");
					var compras = JsonConvert.DeserializeObject<List<ComprasNombre>>(response);

					listaCompra.ItemsSource = compras;
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
		private async void OnItemSelected(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as ComprasNombre;
			await Navigation.PushAsync(new MostrarCompra(detalles.id_compra, detalles.fecha_compra, detalles.numero_factura, detalles.nombre_proveedor,
														detalles.saldo, detalles.total));
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