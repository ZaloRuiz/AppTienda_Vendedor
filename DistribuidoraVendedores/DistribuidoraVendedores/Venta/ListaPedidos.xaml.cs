using DistribuidoraVendedores.Models;
using DistribuidoraVendedores.ViewModels;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaPedidos : TabbedPage
	{
		List<VentasNombre> Items;
		ObservableCollection<VentasNombre> _listaPedidosEnt = new ObservableCollection<VentasNombre>();
		ObservableCollection<VentasNombre> _listaPedidosPen = new ObservableCollection<VentasNombre>();
		ObservableCollection<VentasNombre> _listaPedidosCanc = new ObservableCollection<VentasNombre>();
		List<string> list_C = new List<string>();
		public ListaPedidos()
		{
			InitializeComponent();
		}
		private async void GetVenta()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				
			}

		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			if (CrossConnectivity.Current.IsConnected)
			{
				_listaPedidosEnt.Clear();
				_listaPedidosPen.Clear();
				_listaPedidosCanc.Clear();
				try
				{
					Ventas _Ventas = new Ventas()
					{
						id_vendedor = App._Id_Vendedor
					};
					var json = JsonConvert.SerializeObject(_Ventas);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					HttpClient client = new HttpClient();
					var result = await client.PostAsync("https://dmrbolivia.com/api_distribuidora/ventas/listaVentaNombreBuscar.php", content);

					var jsonR = await result.Content.ReadAsStringAsync();
					var lista_ventas = JsonConvert.DeserializeObject<List<VentasNombre>>(jsonR);

					foreach (var item in lista_ventas)
					{
						if (item.estado == "Entregado")
						{
							_listaPedidosEnt.Add(item);
						}
						else if (item.estado == "Pendiente")
						{
							_listaPedidosPen.Add(item);
						}
						else if (item.estado == "Cancelado")
						{
							_listaPedidosCanc.Add(item);
						}
					}
					listaEntregados.ItemsSource = _listaPedidosEnt;
					listaPendientes.ItemsSource = _listaPedidosPen;
					listaCancelados.ItemsSource = _listaPedidosCanc;
				}
				catch (Exception err)
				{
					await DisplayAlert("Error", "Algo salio mal, intentelo de nuevo por favor", "OK");
				}
				try
				{
					InitList_E();
					//InitList_P();
					//InitList_C();
					InitSearchBar_E();
					//InitSearchBar_p();
					//InitSearchBar_C();
					listaEntregados.BeginRefresh();
					listaEntregados.EndRefresh();
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
		async void InitList_E()
		{
			Items = new List<VentasNombre>();
			try
			{
				foreach(var item in _listaPedidosEnt)
				{
					Items.Add(new VentasNombre
					{
						id_venta = item.id_venta,
						fecha = item.fecha,
						numero_factura = item.numero_factura,
						nombre_cliente = item.nombre_cliente,
						nombre_vendedor = item.nombre_vendedor,
						tipo_venta = item.tipo_venta,
						saldo = item.saldo,
						total = item.total,
						fecha_entrega = item.fecha_entrega,
						estado = item.estado,
						observacion = item.observacion
					});
				}
			}
			catch (Exception err)
			{
				await DisplayAlert("ERROR", err.ToString(), "OK");
			}
			listaEntregados.ItemsSource = Items;

		}
		void InitSearchBar_E()
		{
			sb_search_E.TextChanged += (s, e) => FilterItem_E(sb_search_E.Text);
			sb_search_E.SearchButtonPressed += (s, e) => FilterItem_E(sb_search_E.Text);
		}
		private void FilterItem_E(string filter)
		{
			listaEntregados.BeginRefresh();
			if (string.IsNullOrWhiteSpace(filter))
			{
				listaEntregados.ItemsSource = Items;
			}
			else if (string.IsNullOrEmpty(filter))
			{
				listaEntregados.ItemsSource = Items;
			}
			else
			{
				listaEntregados.ItemsSource = Items.Where(x => x.nombre_cliente.ToLower().Contains(filter.ToLower()));
			}
			listaEntregados.EndRefresh();
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
		private async void toolbarBuscar_Clicked(object sender, EventArgs e)
		{
			
			foreach(var item in _listaPedidosPen)
			{
				list_C.Add(item.nombre_cliente);
			}
			IEnumerable<string> array_C = list_C.Distinct<string>();
			string _c_elegido = await DisplayActionSheet("Elija un cliente", "Cancelar", "Cerrar", array_C.ToArray());
			listaPendientes.ItemsSource = _listaPedidosPen.Where(x => x.nombre_cliente.ToLower().Contains(_c_elegido.ToLower()));
		}
	}
}