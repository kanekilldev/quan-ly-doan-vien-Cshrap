using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDoanvien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Khai báo
        SqlConnection conn;
        string randomCode;
        public static String to;


        //nút thoát 
        private void Label1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private StringBuilder encrypt(String str)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] hash = md5.ComputeHash(inputBytes);
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x"));
            }
            return sb;
        }
        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-J737GAT\SQLEXPRESS;Initial Catalog=QLDV;Integrated Security=True");

            try
            {
                conn.Open();
                string tk = txtTaikhoan.Text;
                string mk = encrypt(txtMatkhau.Text.ToString()).ToString();
                string sql = "select *from TAIKHOAN where TAIKHOAN='" + tk + "' and MATKHAU ='" + mk + "'";
                SqlCommand Cmd = new SqlCommand(sql, conn);
                SqlDataReader dta = Cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    if (randomCode == (txtOTP.Text).ToString())
                    {
                        to = txtTaikhoan.Text;
                        FrmMain rp = new FrmMain();
                        this.Hide();
                        rp.Show();
                    }
                    else
                    {
                        MessageBox.Show("Mã OTP đã sử dụng vui lòng lấy mã OTP khác");
                    }
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void BtnLayOTP_Click(object sender, EventArgs e)
        {
            String from, pass, messageBody;
            Random rand = new Random();
            randomCode = (rand.Next(999999)).ToString();
            MailMessage message = new MailMessage();
            to = (txtTaikhoan.Text).ToString();
            from = "doanviencontract@gmail.com";
            pass = "hvmosrgdbfkwotra";
            messageBody = "DV-" + randomCode + " là mã xác minh tài khoản đăng nhập hết thống quản lý đoàn viên";
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "HỆ THỐNG QUẢN LÝ ĐOÀN VIÊN";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);
                MessageBox.Show("Vui lòng kiểm tra email");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
