using DistribuidoraVendedores.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace DistribuidoraVendedores.ViewModels
{
	public class ListaPedidosVM : INotifyPropertyChanged
	{
		private string _buscarC;
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		private ObservableCollection<VentasNombre> _listaPedidosEnt;
		public ObservableCollection<VentasNombre> ListaPedidosEnt
		{
			get { return _listaPedidosEnt; }
			set
			{
				if (_listaPedidosEnt != value)
				{
					_listaPedidosEnt = value;
					OnPropertyChanged("ListaPedidosEnt");
				}
			}
		}
		private ObservableCollection<VentasNombre> _listaPedidosPen;
		public ObservableCollection<VentasNombre> ListaPedidosPen
		{
			get { return _listaPedidosPen; }
			set
			{
				if (_listaPedidosPen != value)
				{
					_listaPedidosPen = value;
					OnPropertyChanged("ListaPedidosPen");
				}
			}
		}
		private ObservableCollection<VentasNombre> _listaPedidosCanc;
		public ObservableCollection<VentasNombre> ListaPedidosCanc
		{
			get { return _listaPedidosCanc; }
			set
			{
				if (_listaPedidosCanc != value)
				{
					_listaPedidosCanc = value;
					OnPropertyChanged("ListaPedidosCanc");
				}
			}
		}
		public string BuscarC
		{
			get
			{
				return _buscarC;
			}
			set
			{
				_buscarC = value;
				OnPropertyChanged(nameof(BuscarC));
			}
		}
		public ListaPedidosVM()
		{
			_listaPedidosEnt = new ObservableCollection<VentasNombre>();
			_listaPedidosPen = new ObservableCollection<VentasNombre>();
			_listaPedidosCanc = new ObservableCollection<VentasNombre>();
			GetPedidos();
		}
		public async void GetPedidos()
		{
			try
			{
				_listaPedidosEnt.Clear();
				_listaPedidosPen.Clear();
				_listaPedidosCanc.Clear();

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
			}
			catch (Exception err)
			{
				Console.WriteLine("#######################################" + err.ToString() + "#################################################");
			}
		}
	}
}
