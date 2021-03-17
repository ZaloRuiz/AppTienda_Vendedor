using DistribuidoraVendedores.Helpers;
using DistribuidoraVendedores.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		string BusyReason = "Ingresando";
		public Login()
		{
			InitializeComponent();
		}

		private async void btnIngresar_Clicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(entryUsuario.Text) || (!string.IsNullOrEmpty(entryUsuario.Text)))
			{
				if(!string.IsNullOrWhiteSpace(entryPassword.Text) || (!string.IsNullOrEmpty(entryPassword.Text)))
				{
					await PopupNavigation.Instance.PushAsync(new BusyPopup(BusyReason));
					try
					{
						HttpClient client = new HttpClient();
						var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/vendedores/listaVendedores.php");
						var vendedores = JsonConvert.DeserializeObject<List<Vendedores>>(response);

						foreach (var item in vendedores)
						{
							if (entryUsuario.Text.ToLower() == item.usuario)
							{
								if (entryPassword.Text.ToLower() == item.password)
								{
									App._Id_Vendedor = item.id_vendedor;
									App._Nombre_Vendedor = item.nombre;
									entryPassword.Text = string.Empty;
									await Navigation.PushModalAsync(new Menu());
									
								}
								else
								{
									await PopupNavigation.Instance.PopAsync();
									entryPassword.Text = string.Empty;
									await DisplayAlert("Error", "Contraseña incorrecta", "Ok");
								}
							}
						}
					}
					catch(Exception err)
					{
						//await PopupNavigation.Instance.PopAsync();
						await DisplayAlert("Error", err.ToString(), "OK");
					}
					await PopupNavigation.Instance.PopAsync();
				}
				else
				{
					await DisplayAlert("Campo vacio", "El campo de Contraseña esta vacio", "Ok");
				}
			}
			else
			{
				await DisplayAlert("Campo vacio", "El campo de Usuario esta vacio", "Ok");
			}
		}
	}
}