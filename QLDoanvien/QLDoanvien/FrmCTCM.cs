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
    public partial class FrmCTCM : DevExpress.XtraEditors.XtraForm
    {
        public FrmCTCM()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from CTCDCM";
        string sqlCM = "select * from TRDCHUYENMON";
        string sqlDV = "select * from THONGTINCHUNG";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcCTCM.DataSource = dt;
            }
        }

        private void loadCM()
        {
            DataTable dt = con.readData(sqlCM);
            if (dt != null)
            {
                lkuTDCM.Properties.DataSource = dt;
                lkuTDCM.Properties.DisplayMember = "TENTRDCHUYENMON";
                lkuTDCM.Properties.ValueMember = "MATRDCHUYENMON";
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
        private void FrmCTCM_Load(object sender, EventArgs e)
        {
            loadData();
            loadCM();
            loadDV();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((lkuTDCM.EditValue == null) || (lkuTDCM.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập chuyên môn\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lkuTDCM.Focus();
                return;
            }
            if ((lkuDV.EditValue == null) || (lkuDV.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập đoàn viên\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lkuDV.Focus();
                return;
            }

            if ((txtNGC.EditValue == null) || (txtNGC.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập ngày cấp\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNGC.Focus();
                return;
            }

            if ((txtDVC.EditValue == null) || (txtDVC.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập đơn vị cấp\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDVC.Focus();
                return;
            }
           /* bool check = false;
            string sql = "select MACTTDCM from CTCDCM";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtMCTCM.EditValue.ToString().Trim().Equals(dr["MACTTDCM"].ToString()))
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (check)
            {
                XtraMessageBox.Show("Mã chi tiết chuyên môn đã tồn tại\r\nVui lòng chọn mã ban chấp hành khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMCTCM.EditValue = null;
                txtMCTCM.Focus();
                return;
            }
           */
            string sqlC = "insert into CTCDCM(MACTTDCM,MATRDCHUYENMON,MADV,NGAYCAP,DONVICAP)" +

            "values (" + "N'" + con.creatId("CTCM", sqlR) + "'" + "," +
            "N'" + lkuTDCM.EditValue.ToString() + "'" + "," +
            "N'" + lkuDV.EditValue.ToString() + "'" + "," +
            "N'" + txtNGC.EditValue.ToString() + "'" + "," +
            "N'" + txtDVC.EditValue.ToString() + "')";

            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm chi tiết chuyên môn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm chi tiết chuyên môn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMCTCM.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn chi tiết chuyên môn để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtNGC.EditValue == null) || (txtNGC.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Ngày cấp không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNGC.Focus();
                return;
            }
            if ((txtDVC.EditValue == null) || (txtDVC.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Đơn vị cấp không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDVC.Focus();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa ban chấp hành đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update BCH set DONVICAP = N'" + txtDVC.EditValue.ToString() + "',NGAYCAP = N'" + txtNGC.EditValue.ToString() + "', MATRDCHUYENMON = '" + lkuTDCM.EditValue.ToString() + "', MADV = '" + lkuDV.EditValue.ToString() + "' where MACTTDCM = '" + txtMCTCM.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa chi tiết chuyên môn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa chi tiết chuyên môn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMCTCM.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn chi tiết chuyên môn để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá chi tiết chuyên môn đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from CTCDCM where MACTTDCM = '" + txtMCTCM.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá chi tiết chuyên môn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá chi tiết chuyên môn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMCTCM.EditValue = null;
            txtNGC.EditValue = null;
            txtDVC.EditValue = null;
            txtNGC.Focus();
            txtDVC.Focus();
            lkuTDCM.EditValue = "";
            lkuDV.EditValue = "";
        }

        private void gcCTCM_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvCTCM.FocusedRowHandle;
            string colID = "MACTTDCM";
            string col1 = "MATRDCHUYENMON";
            string col2 = "MADV";
            string col3 = "NGAYCAP";
            string col4 = "DONVICAP";
            if ((gvCTCM.GetRowCellValue(row_index, colID) != null) && (gvCTCM.GetRowCellValue(row_index, col1) != null) && (gvCTCM.GetRowCellValue(row_index, col2) != null) && (gvCTCM.GetRowCellValue(row_index, col3) != null) && (gvCTCM.GetRowCellValue(row_index, col4) != null))
            {
                txtMCTCM.EditValue = gvCTCM.GetRowCellValue(row_index, colID).ToString();
                txtNGC.EditValue = gvCTCM.GetRowCellValue(row_index, col3).ToString();
                txtDVC.EditValue = gvCTCM.GetRowCellValue(row_index, col4).ToString();
                lkuTDCM.EditValue = gvCTCM.GetRowCellValue(row_index, col1).ToString();
                lkuDV.EditValue = gvCTCM.GetRowCellValue(row_index, col2).ToString();
            }
        }
    }
}