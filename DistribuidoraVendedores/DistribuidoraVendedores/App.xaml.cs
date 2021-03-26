using DistribuidoraVendedores.Models;
using DistribuidoraVendedores.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();

			DependencyService.Register<MockDataStore>();
			MainPage = new Menu();
			//MainPage = new Login();
		}
		public static ObservableCollection<DetalleVenta_previo> _detalleVData = new ObservableCollection<DetalleVenta_previo>();
		public static ObservableCollection<DetalleVenta_previo> _DetalleVentaData { get { return _detalleVData; } }
		public static ObservableCollection<DetalleCompra_previo> _detalleCData = new ObservableCollection<DetalleCompra_previo>();
		public static ObservableCollection<DetalleCompra_previo> _DetalleCompraData { get { return _detalleCData; } }
		public static DateTime _fechaInicioFiltro = DateTime.Today.AddYears(-5);
		public static DateTime _fechaFinalFiltro = DateTime.Now;
		public static int _Id_Vendedor = 17;
		public static string _Nombre_Vendedor = "Richard Poma";
		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
