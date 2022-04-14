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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Data Source=WIN-0AVBIPRU9F2;Initial Catalog=Projects;Integrated Security=True
        int correct=0;
        int time = 90;
        int select;
        Random rnd = new Random();

        private void GetEng()
        {
            select = rnd.Next(241);
            SqlConnection conn = new SqlConnection("Data Source=WIN-0AVBIPRU9F2;Initial Catalog=Projects;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from tbl_Dict where id=@p",conn);
            cmd.Parameters.AddWithValue("@p",select);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtEng.Text = dr["ENG"].ToString();
                labelAnswer.Text = dr["AZE"].ToString();
            }
            conn.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtEng.Enabled = false;
            GetEng();
            lbl_Time.Text = time.ToString() + " seconds";
        }

        private void txtAZE_TextChanged(object sender, EventArgs e)
        {
            txtAZE.ForeColor = Color.FromArgb(0, 173, 181);
            timer1.Interval = 1000;
            timer1.Start();
            lbl_Time.Text = time.ToString() + " seconds";

        }
        private void HighScore()
        {
            //Update
            
            SqlConnection conn = new SqlConnection("Data Source=WIN-0AVBIPRU9F2;Initial Catalog=Projects;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE tbl_ENGUS set SCORE=@P1 " +
                "WHERE USERNAME=@US AND PASSWORD=@PAS AND "+correct+">score",conn);
            cmd.Parameters.AddWithValue("@P1",correct);
            cmd.Parameters.AddWithValue("@US", UserControl.getUsername);
            cmd.Parameters.AddWithValue("@PAS", UserControl.getPassword);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            lbl_Time.Text = time.ToString() + " seconds";
            if (time == 0)
            {
                txtAZE.Clear();
                timer1.Stop();
                HighScore();
                DialogResult secim= MessageBox.Show($"Your result is {correct} answers in 90 seconds \n" +
                    $"    Do you want to play again?","App",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (secim==DialogResult.Yes)
                {
                    time = 90;
                    correct = 0;
                    lblCorrect.Text = correct.ToString();
                    GetEng();
                    lbl_Time.Text = time.ToString() + " seconds";
                }
                else if (secim==DialogResult.No)
                {
                    Application.Exit();
                }
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtAZE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (txtAZE.Text == labelAnswer.Text)
                {
                    txtAZE.Clear();
                    correct++;
                    lblCorrect.Text = correct.ToString();
                    GetEng();
                }
                else
                {
                    txtAZE.ForeColor = Color.DarkRed;
                }
            }
            else if (e.KeyCode==Keys.Right)
            {
                GetEng();
                txtAZE.Clear();
                txtAZE.ForeColor = Color.FromArgb(0, 173, 181);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            SqlConnection conn = new SqlConnection("Data Source=WIN-0AVBIPRU9F2;Initial Catalog=Projects;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_ENGUS ORDER BY SCORE DESC" ,conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                form4.lblHighScore.Text = dr["SCORE"].ToString() + " correct";
                form4.lblName.Text = dr["USERNAME"].ToString();
                break;
            }
            conn.Close();
            
            form4.Show();

        }
    }
}
