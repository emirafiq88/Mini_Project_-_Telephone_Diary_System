using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Telephone
{
    public partial class Phone : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Phone;Integrated Security=True");

        public Phone()
        {
            InitializeComponent();
        }

        private void Phone_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void tbReset_Click(object sender, EventArgs e)
        {
            tbFirstName.Clear();
            tbLastName.Clear();
            tbMobile.Clear();
            tbEmail.Clear();
            cbCategory.SelectedIndex = -1;

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(@"INSERT INTO MOBILE
                (FIRSTNAME, LASTNAME, MOBILE, EMAIL, CATEGORY) VALUES
                ('" + tbFirstName.Text + "', '" + tbLastName.Text + "', '" + tbMobile.Text + "', '" + tbEmail.Text +"', '"+cbCategory.Text+"')", con);
            cmd.ExecuteNonQuery();
            
            con.Close();
            MessageBox.Show("Successfully saved!");
            Display();
        }

        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM MOBILE", con);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach(DataRow dr in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value= dr["FirstName"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr["LastName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr["Mobile"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = dr["Email"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = dr["Category"].ToString();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            tbFirstName.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            tbLastName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            tbMobile.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            tbEmail.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            cbCategory.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();    
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(@"UPDATE MOBILE
                SET FIRSTNAME = '" + tbFirstName.Text + "', LASTNAME = '" + tbLastName.Text + "', MOBILE= '" + tbMobile.Text +"', EMAIL='" + tbEmail.Text + "', CATEGORY='" + cbCategory.Text + "' " +
                "WHERE MOBILE = '" + tbMobile.Text + "'", con);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Successfully Updated!");
            Display();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand(@"DELETE MOBILE
                WHERE MOBILE = '" + tbMobile.Text + "'", con);
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Successfully Deleted!");
            Display();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM MOBILE WHERE (MOBILE LIKE '%" + textBox1.Text + "%' or FIRSTNAME LIKE '%" + textBox1.Text + "%' or LASTNAME LIKE '%" + textBox1.Text + "%')", con);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            dataGridView1.Rows.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = dr["FirstName"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = dr["LastName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = dr["Mobile"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = dr["Email"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = dr["Category"].ToString();
            }
        }
    }
}
