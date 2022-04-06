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

namespace WpfEntityFrameworkExample2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // access DB
        CustomerEntities db;
        int pID;
        public MainWindow()
        {
            InitializeComponent();
            db = new CustomerEntities();

        }

        void ShowData()
        {
            var personList = from p in db.People
                             select p;
            
            /*var custList = from p in db.Customers
                             select p;*/
            this.dg1.ItemsSource = personList.ToList();

        }
        private void show_Click(object sender, RoutedEventArgs e)
        {
            ShowData();
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person()
            {
                Name = t1.Text,
                Address = t2.Text
            };

            db.People.Add(person);
            db.SaveChanges();
            MessageBox.Show("Success: Inserted Record");
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            int pID = int.Parse(tbDel.Text);
            var row = from p in db.People
                      where p.Id == pID
                      select p;
            Person pDel = row.SingleOrDefault();

            var res = MessageBox.Show("Are you really want to delete? ", "Delete Person", MessageBoxButton.YesNo,
                MessageBoxImage.Warning, MessageBoxResult.No);

            if (res == MessageBoxResult.Yes)
            {
                if (pDel != null)
                {
                    db.People.Remove(pDel);
                    db.SaveChanges();
                    
                }
            }
            else
            {

            }
            ShowData();
        }

        private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg1.SelectedItems.Count > 0)
            {
                Person p = (Person)dg1.SelectedItems[0];

                this.t1.Text = p.Name;
                this.t2.Text = p.Address;
                // get pID
                this.pID = p.Id;
                
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            var row = from p in db.People
                      where p.Id == pID
                      select p;
            
            Person pObj = row.SingleOrDefault();
            if (pObj != null)
            {
                pObj.Name = t1.Text;
                pObj.Address = t2.Text;
            }
            db.SaveChanges();
            MessageBox.Show("Update Success");
            ShowData();

        }
    }
}
