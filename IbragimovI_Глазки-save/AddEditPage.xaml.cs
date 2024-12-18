using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace IbragimovI_Глазки_save
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Agent _currentAgent = new Agent();
        private ProductSale currentProductSale = new ProductSale();
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();

            if (SelectedAgent != null)
            {
                _currentAgent = SelectedAgent;

                ComboType.SelectedIndex = _currentAgent.AgentTypeID - 1;
            }


            var currentProductSale = ИбрагимовИ_ГлазкиSaveEntities.GetContext().ProductSale.Where(p => p.AgentID == _currentAgent.ID).ToList();
            ProductSaleListView.ItemsSource = currentProductSale;
 
            DataContext = _currentAgent;


            ComboSerchAdd.ItemsSource = ИбрагимовИ_ГлазкиSaveEntities.GetContext().Product.ToList();
            ComboSerchAdd.DisplayMemberPath = "Title";
            ComboSerchAdd.SelectedValuePath = "ID";

        }

        public void UpdateProductSale()
        {
            var currentProductSale = ИбрагимовИ_ГлазкиSaveEntities.GetContext().ProductSale.Where(p => p.AgentID == _currentAgent.ID).ToList();
            //currentProductSale = currentProductSale.Where(p => p.ProductIdText.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList(); //для поиска продажи

            ProductSaleListView.ItemsSource = currentProductSale.ToList();

        }

        private void AddBtnPro_Click(object sender, RoutedEventArgs e)
        {
            var selectedProductId = ComboSerchAdd.SelectedValue;
            var selectedProductCount = string.IsNullOrWhiteSpace(ProdCount.Text) ? 0 : Convert.ToInt32(ProdCount.Text);
            var selectedDate = Date.SelectedDate;

            StringBuilder errors = new StringBuilder();
            if (selectedProductId == null || string.IsNullOrWhiteSpace(selectedProductId.ToString()))
                errors.AppendLine("Укажите название!");
            if (selectedDate == null)
                errors.AppendLine("Укажите дату!");
            if (Convert.ToInt32(selectedProductCount) <= 0)
                    errors.AppendLine("Укажите количсетво корректно!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            ProductSale newSale = new ProductSale
            {
                AgentID = _currentAgent.ID, 
                ProductID = Convert.ToInt32(selectedProductId), 
                ProductCount = selectedProductCount,
                SaleDate = selectedDate.Value 
            };

  

            if (currentProductSale.ID == 0)
                ИбрагимовИ_ГлазкиSaveEntities.GetContext().ProductSale.Add(newSale);
            try
            {
                ИбрагимовИ_ГлазкиSaveEntities.GetContext().SaveChanges();
                UpdateProductSale();
                ComboSerchAdd.SelectedIndex = -1; 
                ProdCount.Clear(); 
                Date.SelectedDate = null; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DelBtnPro_Click(object sender, RoutedEventArgs e)
        {
            if (ProductSaleListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите продукт для удаления!");
                return;
            }
            else if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    foreach (ProductSale products in ProductSaleListView.SelectedItems)
                    {
                        ИбрагимовИ_ГлазкиSaveEntities.GetContext().ProductSale.Remove(products);
                    }

                    ИбрагимовИ_ГлазкиSaveEntities.GetContext().SaveChanges();
                    UpdateProductSale();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {


                string path = myOpenFileDialog.FileName;

                // Находим индекс слова "agents"
                int index = path.LastIndexOf("agents");

                // Извлекаем подстроку, начиная от слова "agents"
                path = "\\" + path.Substring(index);


                _currentAgent.Logo = path;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
                errors.AppendLine("Укажите ФИО директора");
            if (ComboType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if (_currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            else
            {
                if (_currentAgent.INN.Length != 10)
                    errors.AppendLine("Укажите правильно ИНН агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            else
            {
                if (_currentAgent.KPP.Length != 9)
                    errors.AppendLine("Укажите правильно КПП агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
                string ph = _currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно телефон агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
                errors.AppendLine("Укажите почту агента");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;

            if (_currentAgent.ID == 0)
                ИбрагимовИ_ГлазкиSaveEntities.GetContext().Agent.Add(_currentAgent);
            try
            {
                ИбрагимовИ_ГлазкиSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var _currentAgent = (sender as Button).DataContext as Agent;

            var currentProductSale = ИбрагимовИ_ГлазкиSaveEntities.GetContext().ProductSale.ToList();
            currentProductSale = currentProductSale.Where(p => p.AgentID == _currentAgent.ID).ToList();

            var currentAgentPriorityHistory = ИбрагимовИ_ГлазкиSaveEntities.GetContext().AgentPriorityHistory.ToList();
            var currentShop = ИбрагимовИ_ГлазкиSaveEntities.GetContext().Shop.ToList();
            currentAgentPriorityHistory = currentAgentPriorityHistory.Where(p => p.AgentID == _currentAgent.ID).ToList();
            currentShop = currentShop.Where(p => p.AgentID == _currentAgent.ID).ToList();


            if (currentProductSale.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, так как существует история реализации продуктов");
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        ИбрагимовИ_ГлазкиSaveEntities.GetContext().Agent.Remove(_currentAgent);

                        if (currentAgentPriorityHistory.Count != 0)
                        {
                            for (int i = 0; currentAgentPriorityHistory.Count == i; i++)
                                ИбрагимовИ_ГлазкиSaveEntities.GetContext().AgentPriorityHistory.Remove(currentAgentPriorityHistory[i]);
                        }
                        if (currentShop.Count != 0)
                        {
                            for (int i = 0; currentShop.Count == i; i++)
                                ИбрагимовИ_ГлазкиSaveEntities.GetContext().Shop.Remove(currentShop[i]);
                        }
                        ИбрагимовИ_ГлазкиSaveEntities.GetContext().SaveChanges();

                        MessageBox.Show("Информация удалена!");
                        Manager.MainFrame.GoBack();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        //private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e) //поиск продажи
        //{
        //    UpdateProductSale(); 
        //}
    }
}
