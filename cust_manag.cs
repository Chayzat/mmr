using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mmr
{
    public partial class cust_manag : Form
    {
        public cust_manag()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 back = new Form1();
            back.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        //добавление нового пользователя
        static public bool AddUser(string login, string password, string role)
        {
            //формирование запроса
            msCommand.CommandText = "INSERT INTO users VALUES('" + login +
             "','" + password + "','" + role + "');";
            //выполение запроса
            if (msCommand.ExecuteNonQuery() > 0)
                return true;
            else
                return false;
        }
        //добавление нового заказчика
        static public void AddCustomer(string user, string name, string telephone, string
        adress, string email = null)
        {
            //формирование запроса
            msCommand.CommandText = "INSERT INTO customers VALUES('" + user + "','" + name
             + "','" + telephone + "','" + email + "','" + adress + "');";
            //выполение запроса
            msCommand.ExecuteNonQuery();
        }
        private void cust_manag_Load(object sender, EventArgs e)
        {

        }
        static public void EditCustomer(string user, string name, string telephone, string adress, string email)
        {
msCommand.CommandText = "UPDATE customers SET name = '" + name +
"', telephone = '" + telephone + "', adress='" + adress +
//выполение запроса
msCommand.ExecuteNonQuery();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //получение значения из 0-й ячейки выделенной строки в таблице
            txtLogin.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //... 

        }
    }
}
