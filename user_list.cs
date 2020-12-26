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
    public partial class user_list : Form
    {
        public user_list()
        {
            InitializeComponent();
        }

        private void user_list_Load(object sender, EventArgs e)
        {
            DBConnection.GetUserList(); //получение списка пользователей
            dataGV.DataSource = DBConnection.dtUsers; //привязка набора данных к таблице
        }

        private void dataGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedRole = null; //выбранное значение поля
                                        //сопоставление номера выбранного значения в списке с типами ролей
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    selectedRole = "admin";
                    break;
                case 2:
                    selectedRole = "customer";
                    break;
            }
            DBConnection.GetUserList(selectedRole); //получение списка пользователей

        }
    }
}
