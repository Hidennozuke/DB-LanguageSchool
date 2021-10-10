using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using static DBKArlGoja.DataFrame;

namespace DBKArlGoja
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 


    // дата добавления, дата последнего посещения, количество посещений
    public partial class MainWindow : Window
    {
        DataFrame dataFrame = new DataFrame();
        public MainWindow(){
            
            InitializeComponent();
            List228.ItemsSource = context.ClienView.ToList();

            List<Gender> genders = context.Gender.ToList();
            genders.Insert(0, new Gender() { GenderName = "Все" });
            CBGenders.ItemsSource = genders;
            CBGenders.DisplayMemberPath = "GenderName";
            CBGenders.SelectedIndex = 0;

        }

        private void TBLLname_TextChanged(object sender, TextChangedEventArgs e){
            Filter();
        }

        private void TBLFname_TextChanged(object sender, TextChangedEventArgs e){
            Filter();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e){
            Filter();
        }

       

        public void Filter()
        {
            var list = context.ClienView.Where(i => i.SName.Contains(TBLName.Text) || i.FName.Contains(TBLName.Text) || i.PName.Contains(TBLName.Text))
                .Where(i => i.Phone.Contains(PhoneBox.Text)).ToList()
                .Where(i => i.Email.Contains(MailBox.Text)).ToList();
           

            if (CBGenders.SelectedIndex == 0)
            {
                List228.ItemsSource = list;
            }
            else
            {
                var gender = CBGenders.SelectedItem as Gender;
                list = list.Where(i => i.Gender == gender.IdGender).ToList();
                List228.ItemsSource = list;
            }

            if (CrackCheck.IsChecked == true)
            {
                list = list.Where(i => i.BthDate.Month == DateTime.Now.Month).ToList();
                List228.ItemsSource = list;
            }


            List228.ItemsSource = list;
        }

        private void Genders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PhoneBox.Text = null;
            TBLName.Text = null;
            MailBox.Text = null;
            CrackCheck.IsChecked = false ;
            CBGenders.Text = "Все";

            Filter();
        }

        private void CrackCheck_Checked(object sender, RoutedEventArgs e)
        {
            Filter();

        }
    }
}
