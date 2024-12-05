using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IbragimovI_Глазки_save
{
    /// <summary>
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        public AgentPage()
        {
            InitializeComponent();

            var currentAgent = ИбрагимовИ_ГлазкиSaveEntities.GetContext().Agent.ToList();

            AgentListView.ItemsSource = currentAgent;

            ComboType.SelectedIndex = 0;
            ComboSort.SelectedIndex = 0;

            UpdateServices();
        }

        private void UpdateServices()
        {
            var currentAgents = ИбрагимовИ_ГлазкиSaveEntities.GetContext().Agent.ToList();

           

            if (ComboType.SelectedIndex == 1)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 1).ToList();
            if (ComboType.SelectedIndex == 2)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 2).ToList();
            if (ComboType.SelectedIndex == 3)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 3).ToList();
            if (ComboType.SelectedIndex == 4)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 4).ToList();
            if (ComboType.SelectedIndex == 5)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 5).ToList();
            if (ComboType.SelectedIndex == 6)
                currentAgents = currentAgents.Where(p => p.AgentTypeID == 6).ToList();

            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Email.ToLower().Contains(TBoxSearch.Text.ToLower()) || p.Phone.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            AgentListView.ItemsSource = currentAgents.ToList();

            if (ComboSort.SelectedIndex == 1)
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Title).ToList();
            if (ComboSort.SelectedIndex == 2)
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Title).ToList();
            //if (ComboSort.SelectedIndex == 3)
            //    AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Discount).ToList();
            //if (ComboSort.SelectedIndex == 4)
            //    AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Discount).ToList();
            if (ComboSort.SelectedIndex == 5)
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Priority).ToList();
            if (ComboSort.SelectedIndex == 6)
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Priority).ToList();

     

        }


        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }
        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

    }
}
