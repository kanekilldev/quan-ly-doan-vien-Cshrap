using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDoanvien
{
    public partial class FrmDoanPhi : DevExpress.XtraEditors.XtraForm
    {
        public FrmDoanPhi()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from DOANPHI";
        string sqlNH = "select * from NAMHOC";
        string sqlDV = "select * from THONGTINCHUNG";
        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcDP.DataSource = dt;
            }
        }
       
        private void loadNM()
        {
            DataTable dt = con.readData(sqlNH);
            if (dt != null)
            {
                lkuNH.Properties.DataSource = dt;
                lkuNH.Properties.DisplayMember = "TENNAMHOC";
                lkuNH.Properties.ValueMember = "MANAMHOC";
            
            }
        }
        private void loadDV()
        {
            DataTable dt = con.readData(sqlDV);
            if (dt != null)
            {
                lkuDV.Properties.DataSource = dt;
                lkuDV.Properties.DisplayMember = "HOTENDV";
                lkuDV.Properties.ValueMember = "MADV";
               
            }
        }
        private void FrmDoanPhi_Load(object sender, EventArgs e)
        {
       

         
            loadData();
            loadNM();
            loadDV();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((lkuDV.EditValue == null) || (lkuDV.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập đoàn viên\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lkuDV.Focus();
                return;
            }
            if ((lkuNH.EditValue == null) || (lkuNH.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập năm học\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lkuNH.Focus();
                return;
            }
            if ((txtTDP.EditValue == null) || (txtTDP.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên đoàn phí\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTDP.Focus();
                return;
            }

            if ((txtMD.EditValue == null) || (txtMD.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập mức đóng/tháng\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMD.Focus();
                return;
            }
            if ((txtT.EditValue == null) || (txtT.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa chọn đóng bao nhiu tháng\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtT.Focus();
                return;
            }

          
            // Tính tiền
            int mucdong, sothang, tongtien;
            mucdong = Convert.ToInt32(txtMD.EditValue.ToString());
            sothang = Convert.ToInt32(txtT.EditValue.ToString());
            tongtien = mucdong * sothang;
            txtTT.Text = tongtien.ToString();

            string sqlC = "insert into DOANPHI(MADP,MADV,MANAMHOC,TENDP,GIATIEN,SOTHANG,TONGTIEN)" +

            "values (" + "N'" + con.creatId("DP", sqlR) + "'" + "," +
            "N'" + lkuDV.EditValue.ToString() + "'" + "," +
            "N'" + lkuNH.EditValue.ToString() + "'" + "," +
            "N'" + txtTDP.EditValue.ToString() + "'" + "," + 
            "N'" + txtMD.EditValue.ToString() + "'" + "," +
            "N'" + txtT.EditValue.ToString() + "'" + "," +
            "N'" + txtTT.EditValue.ToString() + "')";

            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm đoàn phí thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm đoàn phí thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMDP.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn đoàn phí để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTDP.EditValue == null) || (txtTDP.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên đoàn phí không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTDP.Focus();
                return;
            }
            if ((txtMD.EditValue == null) || (txtMD.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Mức phí/tháng không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMD.Focus();
                return;
            }
            if ((txtT.EditValue == null) || (txtT.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Số tháng không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtT.Focus();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa đoàn phí đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Tính tiền
                int mucdong, sothang, tongtien;
                mucdong = Convert.ToInt32(txtMD.EditValue.ToString());
                sothang = Convert.ToInt32(txtT.EditValue.ToString());
                tongtien = mucdong * sothang;
                txtTT.Text = tongtien.ToString();

                string sqlU = "update DOANPHI set TENDP = N'" + txtTDP.EditValue.ToString() + "',GIATIEN = N'" + txtMD.EditValue.ToString() + "',SOTHANG = N'" + txtT.EditValue.ToString() + "',TONGTIEN = N'" + txtTT.EditValue.ToString() + "', MADV = '" + lkuDV.EditValue.ToString() + "', MANAMHOC = '" + lkuNH.EditValue.ToString() + "' where MADP = '" + txtMDP.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa đoàn phí thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa đoàn phí thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMDP.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn đoàn phí để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá đoàn phí đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from DOANPHI where MADP = '" + txtMDP.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá đoàn phí thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá đoàn phí thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMDP.EditValue = null;
            txtTDP.EditValue = null;
            txtMD.EditValue = null;
            txtT.EditValue = null;
            txtTT.EditValue = null;
            txtTDP.Focus();
            txtMD.Focus();
            txtT.Focus();
            txtTT.Focus();
            lkuNH.EditValue = "";
            lkuDV.EditValue = "";
        }

        private void gcDP_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvDP.FocusedRowHandle;
            string colID = "MADP";
            string col1 = "MADV";
            string col2 = "MANAMHOC";
            string col3 = "TENDP";
            string col4 = "GIATIEN";
            string col5 = "SOTHANG";
            string col6 = "TONGTIEN";
            if ((gvDP.GetRowCellValue(row_index, colID) != null) && (gvDP.GetRowCellValue(row_index, col1) != null) && 
                (gvDP.GetRowCellValue(row_index, col2) != null) && (gvDP.GetRowCellValue(row_index, col3) != null) &&
                (gvDP.GetRowCellValue(row_index, col4) != null) && (gvDP.GetRowCellValue(row_index, col5) != null) &&
                (gvDP.GetRowCellValue(row_index, col6) != null))
            {
                txtMDP.EditValue = gvDP.GetRowCellValue(row_index, colID).ToString();
                lkuDV.EditValue = gvDP.GetRowCellValue(row_index, col1).ToString();
                lkuNH.EditValue = gvDP.GetRowCellValue(row_index, col2).ToString();
                txtTDP.EditValue = gvDP.GetRowCellValue(row_index, col3).ToString();
                txtMD.EditValue = gvDP.GetRowCellValue(row_index, col4).ToString();
                txtT.EditValue = gvDP.GetRowCellValue(row_index, col5).ToString();
                txtTT.EditValue = gvDP.GetRowCellValue(row_index, col6).ToString();
            }
        }


    }
}