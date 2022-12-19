﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCCUBH
{
    public partial class frm_Phieucungung : DevExpress.XtraEditors.XtraForm
    {
        QuanLyCuaHangEntities data;
        bool action = false;
        public frm_Phieucungung()
        {
            InitializeComponent();
        }
        void getData()
        {
            data = new QuanLyCuaHangEntities();
            List<PHIEUCUNGUNG> lst = data.PHIEUCUNGUNGs.ToList();
            int i = 0;
            var v = from t in lst
                    select new
                    {
                        No = ++i,
                        SOPHIEU = t.SoPhieu,
                        NGAYLAPPHIEU = t.NgayLapPhieu,
                        TRIGIAPHIEU = t.TriGiaPhieu,
                        NHACUNGCAP = t.NHACUNGCAP.TenNhaCungCap

                    };
            frm_dataphieucungung.DataSource = v.ToList();
        }
        void getNhaCungCap()
        {
            data = new QuanLyCuaHangEntities();
            combomanhacungcap.DataSource = data.NHACUNGCAPs.ToList();
        }
        void init()
        {
            txtngaythanhlap.Format = DateTimePickerFormat.Custom;
            txtngaythanhlap.CustomFormat = "dd/MM/yyyy";
            combomanhacungcap.DisplayMember = "TenNhaCungCap";
            combomanhacungcap.ValueMember = "MaNhaCungCap";
        }
        private void frm_Phieucungung_Load(object sender, EventArgs e)
        {
            getData();
            getNhaCungCap();
            init();
        }

        private void save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (action == false)
            {
                data = new QuanLyCuaHangEntities();
                var k = new PHIEUCUNGUNG
                {
                    SoPhieu = txtsophieu.Text.Trim(),
                    NgayLapPhieu = DateTime.Parse(txtngaythanhlap.Value.ToString("dd/MM/yyyy")),
                    TriGiaPhieu = int.Parse(txttrigiaphieu.Text.Trim()),
                    MaNhaCungCap = combomanhacungcap.SelectedValue.ToString(),
                };
                data.PHIEUCUNGUNGs.Add(k);
                data.SaveChanges();
                getData();
            }
            else
            {
                data = new QuanLyCuaHangEntities();
                var s = (from t in data.PHIEUCUNGUNGs
                         where t.SoPhieu == txtsophieu.Text
                         select t).SingleOrDefault();
                var phieucungung = data.PHIEUCUNGUNGs.FirstOrDefault(x => x.SoPhieu.Contains(txtsophieu.Text));
                phieucungung.NgayLapPhieu = DateTime.Parse(txtngaythanhlap.Value.ToString("dd/MM/yyyy"));
                phieucungung.TriGiaPhieu = int.Parse(txttrigiaphieu.Text.Trim());
                phieucungung.MaNhaCungCap = combomanhacungcap.SelectedValue.ToString();

                data.SaveChanges();
                getData();
            }
        }

        private void Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            data = new QuanLyCuaHangEntities();
            if (MessageBox.Show("Are you sure ?", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var s = (from t in data.PHIEUCUNGUNGs
                         where t.SoPhieu == txtsophieu.Text
                         select t
                    ).SingleOrDefault();
                data.PHIEUCUNGUNGs.Remove(s);
                data.SaveChanges();
                getData();
            }
        }

        private void Cancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            action = false;
            txtsophieu.ReadOnly = false;
            Delete.Enabled = false;
            txtsophieu.Clear();
            txttrigiaphieu.Clear();
        }

        private void frm_dataphieucungung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = frm_dataphieucungung.Rows[e.RowIndex];
                txtsophieu.Text = row.Cells[1].Value.ToString();
                txtngaythanhlap.Text = row.Cells[2].Value.ToString();
                txttrigiaphieu.Text = row.Cells[3].Value.ToString();
                combomanhacungcap.Text = row.Cells[4].Value.ToString();
                Delete.Enabled = true;
                action = true;

            }
        }

        private void Exist_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}