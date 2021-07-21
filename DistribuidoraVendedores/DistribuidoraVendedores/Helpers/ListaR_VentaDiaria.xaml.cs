﻿using DistribuidoraVendedores.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores.Helpers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaR_VentaDiaria : PopupPage
	{
		private DateTime _fechaInicio = DateTime.Today;
		private DateTime _fechaFinal = DateTime.Today;
		private int _cantTotal = 0;
		private decimal _montoTotal = 0;
		public ListaR_VentaDiaria(DateTime _fechaElegida)
		{
			InitializeComponent();
			txtTitulo.Text = "Ventas de el " + _fechaInicio.ToString("dd/MM/yyy");
			_fechaInicio = _fechaElegida;
			_fechaFinal = _fechaElegida;
			GetProductos();
		}
		private async void GetProductos()
		{
			if (CrossConnectivity.Current.IsConnected)
			{
				try
				{
					txtTitulo.Text = "Ventas de el " + _fechaInicio.ToString("dd/MM/yyy");
					ReporteVentaDiaria _ventaXprod = new ReporteVentaDiaria()
					{
						id_vendedor = App._Id_Vendedor,
						fecha_inicio = _fechaInicio,
						fecha_final = _fechaFinal
					};
					var json = JsonConvert.SerializeObject(_ventaXprod);
					var content = new StringContent(json, Encoding.UTF8, "application/json");
					HttpClient client = new HttpClient();
					var result = await client.PostAsync("https://dmrbolivia.com/api_distribuidora/reportes/ReporteVentasDiariasVendedor.php", content);

					var jsonR = await result.Content.ReadAsStringAsync();
					var dataVentXprod = JsonConvert.DeserializeObject<List<ReporteVentaDiaria>>(jsonR);
					if (dataVentXprod != null)
					{
						foreach (var item in dataVentXprod)
						{
							_cantTotal = _cantTotal + item.cantidad;
							_montoTotal = _montoTotal + item.monto_vend;
						}
						txtTotalCajas.Text = _cantTotal.ToString();
						txtTotalMonto.Text = _montoTotal.ToString() + " Bs.";
						listData.ItemsSource = dataVentXprod;
					}
				}
				catch (Exception err)
				{
					await DisplayAlert("Error", "Algo salio mal, intentelo de nuevo", "OK");
				}
			}
			else
			{
				await DisplayAlert("Error", "Necesitas estar conectado a internet", "OK");
			}
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Send(this, "allowPortrait");

		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			MessagingCenter.Send(this, "preventPortrait");
		}
		protected override bool OnBackButtonPressed()
		{
			return true;
		}
		protected override bool OnBackgroundClicked()
		{
			return false;
		}

		private async void btnCerrar_Clicked(object sender, EventArgs e)
		{
			await PopupNavigation.Instance.PopAllAsync();
		}
	}
}