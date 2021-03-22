﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistribuidoraVendedores.ViewModels;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores.Inventario
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inventario : ContentPage
	{
		public Inventario()
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
					BindingContext = new InventarioGeneralVM();
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