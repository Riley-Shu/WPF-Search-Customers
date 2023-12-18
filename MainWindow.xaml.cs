using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
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

using Sample06_WPF.Model;

namespace Sample06_WPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        //Data
        private SqlConnection conn; 
        private SqlCommand cmd; 

        //初始化
        public MainWindow()
        {
            InitializeComponent();

            //Part1 配置連接字串並開啟連接
            String connString = Properties.Resources.sample06; //建立連接字串物件
            conn = new SqlConnection(connString); //使用連接字串開啟資料庫連接
            conn.Open();

            //Part2 建立命令物件並儲存在SqlDataReader
            cmd = conn.CreateCommand(); //建立SqlCommand物件 執行SQL命令
            String queryArea = "Select DISTINCT AreaID,Area FROM Customers WHERE Area is not Null order by AreaID ASC"; 
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = queryArea;
            SqlDataReader reader = cmd.ExecuteReader(); //說明: 執行一個SQL查詢並將結果儲存在SqlDataReader物件中，可以從資料來源中擷取查詢結果。
            
            //Part3 封裝成離線模組並綁定資料
            List<string> resultArea = new List<string>();
            while (reader.Read())
            {
                resultArea.Add(reader["Area"].ToString()); //封裝Area欄位
            }
            comboBoxArea.ItemsSource = resultArea; //綁定comboBox

            reader.Close(); //注意: 記得關閉，否則會無法往下執行            
        }

        //Area查詢按鈕
        private void btnArea(Object sender, RoutedEventArgs e) //說明: RoutedEventArgs初始化事件
        {
            //Part1 建立命令物件並儲存在SqlDataReader
            //cmd = conn.CreateCommand();
            String area = comboBoxArea.SelectedItem as String; //取得選取項目
            String queryCompany = "Select DISTINCT Company FROM Customers WHERE Area = @area"; 

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = queryCompany;
            cmd.Parameters.Clear(); //保守原則，清除cmd參數
            cmd.Parameters.AddWithValue("Area", area); //cmd加入參數Area及物件area，提供查詢參數@area值

            SqlDataReader reader = cmd.ExecuteReader(); 

            //Part2 封裝成離線模組並綁定資料
            List<string> resultCompany = new List<string>();
            while (reader.Read())
            {
                resultCompany.Add(reader["Company"].ToString()); //封裝
            }
            comboBoxCompany.ItemsSource = resultCompany; //綁定comboBox
            
            reader.Close();
        }

        //單位查詢按鈕
        private void btnCompany(Object sender, RoutedEventArgs e)
        {
            //Part1 建立命令物件並儲存在SqlDataReader
            cmd = conn.CreateCommand();
            String company = comboBoxCompany.SelectedItem as String; //取得選取項目
            String queryCustomer = "Select CustomerID,CustomerName,Company,Area,Contact,Phone,Adress,SalesRep,Ps FROM Customers where  Company = @company";
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.CommandText = queryCustomer;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("company", company);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Customers> resultCustomer = new List<Customers>(); //kEY
            while (reader.Read())
            {
                resultCustomer.Add(
                    new Customers()
                    {

                        customerId = reader["CustomerID"].ToString(),
                        customerName = reader["CustomerName"].ToString(),
                        area = reader["Area"].ToString(),
                        company = reader["Company"].ToString(),
                        contact = reader["Contact"].ToString(),
                        phone = reader["Phone"].ToString(),
                        adress = reader["Adress"].ToString(),
                        salesRep = reader["SalesRep"].ToString(),
                        ps = reader["Ps"].ToString(),
                    }
                );
            }
            reader.Close();
            lsitBoxCustomer.ItemsSource = resultCustomer;


            Customers customers = this.lsitBoxCustomer.SelectedItem as Customers;

        }

        private void selectList (Object sender, SelectionChangedEventArgs e) //SelectionChangedEventArgs 通常用於當使用者選擇或取消選擇某個項目時， 記得葉面要綁
        {
            Customers customers = this.lsitBoxCustomer.SelectedItem as Customers;

            txtCustomerId.Text = customers.customerId;
            txtCustomerName.Text = customers.customerName;
            txtCompany.Text = customers.company;
            txtContact.Text = customers.contact;
            txtPhone.Text = customers.phone;
            txtAdress.Text = customers.adress;
            txtSalesRep.Text = customers.salesRep;
            txtPs.Text = customers.ps;

        }
    }
}
