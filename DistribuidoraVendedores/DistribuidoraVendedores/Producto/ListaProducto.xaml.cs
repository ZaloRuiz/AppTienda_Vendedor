﻿using DistribuidoraVendedores.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores.Producto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaProducto : ContentPage
	{
		ObservableCollection<ProductoNombre> _listProdNom = new ObservableCollection<ProductoNombre>();
		public ListaProducto()
		{
			InitializeComponent();
		}
		private void ToolbarItemTP_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ListaTipoProducto());
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();

			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/productos/listaProductoNombres.php");
			var productos = JsonConvert.DeserializeObject<List<ProductoNombre>>(response);

			foreach (var item in productos)
			{
				_listProdNom.Add(item);
			}
			listaProd.ItemsSource = _listProdNom;
			btnOrdNombre.Clicked += (sender, args) => listaProd.ItemsSource = _listProdNom.OrderBy(x => x.nombre_producto).ToList();
			btnOrdStock.Clicked += (sender, args) => listaProd.ItemsSource = _listProdNom.OrderBy(x => x.stock).ToList();
		}
		private async void OnItemSelected(object sender, ItemTappedEventArgs e)
		{
			//var detalles = e.Item as Models.ProductoNombre;
			//await Navigation.PushAsync(new EditarBorrarProducto(detalles.id_producto, detalles.nombre_producto, detalles.nombre_tipo_producto, detalles.stock,
			//	detalles.stock_valorado, detalles.promedio, detalles.precio_venta, detalles.producto_alerta));
		}
		private void btnOrdenar_Clicked(object sender, EventArgs e)
		{
			if (btnOrdNombre.IsVisible == false)
			{
				btnOrdStock.IsVisible = true;
				btnOrdNombre.IsVisible = true;
			}
			else if (btnOrdNombre.IsVisible == true)
			{
				btnOrdNombre.IsVisible = false;
				btnOrdStock.IsVisible = false;
			}
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