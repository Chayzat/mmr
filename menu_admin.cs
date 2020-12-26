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
    public partial class menu_admin : Form
    {
        public menu_admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Form1 back= new Form1();
            back.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cust_manag ad = new cust_manag();
            ad.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            msCommand.CommandText = @"SELECT Assortiment.Product, Assortiment.Name,
 Sum(Store.Count) AS Count
 FROM Assortiment
 INNER JOIN Store USING(Product)
 WHERE Count>0
 GROUP BY Product;";
        }
        static public int WriteOff()
        {
            //формирование запроса на выборку просроченных товаров
            msCommand.CommandText = @"SELECT Store.PositionId, Store.Product, Store.Count,
 Store.Date, Assortiment.ShelfLife
 FROM Store
 INNER JOIN Assortiment USING(Product)
 WHERE To_Days(CURDATE())-TO_Days(Store.Date) >=
 Assortiment.ShelfLife;";
            dtWriteOff.Clear();
            msDataAdapter.Fill(dtWriteOff); //наполнение набора данных
                                            //обход таблицы с просроченными товарами
            foreach (DataRow row in dtWriteOff.Rows)
            {
                //преобразование столбца с датой к используемому формату
                DateTime date = new DateTime();
                date = (DateTime)row[3];
                //формирвание запроса на вставку записи в таблицу с просрочкой
                msCommand.CommandText = @"INSERT INTO WriteOff VALUES('" + row[0] + "','"
                 + row[1] + "','" + row[2] + "','" +
                 date.ToString(("yyyy-MM-dd")) + "','" +
                 DateTime.Today.ToString("yyyy-MM-dd") + "')";
                msCommand.ExecuteNonQuery();
                //формирование запроса на удаление позиции со склада
                msCommand.CommandText = @"DELETE FROM Store
 WHERE PositionId='" + row[0] + "';";
                msCommand.ExecuteNonQuery();
            }
            return dtWriteOff.Rows.Count; //возвращаем количество просроченных товаров
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int count = DBConnection.WriteOff();
            if (count > 0)
                MessageBox.Show("Списано " + count.ToString() + " товаров.");
            else
                MessageBox.Show("Нет просроченных товаров.");
        }
    }
}
