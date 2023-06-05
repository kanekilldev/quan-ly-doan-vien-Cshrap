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
    public partial class FrmTrDoVH : DevExpress.XtraEditors.XtraForm
    {
        public FrmTrDoVH()
        {
            InitializeComponent();
        }
        Con_CRUD con = new Con_CRUD();
        string sqlR = "select * from TRDVANHOA";

        private void loadData()
        {
            DataTable dt = con.readData(sqlR);
            if (dt != null)
            {
                gcTDVH.DataSource = dt;
            }
        }
        private void FrmTrDoVH_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ((txtTVH.EditValue == null) || (txtTVH.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Bạn chưa nhập tên trình độ văn hoá\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTVH.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENTRDVANHOA from TRDVANHOA where TENTRDVANHOA = N'" + txtTVH.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTVH.EditValue.ToString().Trim().Equals(dr["TENTRDVANHOA"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên trình độ văn hoá\"" + txtTVH.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            string sqlC = "insert into TRDVANHOA values ('" + con.creatId("TDVH", sqlR) + "', N'" + txtTVH.EditValue.ToString() + "')";
            if (con.exeData(sqlC))
            {
                loadData();
                XtraMessageBox.Show("Thêm trình độ văn hoá thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
            }
            else
            {
                XtraMessageBox.Show("Thêm trình độ văn hoá thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMVH.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn trình độ văn hoá để sửa\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((txtTVH.EditValue == null) || (txtTVH.EditValue.ToString().Equals("")))
            {
                XtraMessageBox.Show("Tên trình độ văn hoá không được phép để trống\r\nVui lòng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTVH.Focus();
                return;
            }
            bool checkB = false;
            string sql = "select TENTRDVANHOA from TRDVANHOA where TENTRDVANHOA = N'" + txtTVH.EditValue.ToString().Trim() + "'";
            DataTable dt = new DataTable();
            dt = con.readData(sql);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (txtTVH.EditValue.ToString().Trim().Equals(dr["TENTRDVANHOA"].ToString()))
                    {
                        checkB = true;
                        break;
                    }
                }
            }
            if (checkB)
            {
                XtraMessageBox.Show("Tên trình độ văn hoá \"" + txtTVH.EditValue.ToString() + "\" đã tồn tại\r\nVui lòng nhập tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLM.PerformClick();
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn sửa trình độ văn hoá đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlU = "update TRDVANHOA set TENTRDVANHOA = N'" + txtTVH.EditValue.ToString() + "' where MATRDOVH = '" + txtMVH.EditValue.ToString() + "'";
                if (con.exeData(sqlU))
                {
                    loadData();
                    XtraMessageBox.Show("Sửa trình độ văn hoá thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Sửa trình độ văn hoá thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMVH.EditValue == null)
            {
                XtraMessageBox.Show("Bạn chưa chọn trình độ văn hoá để xoá\r\nVui lòng chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xoá trình độ văn hoá đang chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sqlD = "delete from TRDVANHOA where MATRDOVH = '" + txtMVH.EditValue.ToString() + "'";
                if (con.exeData(sqlD))
                {
                    loadData();
                    XtraMessageBox.Show("Xoá trình độ văn hoá thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLM.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("Xoá trình độ văn hoá thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMVH.EditValue = null;
            txtTVH.EditValue = null;
            txtMVH.Focus();
        }

        private void gcTDVH_MouseCaptureChanged(object sender, EventArgs e)
        {
            int row_index = gvTDVH.FocusedRowHandle;
            string colID = "MATRDOVH";
            string colName = "TENTRDVANHOA";
            if ((gvTDVH.GetRowCellValue(row_index, colID) != null) && (gvTDVH.GetRowCellValue(row_index, colName) != null))
            {
                txtMVH.EditValue = gvTDVH.GetRowCellValue(row_index, colID).ToString();
                txtTVH.EditValue = gvTDVH.GetRowCellValue(row_index, colName).ToString();
            }
        }
    }
}