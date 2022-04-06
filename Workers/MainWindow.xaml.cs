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

namespace Workers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dgWorkers.ItemsSource = (new models.praktikaEntities().worker).ToList();

        }

        private void bnEdit_Click(object sender, RoutedEventArgs e)
        {
            Windows.AddWorkers window = new Windows.AddWorkers(this, (sender as Button).DataContext as models.worker);
            this.Hide();
            window.ShowDialog();
        }

        private void bnDelete_Click(object sender, RoutedEventArgs e)
        {
            models.worker worker = (sender as Button).DataContext as models.worker;
            using(models.praktikaEntities db = new models.praktikaEntities())
            {
                db.Entry(worker).State = System.Data.Entity.EntityState.Deleted;
                db.worker.Remove(worker);
                db.SaveChanges();
            }
            dgWorkers.ItemsSource = (new models.praktikaEntities().worker).ToList();

        }

        private void BnAdd_Click(object sender, RoutedEventArgs e)
        {
            Windows.AddWorkers window = new Windows.AddWorkers(this);
            this.Hide();
            window.ShowDialog();
        }
    }
}
