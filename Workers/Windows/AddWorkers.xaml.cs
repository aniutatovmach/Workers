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

namespace Workers.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddWorkers.xaml
    /// </summary>
    public partial class AddWorkers : Window
    {
        private static MainWindow _prevWindow;
        private models.worker _worker;
        public AddWorkers(MainWindow window, models.worker worker = null)
        {
            InitializeComponent();
            _prevWindow = window;
            _worker = worker;
            if (_worker != null)
            {
                TbName.Text = _worker.Name;
                TbDepartment.Text = _worker.Department;
                TbPost.Text = _worker.Post;
                TbPayCheck.Text = Convert.ToString(_worker.PayCheck);
            }
        }

        private void BnAddWorkers_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                using (models.praktikaEntities db = new models.praktikaEntities())
                {
                    db.worker.Add(new models.worker()
                    {
                        Name = TbName.Text,
                        Department = TbDepartment.Text,
                        Post = TbPost.Text,
                        PayCheck = Convert.ToInt32(TbPayCheck.Text)

                    });
                    db.SaveChanges();
                }
                MessageBox.Show("Добавление выполнено");
                this.Hide();
                _prevWindow.dgWorkers.ItemsSource = (new models.praktikaEntities().worker).ToList();
                _prevWindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Добавление провалено");

            }
        }

        private void BnEditWorkers_Click(object sender, RoutedEventArgs e)
        {
            using (models.praktikaEntities db = new models.praktikaEntities())
            {
                db.worker.Attach(_worker);

                TbName.Text = _worker.Name;
                TbDepartment.Text = _worker.Department;
                TbPost.Text = _worker.Post;
                TbPayCheck.Text = Convert.ToString(_worker.PayCheck);
                db.SaveChanges();
            }
            this.Hide();
            _prevWindow.dgWorkers.ItemsSource = (new models.praktikaEntities().worker).ToList();
            _prevWindow.ShowDialog();
        }
    }
}
