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
using System.Windows.Shapes;

namespace IbragimovI_Глазки_save
{
    /// <summary>
    /// Логика взаимодействия для EditPriorityWindow.xaml
    /// </summary>
    public partial class EditPriorityWindow : Window
    {
        public EditPriorityWindow(int maxPriority)
        {
            InitializeComponent();
            TBoxPriority.Text = maxPriority.ToString();
        }

        private void EditPriorityBtn_Click(object sender, RoutedEventArgs e)
        {
            
            this.DialogResult = true;
        }

        //public int NewPriority
        //{
        //    get
        //    {
        //        if (Convert.ToInt32(TBoxPriority) <= 0)
        //            MessageBox.Show("Введите значение больше 0!");

        //        else 
        //            return 1;
        //        else
        //            return Convert.ToInt32(TBoxPriority.Text);
        //    }
        //}
    }
}
