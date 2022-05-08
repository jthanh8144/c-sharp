using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using Emgu.CV;
using Emgu.CV.Structure;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace SBC
{
    public partial class Form1 : Form
    {
        // Biến cho biết dữ liệu có đang load hay không
        bool isLoading = true;
        // Biến cho biết có đang chỉnh sửa hay không
        bool isEditing = false;

        public Form1()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            // Load dữ liệu từ database lên view
            DataTable my_data = Program.Database.Query("SELECT * FROM Sach");
            dataGridView1.DataSource = my_data;
            dataGridView3.DataSource = my_data;
            my_data = Program.Database.Query("SELECT * FROM MuonSach");
            dataGridView2.DataSource = my_data;

            // Sau khi load xong data thì trả về false
            isLoading = false;

            // Gán header cho các bảng của chương trình
            dataGridView1.Columns[0].HeaderText = "Mã sách";
            dataGridView1.Columns[1].HeaderText = "Tên sách";
            dataGridView1.Columns[2].HeaderText = "Vị trí";
            dataGridView1.Columns[3].HeaderText = "Số lượng";
            dataGridView2.Columns[1].HeaderText = "Mã sách";
            dataGridView2.Columns[2].HeaderText = "Ngày mượn";
            dataGridView2.Columns[3].HeaderText = "Ngày trả";
            dataGridView3.Columns[0].HeaderText = "Mã sách";
            dataGridView3.Columns[1].HeaderText = "Tên sách";
            dataGridView3.Columns[2].HeaderText = "Vị trí";
            dataGridView3.Columns[3].HeaderText = "Số lượng";
        }

        // Hàm khởi tạo dữ liệu chạy khi bật chương trình
        private void Form1_Load_1(object sender, EventArgs e)
        {
            loadData();
            cbbSearch.SelectedIndex = 1;
            isLoading = false;

            // Đọc tất cả các camera được gắn trong laptop rồi thêm vào combobox
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filterInfoCollection)
            {
                cbbCamera.Items.Add(filterInfo.Name);
            }
            cbbCamera.SelectedIndex = 0;
            btnCloseCam.Enabled = false;
        }

        // Sự kiện khi ô search có thay đổi (tức là đang search hay không)
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            isLoading = true;
            // Nếu không search
            if (tbSearch.Text == "")
            {
                // Thì trả về tất cả data
                DataTable my_data = Program.Database.Query("SELECT * FROM Sach");
                dataGridView1.DataSource = my_data;
            }
            else
            {
                // Ngược lại thì trả về các data theo tìm kiếm
                String query = "";
                // Nếu đang tìm theo mã sách thì sinh ra query mã sách, ngược lại thì sinh ra query tên sách
                if (cbbSearch.SelectedIndex == 0)
                {
                    query = "SELECT * FROM Sach WHERE Masach LIKE '%" + tbSearch.Text + "%'";
                }
                else
                {
                    query = "SELECT * FROM Sach WHERE Tensach LIKE '%" + tbSearch.Text + "%'";
                }
                // Thực thi query rồi gán lên view
                DataTable my_data = Program.Database.Query(query);
                dataGridView1.DataSource = my_data;
            }
            isLoading = false;
        }

        // Khi thay đổi cái hàng được chọn trên bảng thì ảnh bản đồ sẽ thay đổi
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Khi mà đã load dữ liệu xong
            if (isLoading == false)
            {
                // Load ảnh từ trong folder ra để hiển thị
                pictureBox5.Image = Image.FromFile(System.IO.Directory.GetCurrentDirectory()
                    + @"\Image\" + dataGridView1.SelectedRows[0].Cells[2].Value.ToString() + ".png");
                // Nếu trong kho hết sách (số lượng bằng 0) thì sẽ k cho bấm mượn nữa và ngược lại
                if (Program.Database.GetNumberOfBook(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) < 1)
                {
                    btnMuon.Enabled = false;
                }
                else
                {
                    btnMuon.Enabled = true;
                }
            }
        }
        
        // Sự kiện thêm hoặc sửa sách
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Nếu như đang không sửa thông tin sách thì là thêm sách
            if (isEditing == false)
            {
                if (txtMaSach.Text != "" && txtTenSach.Text != "" && txtViTri.Text != "" && txtSL.Text != "")
                {
                    String sql = "INSERT INTO Sach VALUES ('"
                         + txtMaSach.Text + "', '"
                         + txtTenSach.Text + "', '"
                         + txtViTri.Text + "', '"
                         + txtSL.Text + "')";
                    Program.Database.NonQuery(sql);
                }
            }
            // Ngược lại là sửa sách
            else
            {
                if (txtTenSach.Text != "" && txtViTri.Text != "" && txtSL.Text != "")
                {
                    String sql = "UPDATE Sach SET Tensach = '"
                         + txtTenSach.Text + "', Vitri = '"
                         + txtViTri.Text + "', Soluong = '"
                         + txtSL.Text + "' WHERE Masach = '"
                         + txtMaSach.Text + "'";
                    Program.Database.NonQuery(sql);

                    // sau khi chỉnh sửa xong thì cho các nút quay về ban đầu
                    btnThem.Text = "Thêm";
                    btnSua.Text = "Sửa";
                    isEditing = false;
                    txtMaSach.Enabled = true;
                }
            }
            isLoading = true;
            loadData();
            // clear các ô input
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtViTri.Text = "";
            txtSL.Text = "";
        }

        // Khi bấm vào nút sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (isEditing == false)
            {
                // Nếu đang k sửa thì chỉnh qua chế độ sửa
                isEditing = true;
                txtMaSach.Text = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                txtTenSach.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
                txtViTri.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
                txtSL.Text = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
                txtMaSach.Enabled = false;
                btnThem.Text = "Lưu";
                btnSua.Text = "Hủy";
            }
            else
            {
                // Nếu đang sửa thì hủy chế độ sửa
                btnThem.Text = "Thêm";
                btnSua.Text = "Sửa";
                isEditing = false;
                txtMaSach.Enabled = true;
                txtMaSach.Text = "";
                txtTenSach.Text = "";
                txtViTri.Text = "";
                txtSL.Text = "";
            }
        }

        // Bấm nút xóa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Hiện ra thông báo là: "Bạn có chắc muốn xóa sách này không?"
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa sách này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (dr)
            {
                // Nếu bấm có thì vào database xóa đi rồi chỉnh các nút và clear các input
                case DialogResult.Yes:
                    String query = "DELETE FROM Sach WHERE Masach = '" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'";
                    Program.Database.NonQuery(query);
                    isLoading = true;
                    loadData();
                    btnThem.Text = "Thêm";
                    btnSua.Text = "Sửa";
                    isEditing = false;
                    txtMaSach.Enabled = true;
                    txtMaSach.Text = "";
                    txtTenSach.Text = "";
                    txtViTri.Text = "";
                    txtSL.Text = "";
                    break;
                // Nếu bấm không thì không làm gì hết
                case DialogResult.No:
                    break;
            }
            
        }

        private void btnMuon_Click(object sender, EventArgs e)
        {
            // Nếu như mà cái ô hiện mã sách thuê k có (tức là k dùng camera để quét QR) thì sẽ dùng cái sách đang được lựa chọn
            // trên cái bảng
            String maSach = "";
            if (txtMS.Text == "")
            {
                maSach = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
            else
            {
                maSach = txtMS.Text;
            }
            // Tạo query
            String query = "INSERT INTO MuonSach (Masach, Ngaymuon, Ngaytra) VALUES ('"
                + maSach + "', '" + DateTime.UtcNow.Date.ToString("MM/dd/yyyy") + "', '')";
            Program.Database.NonQuery(query);
            // Trừ số lượng cuốn sách được chọn đi 1
            int sl = Program.Database.GetNumberOfBook(maSach) - 1;
            // Update số lượng cuốn sách đó trong database
            query = "UPDATE Sach SET Soluong = '" + sl + "' WHERE Masach = '" + maSach + "'";
            Program.Database.NonQuery(query);
            isLoading = true;
            loadData();
            txtMS.Text = "";
        }

        // Nếu mà trên bảng đã có ngày trả thì không cho trả sách nữa
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading == false)
            {

                if (dataGridView2.SelectedRows[0].Cells[3].Value.ToString() == "")
                {
                    btnTra.Enabled = true;
                }
                else
                {
                    btnTra.Enabled = false;
                }
            }
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            // Lấy mã sách đang được chọn trong bảng
            String maSach = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            // Tăng số lượng sách
            int sl = Program.Database.GetNumberOfBook(maSach) + 1;
            // Update vào database
            String query = "UPDATE Sach SET Soluong = '" + sl + "' WHERE Masach = '" + maSach + "'";
            Program.Database.NonQuery(query);
            query = "UPDATE MuonSach SET Ngaytra = '" + DateTime.UtcNow.Date.ToString("MM/dd/yyyy")
                + "' WHERE ID = " + dataGridView2.SelectedRows[0].Cells[0].Value.ToString() + "";
            Program.Database.NonQuery(query);
            isLoading = true;
            loadData();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // Nếu như 1 trong 2 ô được tick thì mới thống kê được
            if (chbTenSach.Checked == true || chbNgay.Checked == true)
            {
                // Tạo query mặc định
                String query = "SELECT ms.Masach, s.Tensach, ms.Ngaymuon, ms.Ngaytra FROM MuonSach as ms INNER JOIN Sach as s ON ms.Masach = s.Masach WHERE ";
                bool status = false;
                // Nếu như chọn ngày thì thêm vô query ngày đó
                if (chbNgay.Checked == true)
                {
                    String ngay = dateTimePicker2.Value.ToString("MM/dd/yyyy");
                    query = query + "(ms.Ngaymuon = '" + ngay + "' OR ms.Ngaytra = '" + ngay + "')";
                    status = true;
                }
                // Nếu như chọn tên sách thì thêm vô query tên sách đó
                if (chbTenSach.Checked == true)
                {
                    if (status == true)
                    {
                        query += " AND ";
                    }
                    query = query + "s.Tensach LIKE '%" + txtTKTenSach.Text + "%'";
                }
                dataGridView4.DataSource = Program.Database.Query(query);
                // Gán lại các header cho có dấu
                dataGridView4.Columns[0].HeaderText = "Mã sách";
                dataGridView4.Columns[1].HeaderText = "Tên sách";
                dataGridView4.Columns[2].HeaderText = "Ngày mượn";
                dataGridView4.Columns[3].HeaderText = "Ngày trả";
            }
        }

        // Biến lưu các camera được kết nối với laptop
        FilterInfoCollection filterInfoCollection;
        // Biến lưu cái camera được chọn
        VideoCaptureDevice captureDevice;

        private void button1_Click(object sender, EventArgs e)
        {
            // Khi bấm mở camera thì nó lấy cái device đang được chọn trong combobox
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cbbCamera.SelectedIndex].MonikerString);
            // Tạo cái frame mới gọi hàm captureDevice_NewFrame
            captureDevice.NewFrame += captureDevice_NewFrame;
            // Bắt đầu bật camera
            captureDevice.Start();
            // Bắt đầu đếm thời gian mỗi lần quét QR
            timer1.Start();
            button1.Enabled = false;
            btnCloseCam.Enabled = true;
        }

        // Hàm sinh ra do thư viện
        void captureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Gán cái ô để hiển thị camera trên form thành hình đọc được từ camera
            khung_hinh.Image = (Bitmap) eventArgs.Frame.Clone();
        }

        private void btnCloseCam_Click(object sender, EventArgs e)
        {
            // Khi mà bấm stop, nếu camera đang chạy thì stop camera và gán khung hình bằng rỗng
            if (captureDevice.IsRunning)
            {
                captureDevice.Stop();
                khung_hinh.Image = null;
                button1.Enabled = true;
                btnCloseCam.Enabled = false;
            }
        }

        // Khi mà đóng chương trình, nếu camera đang ở thì đóng camera
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (captureDevice != null)
            {
                if (captureDevice.IsRunning)
                {
                    captureDevice.Stop();
                }
            }
        }

        // Hàm chạy mỗi 1 khoảng giờ gian nhất định
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Nếu camera đang mở
            if (khung_hinh.Image != null)
            {
                // Tạo đối tượng để đọc QR (đối tượng này cung cấp bởi thư viện)
                BarcodeReader barcodeReader = new BarcodeReader();
                // Giải mã kết quả đọc được từ cái khung hình
                Result result = barcodeReader.Decode((Bitmap) khung_hinh.Image);
                // Nếu đọc được kết quả từ QR
                if (result != null)
                {
                    // thì hiển thị kết quả lên cái ô
                    txtMS.Text = result.ToString();
                    // Dừng đếm thời gian
                    timer1.Stop();
                    // Tắt camera
                    if (captureDevice.IsRunning)
                    {
                        captureDevice.Stop();
                        khung_hinh.Image = null;
                        button1.Enabled = true;
                        btnCloseCam.Enabled = false;
                    }
                }
            }
        }
    }
}
