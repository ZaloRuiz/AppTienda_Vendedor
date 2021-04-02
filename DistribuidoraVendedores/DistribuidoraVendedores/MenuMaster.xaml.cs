using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraVendedores
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuMaster : ContentPage
	{
		public ListView ListView;

		public MenuMaster()
		{
			InitializeComponent();

			BindingContext = new MenuMasterViewModel();
			ListView = MenuItemsListView;
		}
		class MenuMasterViewModel : INotifyPropertyChanged
		{
			public ObservableCollection<MenuMasterMenuItem> MenuItems { get; set; }

			public MenuMasterViewModel()
			{
				MenuItems = new ObservableCollection<MenuMasterMenuItem>(new[]
				{
					new MenuMasterMenuItem { Id = 0, Title = "Inicio", TargetType = typeof(MenuDetail), icon="icon_inicio.png"},
					new MenuMasterMenuItem { Id = 1, Title = "Pedidos", TargetType = typeof(Venta.ListaPedidos), icon="icon_pedido.png"},
					new MenuMasterMenuItem { Id = 2, Title = "Clientes", TargetType = typeof(Cliente.ListaCliente), icon="icon_cliente.png"},
					new MenuMasterMenuItem { Id = 3, Title = "Productos", TargetType = typeof(Producto.ListaProducto), icon="icon_producto.png"},
					new MenuMasterMenuItem { Id = 4, Title = "Inventario", TargetType = typeof(Inventario.Inventario), icon="icon_inventario.png"},
				});
			}

			#region INotifyPropertyChanged Implementation
			public event PropertyChangedEventHandler PropertyChanged;
			void OnPropertyChanged([CallerMemberName] string propertyName = "")
			{
				if (PropertyChanged == null)
					return;

				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
			#endregion
		}
	}
}