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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //Data Source=WIN-0AVBIPRU9F2;Initial Catalog=Projects;Integrated Security=True
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=WIN-0AVBIPRU9F2;Initial Catalog=Projects;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from tbl_ENGUS WHERE " +
                "USERNAME=@P1 AND PASSWORD=@P2",conn);
            cmd.Parameters.AddWithValue("@P1",textBox1.Text);
            cmd.Parameters.AddWithValue("@P2", textBox2.Text);
            UserControl.setUsername = textBox1.Text;
            UserControl.setPassword = textBox2.Text;
            
            string scl = cmd.ExecuteScalar().ToString();
            if (int.Parse(scl)>0)
            {
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Belə bir istifadəçi tapılmadı","App",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            conn.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 frm3 = new Form3();
            frm3.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
