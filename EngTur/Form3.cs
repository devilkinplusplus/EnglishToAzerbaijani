using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;    

namespace EngTur
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private bool UserExits()
        {
            bool check = true;
            SqlConnection conn = new SqlConnection("Data Source=WIN-0AVBIPRU9F2;Initial Catalog=Projects;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_ENGUS" +
                " where USERNAME=@P1 AND PASSWORD=@P2",conn);
            cmd.Parameters.AddWithValue("@P1", txtUsRe.Text);
            cmd.Parameters.AddWithValue("@P2", txtPasRe.Text);
            string scl = cmd.ExecuteScalar().ToString();
            if (int.Parse(scl)<1)
            {
                check = false;
            }
            return check;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserExits()==false)
            {
                SqlConnection conn = new SqlConnection("Data Source=WIN-0AVBIPRU9F2;Initial Catalog=Projects;Integrated Security=True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tbl_ENGUS (USERNAME,PASSWORD) " +
                    "values(@USERNAME,@PASSWORD)", conn);
                cmd.Parameters.AddWithValue("@USERNAME", txtUsRe.Text);
                cmd.Parameters.AddWithValue("@PASSWORD", txtPasRe.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Qeydiyyatdan uğurla keçdiniz", "App", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Belə bir istifadəçi artıq mövcuddur", "App", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
