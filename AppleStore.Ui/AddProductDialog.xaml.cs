using System.IO;
using System.Windows;
using AppleStore.Ui.Models;
using Microsoft.Win32;


namespace AppleStore.Ui
{
    /// <summary>
    /// Логика взаимодействия для AddProductDialog.xaml
    /// </summary>
    public partial class AddProductDialog : Window
    {
        public Product NewProduct { get; set; }

        public AddProductDialog()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = ProductNameTextBox.Text,
                Price = decimal.Parse(ProductPriceTextBox.Text),
                Image = LoadImage(ProductImagePathTextBox.Text)
            };
            DialogResult = true;
        }

        private byte[] LoadImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                return File.ReadAllBytes(imagePath);
            }
            return null;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ProductImagePathTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}