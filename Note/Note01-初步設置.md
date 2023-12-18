# Step01 建立專案

![image](https://github.com/Riley-Shu/WebForSearchingYoubike/blob/master/Note/image/N01-P01.png])


# Step02 初步編輯版面
```html
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
            <Button Content="新增" Height="30" Width="114" Margin="25,60,10,10"/>
            <Button Content="修改" Height="30" Width="114"  Margin="25,10,10,10"/>
            <Button Content="查詢" Height="30" Width="114" Margin="25,10,10,10"/>
            <Button Content="離開" Height="30" Width="114"  Margin="25,280,10,10" Background="AliceBlue" RenderTransformOrigin="0.563,-6.03" />
        </StackPanel>
        
        <StackPanel Orientation="Vertical" Margin="206,69,0,0">
            <StackPanel Orientation="Horizontal">
                <TextBlock  Text="所屬地區" />
                <ComboBox  Width="120" />
                <Button Content="查詢"/>
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <TextBlock  Text="客戶單位" />
                <ComboBox  Width="120" />
                <Button Content="查詢"/>
            </StackPanel>
        </StackPanel>
        
        <ListBox d:ItemsSource="{d:SampleData ItemCount=5}" Margin="227,196,534,123"/>

        <StackPanel Orientation="Vertical" Margin="603,114,0,0">
            <StackPanel Orientation="Horizontal">
                <TextBlock  Text="客戶編號" />
                <TextBox Width="100"/>
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <TextBlock  Text="客戶編號" />
                <TextBox Width="100"/>
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <TextBlock  Text="客戶編號" />
                <TextBox Width="100"/>
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <TextBlock  Text="客戶編號" />
                <TextBox Width="100"/>
            </StackPanel>
        </StackPanel>
    </Grid>
</Window>

```
![image](https://github.com/Riley-Shu/WebForSearchingYoubike/blob/master/Note/image/N01-P02.png])


# Step03 建立資料庫
- 建立資料庫
- 建立Table
- 插入資料
![image](https://github.com/Riley-Shu/WebForSearchingYoubike/blob/master/Note/image/N01-P03.png])

```sql
use Sample06
 insert into Customers
 (CustomerID,CustomerName,Company,AreaID,Area,Contact,Phone,Adress,SalesRep,Ps)
 values
(N'ANU007',N'陳美美老師實驗室',N'國立01大學',N'001',N'北區',N'林曉珍',N'(02)2222222轉1240',N'台北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'無'),
(N'ANU008',N'王宏志老師實驗室',N'國立01大學',N'001',N'北區',N'陳曉珍',N'(02)2222222轉1241',N'台北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'無'),
(N'ANU009',N'林偉志老師實驗室',N'國立02大學',N'001',N'北區',N'王曉珍',N'(02)2222222轉1242',N'台北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'出貨要電話通知'),
(N'ANU010',N'陳美美老師實驗室',N'國立03大學',N'001',N'北區',N'林曉珍',N'(02)2222222轉1243',N'台北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'出貨要電話通知'),
(N'ANU011',N'鄭大佑老師實驗室',N'國立03大學',N'001',N'北區',N'鄭曉珍',N'(02)2222222轉1244',N'新北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'出貨要電話通知'),
(N'ANU012',N'王博宏老師實驗室',N'國立02大學',N'001',N'北區',N'陳曉珍',N'(02)2222222轉1245',N'新北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'出貨要電話通知'),
(N'ANU013',N'林大偉老師實驗室',N'國立01大學',N'001',N'北區',N'王曉珍',N'(02)2222222轉1246',N'新北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'無'),
(N'ANU014',N'陳麗華老師實驗室',N'國立01大學',N'001',N'北區',N'林曉珍',N'(02)2222222轉1247',N'新北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'無'),
(N'ANU015',N'王博宏老師實驗室',N'國立03大學',N'001',N'北區',N'陳曉珍',N'(02)2222222轉1248',N'新北市北區OO路OO號OO院O樓OOO室',N'陳孟軒',N'無'),
(N'ANU016',N'王博宏老師實驗室',N'國立04大學',N'002',N'中區',N'陳曉珍',N'(06)2222222轉1249',N'台中市北區OO路OO號OO院O樓OOO室',N'王大明',N'無'),
(N'ANU017',N'林大偉老師實驗室',N'國立05大學',N'002',N'中區',N'王曉珍',N'(06)2222222轉1250',N'台中市北區OO路OO號OO院O樓OOO室',N'王大明',N'出貨要電話通知'),
(N'ANU001',N'王博宏老師實驗室',N'國立06大學',N'003',N'南區',N'陳曉珍',N'(06)2222222轉1234',N'台南市北區OO路OO號OO院O樓OOO室',N'林秀明',N'出貨要電話通知'),
(N'ANU002',N'林大偉老師實驗室',N'國立06大學',N'003',N'南區',N'王友志',N'(06)2222222轉1235',N'高雄市北區OO路OO號OO院O樓OOO室',N'林秀明',N'無'),
(N'ANU003',N'陳麗華老師實驗室',N'國立06大學',N'003',N'南區',N'林真珍',N'(06)2222222轉1236',N'高雄市北區OO路OO號OO院O樓OOO室',N'林秀明',N'無'),
(N'ANU004',N'鄭大佑老師實驗室',N'國立07大學',N'003',N'南區',N'鄭筱曉',N'(06)2222222轉1237',N'高雄市北區OO路OO號OO院O樓OOO室',N'林秀明',N'無'),
(N'ANU005',N'洪鴻宏老師實驗室',N'國立08大學',N'003',N'南區',N'陳寬宥',N'(06)2222222轉1238',N'高雄市北區OO路OO號OO院O樓OOO室',N'林秀明',N'無'),
(N'ANU006',N'林大佑老師實驗室',N'國立09大學',N'003',N'南區',N'王曉珍',N'(06)2222222轉1239',N'台南市北區OO路OO號OO院O樓OOO室',N'林秀明',N'無'),
(N'ANU018',N'陳麗華老師實驗室',N'國立OO大學',N'004',N'東區',N'林曉珍',N'(06)2222222轉1251',N'台東市北區OO路OO號OO院O樓OOO室',N'王大明',N'出貨要電話通知')
```
