using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistribuidoraVendedores.ViewModels;
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
			BindingContext = new InventarioGeneralVM();
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