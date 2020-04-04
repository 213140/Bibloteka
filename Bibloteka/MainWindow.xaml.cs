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
        //List<Book> Book_list = new List<Book>();
        //Book_list.Add(new Book(text_box_book_name.Text, "A.M.", 123, false));
        //text_box_result.Text = Book_list[0].book_name + " " + Book_list[0].book_author;

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
            int new_book_quantity = 0;
            int if_available = 0;
            try
            {
                 new_book_quantity = Int32.Parse(textbox_newbookQUANTITY.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (new_book_quantity > 0) { if_available = 1;}
            String sqlSelectQuery = "INSERT INTO bibloteka.books VALUES ('" + textbox_newbookID.Text + "', '" + textbox_newbookNAME.Text + "', '" + textbox_newbookAUTHOR.Text + "', '" + textbox_newbookPAGES.Text + "', '" + textbox_newbookQUANTITY.Text + "', '" + if_available.ToString() + "')";
            MessageBox.Show(sqlSelectQuery);
            MySqlCommand cmd = new MySqlCommand(sqlSelectQuery, connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            //if(dr.Read())
           // {
             //   textbox_data_from_DB.Text = dr[0].ToString() + " " + dr[1].ToString(); ;
          //  }
            
        }
    }
}
