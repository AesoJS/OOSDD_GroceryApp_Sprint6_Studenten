using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class NewProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        private string productName;

        [ObservableProperty]
        private int productStock;

        [ObservableProperty]
        private DateTime productShelfLife = DateTime.Now;

        [ObservableProperty]
        private decimal productPrice;

        [ObservableProperty]
        private string productMessage;

        public NewProductViewModel(IProductService productService)
        {
            _productService = productService;
        }

        [RelayCommand]
        private void AddProduct()
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                ProductMessage = "Vul een geldige productnaam in.";
                return;
            }
            if (ProductStock < 0)
            {
                ProductMessage = "Voorraad kan niet negatief zijn.";
                return;
            }
            if (ProductPrice < 0)
            {
                ProductMessage = "Prijs kan niet negatief zijn.";
                return;
            }
            DateOnly convertedShelfLife = DateOnly.FromDateTime(ProductShelfLife);
            int highestId = _productService.GetAll().Count != 0 ? _productService.GetAll().Max(p => p.Id) : 0;
            Product newProduct = new(highestId + 1, ProductName, ProductStock, convertedShelfLife, ProductPrice);
            Product addedProduct = _productService.Add(newProduct);
            if (addedProduct != null)
            {
                ProductMessage = $"Product '{addedProduct.Name}' toegevoegd!";
                ProductName = string.Empty;
                ProductStock = 0;
                ProductPrice = 0;
                ProductShelfLife = DateTime.Now;
            }
            else
            {
                ProductMessage = "Fout bij het toevoegen van het product. Probeer het opnieuw.";
            }
        }
    }
}
