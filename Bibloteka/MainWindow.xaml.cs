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
        String sqlSelectQuery;
        public MainWindow()
        {
            InitializeComponent();
        }
        //List<Book> Book_list = new List<Book>();
        //Book_list.Add(new Book(text_box_book_name.Text, "A.M.", 123, false));
        //text_box_result.Text = Book_list[0].book_name + " " + Book_list[0].book_author;

        public MySqlDataReader Create_MySqlDataReader(MySqlConnection con, string SQL_code)
        {
            sqlSelectQuery = SQL_code;
            MySqlCommand cmd = new MySqlCommand(sqlSelectQuery, con);
            return cmd.ExecuteReader();
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
            sqlSelectQuery = "SELECT * FROM bibloteka.books WHERE bibloteka.books.book_name = '" + textbox_newbookNAME.Text + "' AND bibloteka.books.book_author = '" + textbox_newbookAUTHOR.Text + "'";
            MySqlDataReader dr = Create_MySqlDataReader(connection, sqlSelectQuery);

            int new_book_quantity = 0;
            int if_available = 0;
            try
            {
                 new_book_quantity = Int32.Parse(textbox_newbookQUANTITY.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (new_book_quantity > 0) { if_available = 1;}

            if (dr.Read())
            {
                int present_quantity = dr.GetInt32(dr.GetOrdinal("book_quantity"));
                int current_ID = dr.GetInt32(dr.GetOrdinal("ID"));
                dr.Close();

                sqlSelectQuery = "UPDATE bibloteka.books SET bibloteka.books.book_quantity = '" + (present_quantity + new_book_quantity).ToString() + "' WHERE bibloteka.books.ID = '" + current_ID.ToString() + "'";
                dr = Create_MySqlDataReader(connection, sqlSelectQuery);

                dr.Close();
                return;
            }
            else
            {
                dr.Close();
                sqlSelectQuery = "INSERT INTO bibloteka.books VALUES ('" + textbox_newbookID.Text + "','" + textbox_newbookNAME.Text + "','" + textbox_newbookAUTHOR.Text + "','" + textbox_newbookPAGES.Text + "','" + textbox_newbookQUANTITY.Text + "','" + if_available.ToString() + "')";
                dr = Create_MySqlDataReader(connection, sqlSelectQuery);
                dr.Close();
                return;
            }
            
        }
    }
}
