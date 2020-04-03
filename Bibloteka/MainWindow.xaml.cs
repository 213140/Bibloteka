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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Bibloteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=admin;password=2610Andre");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Book> Book_list = new List<Book>();
            Book_list.Add(new Book(text_box_book_name.Text, "A.M.", 123, false));
            text_box_result.Text = Book_list[0].book_name + " " + Book_list[0].book_author;
        }

        private void Connect_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    textbox_connection_status.Text = "Conected! :)";
                }
                else
                {
                    textbox_connection_status.Text = "FAIL! :(";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_getData_from_DB_Click(object sender, RoutedEventArgs e)
        {
            String sqlSelectQuery = "SELECT * FROM sys.table";
            MySqlCommand cmd = new MySqlCommand(sqlSelectQuery, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                textbox_data_from_DB.Text = dr["name"].ToString();
            }
            
        }
    }
}
