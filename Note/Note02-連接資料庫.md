# Step01 加入連接字串
## Resources.resx
![image](https://github.com/Riley-Shu/WPF-Search_Customers/blob/master/Note/image/N02-P01.png)

# Step02 建立版面初始化
```cs

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
            String queryArea = "Select DISTINCT AreaID,Area FROM Customers WHERE Area is not Null order by AreaID ASC"; //kEY
            //SqlCommand cmd = new SqlCommand(queryArea,conn);
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
            reader.Close();
        }
```
- 注意: 記得確認語法
![image](https://github.com/Riley-Shu/WPF-Search_Customers/blob/master/Note/image/N02-P02.png)
