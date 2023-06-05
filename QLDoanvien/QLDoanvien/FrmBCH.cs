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
    public partial class FrmBCH : DevExpress.XtraEditors.XtraForm
    {
        public FrmBCH()
        {
            InitializeComponent();
        
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from BCH";
        string sqlCV = "select * from CHUCVU";
        string sqlDV = "select * from THONGTINCHUNG";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcBCH.DataSource = dt;
            }
        }

        private void loadCV()
        {
            DataTable dt = con.readData(sqlCV);
            if (dt != null)
            {
                lkuCV.Properties.DataSource = dt;
                lkuCV.Properties.DisplayMember = "TENCHUCVU";
                lkuCV.Properties.ValueMember = "MACHUCVU";
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
        private void FrmBCH_Load(object sender, EventArgs e)
        {
            
            loadData();
            loadCV();
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
            if ((lkuCV.EditValue == null) || (lkuCV.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập chức vụ\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lkuCV.Focus();
                return;
            }
            if ((txtNGK.EditValue == null) || (txtNGK.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập ngày ký\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNGK.Focus();
                return;
            }

            if ((txtNK.EditValue == null) || (txtNK.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập người ký\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNK.Focus();
                return;
            }
            bool check = false;
            string sql = "select MABCH from BCH";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtMBCH.EditValue.ToString().Trim().Equals(dr["MABCH"].ToString()))
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (check)
            {
                XtraMessageBox.Show("Mã ban chấp hành đã tồn tại\r\nVui lòng chọn mã ban chấp hành khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMBCH.EditValue = null;
                txtMBCH.Focus();
                return;
            }
           
            string sqlC = "insert into BCH(MABCH,MACHUCVU,MADV,TUNGAY,NGUOIKY)" +

            "values (" + "N'" + con.creatId("BCH", sqlR) + "'" + "," +
            "N'" + lkuCV.EditValue.ToString() + "'" + "," +
            "N'" + lkuDV.EditValue.ToString() + "'" + "," +
            "N'" + txtNGK.EditValue.ToString() + "'" + "," +
            "N'" + txtNK.EditValue.ToString() + "')";

            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm ban chấp hành thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm ban chấp hành thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMBCH.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn ban chấp hành để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtNGK.EditValue == null) || (txtNGK.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Ngày ký không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNGK.Focus();
                return;
            }
            if ((txtNK.EditValue == null) || (txtNK.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên người ký không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNK.Focus();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa ban chấp hành đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update BCH set TUNGAY = N'" + txtNGK.EditValue.ToString() + "',NGUOIKY = N'" + txtNK.EditValue.ToString() + "', MACHUCVU = '" + lkuCV.EditValue.ToString() + "', MADV = '" + lkuDV.EditValue.ToString() + "' where MABCH = '" + txtMBCH.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa ban chấp hành thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa ban chấp hành thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMBCH.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn ban chấp hành để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá ban chấp hành đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from BCH where MABCH = '" + txtMBCH.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá ban chấp hành thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá ban chấp hành thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMBCH.EditValue = null;
            txtNGK.EditValue = null;
            txtNK.EditValue = null;
            txtNGK.Focus();
            txtNK.Focus();
            lkuCV.EditValue = "";
            lkuDV.EditValue = "";
        }

        private void gcBCH_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvBCH.FocusedRowHandle;
            string colID = "MABCH";
            string colCV = "MACHUCVU";
            string colDV = "MADV";
            string colTN = "TUNGAY";
            string colNK = "NGUOIKY";
            if ((gvBCH.GetRowCellValue(row_index, colID) != null) && (gvBCH.GetRowCellValue(row_index, colCV) != null) && (gvBCH.GetRowCellValue(row_index, colDV) != null) && (gvBCH.GetRowCellValue(row_index, colNK) != null) && (gvBCH.GetRowCellValue(row_index, colNK) != null))
            {
                txtMBCH.EditValue = gvBCH.GetRowCellValue(row_index, colID).ToString();
                txtNGK.EditValue = gvBCH.GetRowCellValue(row_index, colTN).ToString();
                txtNK.EditValue = gvBCH.GetRowCellValue(row_index, colNK).ToString();
                lkuCV.EditValue = gvBCH.GetRowCellValue(row_index, colCV).ToString();
                lkuDV.EditValue = gvBCH.GetRowCellValue(row_index, colDV).ToString();
            }
        }
    }
}