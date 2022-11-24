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

namespace Project_batch_rename_2022
{
    /// <summary>
    /// Interaction logic for ReadMe.xaml
    /// </summary>
    public partial class ReadMe : Window
    {
        public ReadMe()
        {
            InitializeComponent();
            MainContent.Text = "Đồ án batch rename môn Lập Trình Windows - 2022 ";

            var bannerLine = new TextBlock();
            bannerLine.FontSize = 15;
            bannerLine.FontWeight = FontWeights.Bold;
            bannerLine.Text = "Thông tin chung:";

            var introduceLine = new TextBlock();
            introduceLine.Text = "Đây là đồ án xây dựng ứng dụng đổi tên file hàng loạt chạy trên nền tảng windows 7 trở lên, với phiên bản .NET 6 framework gồm các thành viên:" +
                "\n \t * 20120364 - Phạm Phước Sang" +
                "\n \t * 20120567 - Nguyễn Trần Ngọc Sương" +
                "\n \t * 20120633 - Viên Hải Yến" +
                "\n \t * 20120634 - Lê Minh Trí";
            introduceLine.TextWrapping = TextWrapping.Wrap;
            
            MainField.Children.Add(bannerLine);
            MainField.Children.Add(introduceLine);
            var bannerLine2 = new TextBlock();
            bannerLine2.FontSize = 15;
            bannerLine2.FontWeight = FontWeights.Bold;
            var introduceLine2 = new TextBlock();
            bannerLine2.Text = "Hướng dẫn sử dụng:";
            introduceLine2.Text = "1.Người dùng chọn vào nút thêm file ở Menu thêm file Items Actions"
                +"\n 2.Chọn qua tab Options để có thể sử dụng các tính năng sau:"
                +"\n \t Resolve Conflicts: Thực hiện hành động khi có file trùng gồm:" +
                "\n \t \t - Override: Ghi đè lên file đã xuất hiện" +
                "\n \t \t - Skip: Bỏ qua các file có tên đã xuất hiện trước" +
                "\n \t \t - None: Không hành động gì" +
                "\n" +
                "\n \t Batch Actions: " +
                "\n \t \t - Rename On Originals: Đổi tên file trên file gốc" +
                "\n \t \t - Move To New Folder: Di chuyển qua thư mục khác" +
                "\n \t \t - Copy to New Folder: Sao chép qua thư mục khác (Mặc định) " +
                "\n" +
                "\n 3.Chọn Rename Rules tương ứng:" +
                "\n \t - Add Counter As Suffix: Thêm counter vào cuối tên file " +
                "\n \t - Add Counter As Prefix: Thêm counter vào đầu tên file" +
                "\n \t - Add prefix: Thêm chuỗi vào đầu file" +
                "\n \t - Add suffix: Thêm chuỗi vào cuối file" +
                "\n \t - Change Extension: Đổi đuôi file" +
                "\n \t - Remove Whitespace: Loại đỏ khoảng trắng" +
                "\n \t - Lower case: Chuyển tên thành viết thường" +
                "\n \t - Pacal case: Chuyển tên thành dạng Pascal" +
                "\n \t - Remove Space: Loại bỏ khoảng trắng đầu và cuối tên file" +
                "\n \t - Replace Character: Thay thế một ký tự được chọn với một ký tự khác\n " +
                "\n 4. Chọn add rule tương ứng \n"+
                "\n 5.Bấm vào nút Starts thể thực hiện sử dụng tính năng";
            MainField.Children.Add(bannerLine2);
            MainField.Children.Add(introduceLine2);
        }

        private void backBtnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;    
        }
    }
}
