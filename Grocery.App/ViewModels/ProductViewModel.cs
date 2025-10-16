using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products { get; set; }

        Client client;

        public ProductViewModel(IProductService productService, GlobalViewModel global)
        {
            _productService = productService;
            Products = [];
            client = global.Client;
            foreach (Product p in _productService.GetAll()) Products.Add(p);
        }

        [RelayCommand]
        private void ShowNewProductView()
        {
            if (client.Role == Role.Admin)
            {
                Shell.Current.GoToAsync($"{nameof(NewProductView)}", true);
            }
        }
    }
}
