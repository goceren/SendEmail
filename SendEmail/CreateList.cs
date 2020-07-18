using SendEmail.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendEmail
{
    public partial class CreateList : Form
    {
        public CreateList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            string text = textBox1.Text.ToString();
            if (text != null && text != "")
            {
                PersonList newList = new PersonList();
                newList.ListName = textBox1.Text;
                if (Directory.Exists(@"C:\SendEmail\"))
                {
                    if (File.Exists(@"C:\SendEmail\document.json"))
                    {
                        string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                        List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);
                        if (document.Where(i => i.ListName == textBox1.Text).Any())
                        {
                            MessageBox.Show("Böyle Bir Liste Zaten Mevcut");
                        }
                        else
                        {
                            document.Add(newList);
                            string eklenen = Newtonsoft.Json.JsonConvert.SerializeObject(document);
                            File.WriteAllText(@"C:\SendEmail\document.json", eklenen);
                            MessageBox.Show("Eklendi");
                            this.Close();
                        }
                    }
                    else
                    {
                        List<PersonList> liste = new List<PersonList>();
                        liste.Add(newList);
                        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(liste);
                        File.WriteAllText(@"C:\SendEmail\document.json", jsonString);
                        MessageBox.Show("Eklendi");
                        this.Close();
                    }
                }
                else
                {
                    Directory.CreateDirectory(@"C:\SendEmail\");
                    List<PersonList> liste = new List<PersonList>();
                    liste.Add(newList);
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(liste);
                    File.WriteAllText(@"C:\SendEmail\document.json", jsonString);
                    MessageBox.Show("Eklendi");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Liste Adı Giriniz");
            }        
        }       
    }
}
