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
    public partial class FrmSoDoan : DevExpress.XtraEditors.XtraForm
    {
        public FrmSoDoan()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from SODOAN";
        string sqlDV = "select * from THONGTINCHUNG";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcSD.DataSource = dt;
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
        private void FrmSoDoan_Load(object sender, EventArgs e)
        {
            loadData();
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
            if ((txtNNS.EditValue == null) || (txtNNS.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập ngày nhận sổ\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNNS.Focus();
                return;
            }
         
          /*  bool check = false;
            string sql = "select MADV from SODOAN";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (lkuDV.EditValue.ToString().Trim().Equals(dr["MADV"].ToString()))
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (check)
            {
                XtraMessageBox.Show("Sổ đoàn có mã đoàn viên \"" + lkuDV.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng chọn kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lkuDV.EditValue = null;
                lkuDV.Focus();
                return;
            }*/

            string sqlC = "insert into SODOAN(MASODOAN,MADV,NGAYNHAN,NGAYCHUYEN,GHICHU)" +

            "values (" + "N'" + con.creatId("SD", sqlR) + "'" + "," +
            "N'" + lkuDV.EditValue.ToString() + "'" + "," +
            "N'" + txtNNS.EditValue.ToString() + "'" + "," +
            "N'" + txtNCS.EditValue.ToString() + "'" + "," +
            "N'" + txtGC.EditValue.ToString() + "')";

            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm sổ đoàn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm sổ đoàn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMSD.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn sổ đoàn để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtNNS.EditValue == null) || (txtNNS.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Ngày nhận sổ đoàn không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNNS.Focus();
                return;
            }
         
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa sổ đoàn đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update SODOAN set NGAYNHAN = N'" + txtNNS.EditValue.ToString() + "',NGAYCHUYEN = N'" + txtNCS.EditValue.ToString() + "', GHICHU = '" + txtGC.EditValue.ToString() + "', MADV = '" + lkuDV.EditValue.ToString() + "' where MASODOAN = '" + txtMSD.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa sổ đoàn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa sổ đoàn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMSD.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn sổ đoàn để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá sổ đoàn đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from SODOAN where MASODOAN = '" + txtMSD.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá sổ đoàn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá sổ đoàn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMSD.EditValue = null;
            txtNNS.EditValue = null;
            txtNCS.EditValue = null; 
            txtGC.EditValue = null;
            txtNNS.Focus();
            txtNCS.Focus();
            txtGC.Focus();
            lkuDV.EditValue = "";
        }

        private void gcSD_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvSD.FocusedRowHandle;
            string colID = "MASODOAN";
            string col1 = "MADV";
            string col2 = "NGAYNHAN";
            string col3 = "NGAYCHUYEN";
            string col4 = "GHICHU";
            if ((gvSD.GetRowCellValue(row_index, colID) != null) && (gvSD.GetRowCellValue(row_index, col1) != null) && (gvSD.GetRowCellValue(row_index, col2) != null) && (gvSD.GetRowCellValue(row_index, col3) != null) && (gvSD.GetRowCellValue(row_index, col4) != null))
            {
                txtMSD.EditValue = gvSD.GetRowCellValue(row_index, colID).ToString();
                txtNNS.EditValue = gvSD.GetRowCellValue(row_index, col2).ToString();
                txtNCS.EditValue = gvSD.GetRowCellValue(row_index, col3).ToString();
                txtGC.EditValue = gvSD.GetRowCellValue(row_index, col4).ToString();
                lkuDV.EditValue = gvSD.GetRowCellValue(row_index, col1).ToString();
            }

        }
    }
}