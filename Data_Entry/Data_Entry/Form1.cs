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

namespace Data_Entry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // connection string :

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-NC137G9\\SQLEXPRESS;Initial Catalog=baza2;Integrated Security=True");

        //save data in database :-)

        private void btnsave_Click(object sender, EventArgs e)
        {
            
            if (txtid.Text == "" || txtime.Text == "" || txtzanr.Text == "" || txtbroj_strana.Text == "" || txtgodina_izdanja.Text == "" || txtautor.Text == "")
                {
                    MessageBox.Show("Prvo popunite polje.");
                }
                else
                {
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("insert into baza2.dbo.Library1(id,ime,autor,zanr,godina_izdanja,broj_strana)values('" + txtid.Text + "','" + txtime.Text + "','" + txtautor.Text + "','" + txtzanr.Text + "','" + txtgodina_izdanja.Text + "','" + txtbroj_strana.Text + "')", conn);

                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Podaci su prosli uspesno. . . .");
                    panel1.Enabled = false;

                }
            
        }
        // view the data in datagridview :-)

        private void btnview_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            //conn.Open();
            //SqlDataAdapter sda = new SqlDataAdapter("select * from baza2.dbo.Library1",conn);
            //DataTable data = new DataTable();
            //sda.Fill(data);
            //dataGridView1.DataSource = data;
            load_data();
            //conn.Close();

            
        }

        private void load_data()
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from baza2.dbo.Library1", conn);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            conn.Close();
        }
        // double click event for updating and deleting the data from database :-)

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            panel1.Enabled = true;
            txtid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtime.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtautor.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtzanr.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtgodina_izdanja.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtbroj_strana.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            


        }
        //update the data :-)

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (panel1.Enabled == true)
            {

                if (txtid.Text == "" || txtime.Text == "" || txtzanr.Text == "" || txtbroj_strana.Text == "" || txtgodina_izdanja.Text == "" || txtautor.Text == "")
                {
                    MessageBox.Show("Popunite polja...");
                }
                else
                {
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("UPDATE baza2.dbo.Library1 SET id ='" + txtid.Text + "',ime = '" + txtime.Text + "',autor = '" + txtautor.Text + "', zanr = '" + txtzanr.Text + "',godina_izdanja = '" + txtgodina_izdanja.Text + "',broj_strana = '" + txtbroj_strana.Text + "' where id ='" + txtid.Text + "'", conn);

                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Podaci su uspesno promenjeni. . . .");
                    load_data();
                    panel1.Enabled = false;
                }

            }
            else
            {
                MessageBox.Show("Selektujte sta zelite da promenite.");
            }
           
            
        }

        //delete btn c0ding :-)

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (panel1.Enabled == true)
            {

                if (txtid.Text == "" || txtime.Text == "" || txtzanr.Text == "" || txtbroj_strana.Text == "" || txtgodina_izdanja.Text == "" || txtautor.Text == "")
                {
                    MessageBox.Show("Selectujte sacuvnao");
                }
                else
                {
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("delete from baza2.dbo.Library1 where id ='" + txtid.Text + "'", conn);

                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Podaci su uspesno obrisani. . . .");
                    load_data();
                    panel1.Enabled = false; 
                }
               
            }
            else
            {
                MessageBox.Show("Prvo selectujte sacuvano");
            }
           
        }

        //to insert the new entry :-*

        private void btnnew_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = false;
            panel1.Enabled = true;
            
            txtid.Text = "";
            txtime.Text = "";
            txtautor.Text = "";
            txtzanr.Text = "";
            txtgodina_izdanja.Text = "";
            txtbroj_strana.Text = "";
            
        }

        //search the data from database by name :-*

        private void srchbtn_Click(object sender, EventArgs e)
        {
            if (txtnamesrch.Text == "")
            {
                MessageBox.Show("Polje je prazo.");
            }
            
            else
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from baza2.dbo.Library1 where ime LIKE '%" + txtnamesrch.Text + "%'", conn);
                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;
                conn.Close();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
