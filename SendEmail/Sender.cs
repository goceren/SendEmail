using SendEmail.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendEmail
{
    public partial class Sender : Form
    {
        public Sender()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListForm listform = new ListForm();
            listform.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text.Trim() != "" && textBox2.Text != null && textBox2.Text.Trim() != "" && textBox3.Text != null && textBox3.Text.Trim() != "" && textBox4.Text != null && textBox4.Text.Trim() != "" && textBox8.Text != null && textBox8.Text.Trim() != "")
            {
                try
                {
                    if (Directory.Exists(@"C:\SendEmail\"))
                    {
                        if (File.Exists(@"C:\SendEmail\document.json"))
                        {
                            string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                            List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);
                            if (document.Where(i => i.ListName == comboBox1.SelectedValue.ToString()).Any())
                            {
                                if (document.Where(i => i.ListName == comboBox1.SelectedValue.ToString()).FirstOrDefault().People.ToList() != null)
                                {
                                    SmtpClient clientDetails = new SmtpClient();
                                    clientDetails.Port = Convert.ToInt32(textBox4.Text.Trim());
                                    clientDetails.Host = textBox3.Text.Trim();
                                    clientDetails.EnableSsl = checkBox1.Checked;
                                    clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    clientDetails.UseDefaultCredentials = false;
                                    clientDetails.Credentials = new NetworkCredential(textBox1.Text.Trim(), textBox2.Text.Trim());
                                    MailMessage mailDetails = new MailMessage();
                                    mailDetails.From = new MailAddress(textBox1.Text.Trim());
                                    foreach (var item in document.Where(i => i.ListName == comboBox1.SelectedValue.ToString()).FirstOrDefault().People.ToList())
                                    {
                                        mailDetails.To.Add(item.Email.Trim());

                                    }
                                    mailDetails.Subject = textBox8.Text.Trim();
                                    mailDetails.IsBodyHtml = checkBox2.Checked;
                                    mailDetails.Body = richTextBox1.Text.Trim();

                                    clientDetails.Send(mailDetails);
                                    MessageBox.Show("Email Gönderildi");
                                }
                                else
                                {
                                    MessageBox.Show("Kişi Listesi Boş");

                                }
                            }
                            else
                            {
                                MessageBox.Show("Kişi Listesi Bulunamadı");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Dosya Bulunamadı");

                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Gerekli Bilgileri Doldurunuz");

            }

        }

        private void Sender_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            if (Directory.Exists(@"C:\SendEmail\"))
            {
                if (File.Exists(@"C:\SendEmail\document.json"))
                {
                    string okunanJson = File.ReadAllText(@"C:\SendEmail\document.json");
                    List<PersonList> document = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonList>>(okunanJson);
                    comboBox1.DataSource = document;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Sender_Load(null,EventArgs.Empty);
        }
    }
}
