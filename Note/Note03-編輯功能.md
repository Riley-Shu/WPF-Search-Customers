# Step01 第一層查詢按鈕
## MainWindow.xaml.cs
- 編輯按鈕功能
	- 取得選取項目
	- 建立命令物件並儲存查詢功能
	- 由於選取項目會改變，`cmd.Parameters.AddWithValue("Area", area);` 綁定查詢參數
	- 封裝成離線模組並綁定資料
```cs
//Area查詢按鈕
private void btnArea(Object sender, RoutedEventArgs e) //說明: RoutedEventArgs初始化事件
{
	//Part1 建立命令物件並儲存在SqlDataReader
	String area = comboBoxArea.SelectedItem as String; //取得選取項目
	String queryCompany = "Select DISTINCT Company FROM Customers WHERE Area = @area"; 

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
```

## MainWindow.xaml
- 按鈕綁定功能 `x:Name="comboBoxArea"`
```xml
 <StackPanel Orientation="Horizontal" Margin="0,0,0,10" >
	 <TextBlock  Text="所屬地區      " TextBlock.FontSize="16" />
	 <ComboBox  Width="180" x:Name="comboBoxArea"   Margin="0,0,10,0"/>
	 <Button Content="查詢" Click="btnArea" FontSize="16"/>
 </StackPanel>
```

# Step02 第二層查詢按鈕

## Customer.cs
- 由於後續會要取得選擇項目的各項資料並封裝成模組，須先建立Class
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Sample06_WPF.Model
{
    public class Customer
    {
        public String customerId { get; set; }
        public String customerName { get; set; }
        public String Area { get; set; }
        public String company { get; set; }
        public String contact { get; set; }
        public String phone { get; set; }
        public String adress { get; set; }
        public String salesRep { get; set; }
        public String Ps { get; set; }
    }
}

```

## MainWindow.xaml.cs
- 封裝資料為Customers
```cs
//單位查詢按鈕
private void btnCompany(Object sender, RoutedEventArgs e)
{
	//Part1 建立命令物件並儲存在SqlDataReader
	String company = comboBoxCompany.SelectedItem as String; 
	String queryCustomer = "Select CustomerID,CustomerName,Company,Area,Contact,Phone,Adress,SalesRep,Ps FROM Customers where  Company = @company";

	cmd.CommandText = queryCustomer;
	cmd.Parameters.Clear();
	cmd.Parameters.AddWithValue("company", company);

	SqlDataReader reader = cmd.ExecuteReader();

	//Part2 封裝成離線模組並綁定資料
	List<Customers> resultCustomer = new List<Customers>(); //建立List參考Model
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
}
```

## MainWindow.xaml
- 綁定事件
```cs
     <StackPanel Orientation="Horizontal">
         <TextBlock  Text="客戶單位      " TextBlock.FontSize="16"/>
         <ComboBox  Width="180" x:Name="comboBoxCompany" Margin="0,0,10,0"/>
         <Button Content="查詢" Click="btnCompany" FontSize="16"/>
     </StackPanel>
     <TextBlock Text="客戶名稱" FontSize="16" Margin="0,30,0,0"/>
 </StackPanel>
```

# Step03 選擇ListBox項目
## MainWindow.xaml.cs
- 使用SelectionChangedEventArgs
- 綁定資料
```cs
  //ListBox顯示
        private void selectList (Object sender, SelectionChangedEventArgs e) //說明: SelectionChangedEventArgs是一個特定的事件參數，它包含與選擇更改事件相關的狀態信息和事件數據。通常用於當使用者選擇或取消選擇某個項目時。
        {
            Customers customers = this.lsitBoxCustomer.SelectedItem as Customers; //綁定項目

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
```

## MainWindow.xaml
- 綁定ListBoc和TextBox
```cs
<ListBox x:Name="lsitBoxCustomer" d:ItemsSource="{d:SampleData ItemCount=5}" Margin="206,188,478,75" SelectionChanged="selectList" FontSize="16"/>

<StackPanel Orientation="Vertical" Margin="544,0,36,0" >
	<StackPanel Orientation="Horizontal"  Margin="50,180,0,8">
		<TextBlock  Text="客戶編號     "  VerticalAlignment="Center" TextBlock.FontSize="16"/>
		<TextBox Width="235" x:Name="txtCustomerId" Height="25" FontSize="14"/>
	</StackPanel>

	<StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
		<TextBlock  Text="客戶名稱     " TextBlock.FontSize="16"  VerticalAlignment="Center" />
		<TextBox Width="235"  Height="25"  x:Name="txtCustomerName" FontSize="14"/>
	</StackPanel>

	<StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
		<TextBlock  Text="客戶單位     " TextBlock.FontSize="16"/>
		<TextBox Width="235"  x:Name="txtCompany" Height="25" FontSize="14"/>
	</StackPanel>

	<StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
		<TextBlock  Text="聯  絡  人     "  TextBlock.FontSize="16"/>
		<TextBox Width="235" x:Name="txtContact" Height="25" FontSize="14"/>
	</StackPanel>

	<StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
		<TextBlock  Text="聯絡方式     " TextBlock.FontSize="16" />
		<TextBox Width="235" x:Name="txtPhone" Height="25" FontSize="14"/>
	</StackPanel>

	<StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
		<TextBlock  Text="送貨地址     " TextBlock.FontSize="16"/>
		<TextBox Width="235"  x:Name="txtAdress" Height="25" FontSize="14"/>
	</StackPanel>

	<StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
		<TextBlock  Text="負責業務     " TextBlock.FontSize="16"/>
		<TextBox Width="235"  x:Name="txtSalesRep" Height="25" FontSize="14"/>
	</StackPanel>

	<StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
		<TextBlock  Text="備        註     " TextBlock.FontSize="16" TextAlignment="Justify"/>
		<TextBox Width="235"  x:Name="txtPs" Height="25" FontSize="14"/>
	</StackPanel>
</StackPanel>
```

![image](https://github.com/Riley-Shu/WPF-Search_Customers/blob/master/Note/image/N03-P01.png)
## 補充說明
RoutedEventArgs 和 SelectionChangedEventArgs 都是 WPF 中的事件參數。

- RoutedEventArgs 是一個基本的事件參數，它包含與路由事件相關的狀態信息和事件數據。
- SelectionChangedEventArgs 是一個特定的事件參數，它包含與選擇更改事件相關的狀態信息和事件數據。
  SelectionChangedEventArgs 通常用於當使用者選擇或取消選擇某個項目時，例如在 ListBox 或 ComboBox 控制項中。如果您需要進一步的幫助，請參閱 Microsoft 的官方文件或請求專業人員的協助。

# Step04 調整版面
```xml
<Window x:Class="Sample06_WPF.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Sample06_WPF"
        mc:Ignorable="d"
        Title="客戶查詢" Height="550" Width="1000">

    <Grid Background="Azure">

        <StackPanel Margin="0,0,830,0" Background="CadetBlue">
            <Button Content="新增" Height="30" Width="114" Margin="25,60,10,10" TextBlock.FontSize="14" />
            <Button Content="修改" Height="30" Width="114"  Margin="25,10,10,10" TextBlock.FontSize="14"/>
            <Button Content="查詢" Height="30" Width="114" Margin="25,10,10,10" TextBlock.FontSize="14" />
            <Button Content="離開" Height="30" Width="114"  Margin="25,250,10,10"  TextBlock.FontSize="14" RenderTransformOrigin="0.563,-6.03" />
        </StackPanel>

        <StackPanel Orientation="Vertical" Margin="206,69,404,297">
            <StackPanel Orientation="Horizontal" Margin="0,0,0,10" >
                <TextBlock  Text="所屬地區      " TextBlock.FontSize="16" />
                <ComboBox  Width="180" x:Name="comboBoxArea"   Margin="0,0,10,0"/>
                <Button Content="查詢" Click="btnArea" FontSize="16"/>
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <TextBlock  Text="客戶單位      " TextBlock.FontSize="16"/>
                <ComboBox  Width="180" x:Name="comboBoxCompany" Margin="0,0,10,0"/>
                <Button Content="查詢" Click="btnCompany" FontSize="16"/>
            </StackPanel>
            <TextBlock Text="客戶名稱" FontSize="16" Margin="0,30,0,0"/>
        </StackPanel>

        <ListBox x:Name="lsitBoxCustomer" d:ItemsSource="{d:SampleData ItemCount=5}" Margin="206,188,478,75" SelectionChanged="selectList" FontSize="16"/>

        <StackPanel Orientation="Vertical" Margin="544,0,36,0" >
            <StackPanel Orientation="Horizontal"  Margin="50,180,0,8">
                <TextBlock  Text="客戶編號     "  VerticalAlignment="Center" TextBlock.FontSize="16"/>
                <TextBox Width="235" x:Name="txtCustomerId" Height="25" FontSize="14"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
                <TextBlock  Text="客戶名稱     " TextBlock.FontSize="16"  VerticalAlignment="Center" />
                <TextBox Width="235"  Height="25"  x:Name="txtCustomerName" FontSize="14"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
                <TextBlock  Text="客戶單位     " TextBlock.FontSize="16"/>
                <TextBox Width="235"  x:Name="txtCompany" Height="25" FontSize="14"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
                <TextBlock  Text="聯  絡  人     "  TextBlock.FontSize="16"/>
                <TextBox Width="235" x:Name="txtContact" Height="25" FontSize="14"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
                <TextBlock  Text="聯絡方式     " TextBlock.FontSize="16" />
                <TextBox Width="235" x:Name="txtPhone" Height="25" FontSize="14"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
                <TextBlock  Text="送貨地址     " TextBlock.FontSize="16"/>
                <TextBox Width="235"  x:Name="txtAdress" Height="25" FontSize="14"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
                <TextBlock  Text="負責業務     " TextBlock.FontSize="16"/>
                <TextBox Width="235"  x:Name="txtSalesRep" Height="25" FontSize="14"/>
            </StackPanel>

            <StackPanel Orientation="Horizontal"  Margin="50,0,0,8">
                <TextBlock  Text="備        註     " TextBlock.FontSize="16" TextAlignment="Justify"/>
                <TextBox Width="235"  x:Name="txtPs" Height="25" FontSize="14"/>
            </StackPanel>
        </StackPanel>
    </Grid>
</Window>

```


# 待增加
1. 新增功能
2. 修改功能
3. 刪除功能
4. 離開
