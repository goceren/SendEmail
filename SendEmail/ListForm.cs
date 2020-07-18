using SendEmail.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using System.Windows.Forms;

namespace SendEmail
{
    public partial class ListForm : Form
    {
        public ListForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateList createList = new CreateList();
            createList.ShowDialog();
            this.ListForm_Load(null, EventArgs.Empty);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != null && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox4.Text != null)
            {
                if (Directory.Exists(@"C:\SendEmail\") && File.Exists(@"C:\SendEmail\document.json"))
                {
                    string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                    List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);

                    if (document.Where(i => i.ListName == textBox2.Text).Any())
                    {
                        var sorgu = document.Where(i => i.ListName == textBox2.Text).FirstOrDefault().People;
                        if (sorgu != null && sorgu.Where(i => i.Email == textBox4.Text).Any())
                        {
                            MessageBox.Show("Bu email bu listede zaten var");
                        }
                        else
                        {
                            Person eklenen = new Person()
                            {
                                NameSurname = textBox3.Text,
                                Email = textBox4.Text
                            };
                            List<Person> liste = new List<Person>();
                            if (document.Where(i => i.ListName == textBox2.Text).FirstOrDefault().People != null)
                            {
                                liste.AddRange(document.Where(i => i.ListName == textBox2.Text).FirstOrDefault().People);
                            }
                            liste.Add(eklenen);

                            document.Where(i => i.ListName == textBox2.Text).FirstOrDefault().People = liste;
                            string doc = Newtonsoft.Json.JsonConvert.SerializeObject(document);
                            File.WriteAllText(@"C:\SendEmail\document.json", doc);
                            dataGridView2.DataSource = document.Where(i => i.ListName == textBox2.Text).FirstOrDefault().People;
                            MessageBox.Show("Eklendi");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Liste Bulunamadı. Liste Seçiniz Veya Liste Adı kısmına listenin adını yazınız.");
                    }

                }
            }
            else
            {
                MessageBox.Show("Liste Bulunamadı. Liste Seçiniz Veya Liste Adı kısmına listenin adını yazınız.");
            }

        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(@"C:\SendEmail\"))
            {
                if (File.Exists(@"C:\SendEmail\document.json"))
                {
                    string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                    List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);
                    dataGridView1.DataSource = document;
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                string selectedList = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                textBox2.Text = selectedList;

                if (Directory.Exists(@"C:\SendEmail\"))
                {
                    if (File.Exists(@"C:\SendEmail\document.json"))
                    {
                        string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                        List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);
                        dataGridView2.DataSource = document.Where(i => i.ListName == selectedList).FirstOrDefault().People;
                    }
                }
            }        
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                string name = dataGridView2.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                string email = dataGridView2.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                textBox3.Text = name;
                textBox4.Text = email;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(@"C:\SendEmail\") && File.Exists(@"C:\SendEmail\document.json"))
            {
                if (Directory.Exists(@"C:\SendEmail\") && File.Exists(@"C:\SendEmail\document.json"))
                {
                    string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                    List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);

                    if (document.Where(i => i.ListName == textBox2.Text).Any())
                    {
                        var sorgu = document.Where(i => i.ListName == textBox2.Text).FirstOrDefault().People;
                        if (sorgu != null && sorgu.Where(i => i.Email == textBox4.Text).Any())
                        {
                            document.Where(i => i.ListName == textBox2.Text).FirstOrDefault().People.RemoveAll(i => i.Email == textBox4.Text);
                            string doc = Newtonsoft.Json.JsonConvert.SerializeObject(document);
                            File.WriteAllText(@"C:\SendEmail\document.json", doc);
                            dataGridView2.DataSource = document.Where(i => i.ListName == textBox2.Text).FirstOrDefault().People;
                            MessageBox.Show("Silindi");
                            textBox3.Clear();
                            textBox4.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı Bulunamadı");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Liste Bulunamadı. Liste Seçiniz Veya Liste Adı kısmına listenin adını yazınız.");
                    }

                }
            }
            else
            {
                MessageBox.Show("Liste Bulunamadı. Liste Seçiniz Veya Liste Adı kısmına listenin adını yazınız.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(@"C:\SendEmail\"))
            {
                if (File.Exists(@"C:\SendEmail\document.json"))
                {
                    string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                    List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);
                    dataGridView1.DataSource = document.Where(i => i.ListName.ToLower().StartsWith(textBox1.Text.ToLower())).ToList();
                    dataGridView2.DataSource = null;
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (Directory.Exists(@"C:\SendEmail\"))
            {
                if (File.Exists(@"C:\SendEmail\document.json"))
                {
                    string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                    List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);
                    dataGridView1.DataSource = document;

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null && textBox2.Text.Trim() != "")
            {

                if (Directory.Exists(@"C:\SendEmail\"))
                {
                    if (File.Exists(@"C:\SendEmail\document.json"))
                    {
                        string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                        List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);
                        if (document.Where(i => i.ListName == textBox2.Text).Any())
                        {
                            DialogResult res = MessageBox.Show("Silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            if (res == DialogResult.OK)
                            {
                                document.RemoveAll(i => i.ListName == textBox2.Text);
                                string doc = Newtonsoft.Json.JsonConvert.SerializeObject(document);
                                File.WriteAllText(@"C:\SendEmail\document.json", doc);
                                dataGridView1.DataSource = document;
                                dataGridView2.DataSource = null;
                                MessageBox.Show("Silindi");
                                textBox2.Clear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Liste Bulunamadı. Liste Seçiniz Veya Liste Adı kısmına listenin adını yazınız.");
                        }

                    }
                }

            }
            else
            {
                MessageBox.Show("Liste Bulunamadı. Liste Seçiniz Veya Liste Adı kısmına listenin adını yazınız.");
            }

        }

    }
}

