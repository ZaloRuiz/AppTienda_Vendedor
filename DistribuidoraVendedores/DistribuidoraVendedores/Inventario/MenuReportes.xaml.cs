using DistribuidoraVendedores.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores.Inventario
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuReportes : ContentPage
	{
		public MenuReportes ()
		{
			InitializeComponent ();
		}
		private async void Button_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new R_VentaDiaria());
		}

		private async void Button_Clicked_1(object sender, EventArgs e)
		{
			await PopupNavigation.Instance.PushAsync(new ListaR_InventarioDia());
		}
	}
}