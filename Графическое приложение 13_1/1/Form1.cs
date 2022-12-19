using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        abstract class Product
        {
            public abstract void Input(RichTextBox richTextBox1);
            public abstract bool Srok_god();
            public string Name;
        }
        class Item : Product
        {

            double Price;
            DateTime Data_Proiz;
            DateTime Expiration_Date;
            public Item(string Name, double Price, DateTime Data_Proiz, DateTime Expiration_Date)
            {
                this.Name = Name;
                this.Price = Price;
                this.Data_Proiz = Data_Proiz;
                this.Expiration_Date = Expiration_Date;
            }
            public override void Input(RichTextBox richTextBox1)
            {
                richTextBox1.Text+="Название: " + Name + '\n' +
                    "Цена: " + Price + '\n' +
                    "Дата производства: " + Data_Proiz.ToShortDateString() + '\n' +
                    "Срок годности: " + Expiration_Date.ToShortDateString() + '\n';
            }

            public override bool Srok_god()
            {
                if (Expiration_Date >= DateTime.Now)
                {
                    return true;
                }
                else return false;
            }
        }
        class Party : Product
        {
            double Price;
            int Kol_Vo;
            DateTime Data_Proiz;
            DateTime Expiration_Date;
            public Party(string Name, double Price, int Kol_Vo, DateTime Data_Proiz, DateTime Expiration_Date)
            {
                this.Name = Name;
                this.Price = Price;
                this.Kol_Vo = Kol_Vo;
                this.Data_Proiz = Data_Proiz;
                this.Expiration_Date = Expiration_Date;
            }
            public override void Input(RichTextBox richTextBox1)
            {
                richTextBox1.Text += "Название: " + Name + '\n' +
                    "Цена: " + Price + '\n' +
                    "Количество: " + Kol_Vo + '\n' +
                    "Дата производства: " + Data_Proiz.ToShortDateString() + '\n' +
                    "Срок годности: " + Expiration_Date.ToShortDateString() + '\n';
            }

            public override bool Srok_god()
            {
                if (Expiration_Date >= DateTime.Now)
                {
                    return true;
                }
                else return false;
            }
        }
        class Set : Product
        {

            double Price;
            string List;
            DateTime Expiration_Date;
            public Set(string Name, double Price, string List, DateTime Expiration_Date)
            {
                this.Name = Name;
                this.Price = Price;
                this.List = List;
                this.Expiration_Date = Expiration_Date;
            }
            public override void Input(RichTextBox richTextBox1)
            {
                richTextBox1.Text += "Название: " + Name + '\n' +
                    "Цена: " + Price + '\n' +
                    "Перечень продуктов: " + List + '\n';
            }

            public override bool Srok_god()
            {
                if (Expiration_Date >= DateTime.Now)
                {
                    return true;
                }
                else return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = File.ReadAllLines("text.txt").Length;
            Product[] products = new Product[count];
            string[] str;
            StreamReader sr = new StreamReader("text.txt");
            for (int i = 0; i < count; i++)
            {
                str = sr.ReadLine().Split(' ');
                if (str[0] == "Item")
                    products[i] = new Item(str[1], double.Parse(str[2]), DateTime.Parse(str[3]), DateTime.Parse(str[4]));
                else if (str[0] == "Party")
                    products[i] = new Party(str[1], double.Parse(str[2]), int.Parse(str[3]), DateTime.Parse(str[4]), DateTime.Parse(str[5]));
                else if (str[0] == "Set")
                {
                    string[] line = str[3].Split(':');
                    string lines = "";
                    for (int j = 0; j < line.Length; j++)
                    {
                        lines += line[j] + " ";
                    }
                    products[i] = new Set(str[1], double.Parse(str[2]), lines, DateTime.Parse(str[4]));
                }
            }
            sr.Close();
            for (int i = 0; i < count; i++)
            {
                products[i].Input(richTextBox1);
                richTextBox1.Text += "---------------------------------------------"+'\n';
            }
            richTextBox2.Text += "Просроченные товары:"+'\n';

            foreach (var List in products)
            {
                if (!List.Srok_god())
                {
                   richTextBox2.Text+=(List.Name);
                }
            }
        }
    }
}
