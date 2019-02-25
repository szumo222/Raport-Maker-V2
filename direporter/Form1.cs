using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace direporter
{
    public partial class Form1 : Form
    {
        string miesiac;
        string rok;
        bool czynadpisac;
        string f_xslt;
        string[] day;
        string destination_folder_zaiks;
        string destination_folder_stoart;
        string destination_folder_ekstra_zaiks;
        string destination_folder_ekstra_stoart;
        string get_folder_szczecin_zaiks;
        string get_folder_szczecin_stoart;
        string get_folder_szn_ekstra_zaiks;
        string get_folder_szn_ekstra_stoart;
        string get_folder_zaiks;
        string get_folder_stoart;
        string fname;
        string fname_part;
        int szn_or_szn_ekstra = 0;
        bool error = false;
        bool text_box_text_change = false;
        List<string> array2 = new List<string>();

        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (text_box_text_change == false)
                {
                    MessageBox.Show("Ustaw datę!", "Raport Maker V2");
                    return;
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(@"config_raport_maker.xml");
                XmlNodeList xml_zaiks = doc.GetElementsByTagName("zaiks");
                for (int i = 0; i < xml_zaiks.Count; i++)
                {
                    XmlNode xml_dest_folder_zaiks = xml_zaiks[i].SelectSingleNode("destination_folder");
                    XmlNode xml_dest_folder_ekstra_zaiks = xml_zaiks[i].SelectSingleNode("destination_folder_ekstra");
                    XmlNode xml_get_folder_zaiks = xml_zaiks[i].SelectSingleNode("get_folder_szczecin");
                    XmlNode xml_get_folder_x_zaiks = xml_zaiks[i].SelectSingleNode("get_folder_x");
                    destination_folder_zaiks = xml_dest_folder_zaiks.InnerText;
                    destination_folder_ekstra_zaiks = xml_dest_folder_ekstra_zaiks.InnerText;
                    get_folder_szczecin_zaiks = xml_get_folder_zaiks.InnerText;
                    get_folder_szn_ekstra_zaiks = xml_get_folder_x_zaiks.InnerText;
                }
                XmlNodeList xml_stoart = doc.GetElementsByTagName("stoart");
                for (int i = 0; i < xml_stoart.Count; i++)
                {
                    XmlNode xml_dest_folder_stoart = xml_stoart[i].SelectSingleNode("destination_folder");
                    XmlNode xml_dest_folder_ekstra_stoart = xml_stoart[i].SelectSingleNode("destination_folder_ekstra");
                    XmlNode xml_get_folder_stoart = xml_stoart[i].SelectSingleNode("get_folder_szczecin");
                    XmlNode xml_get_folder_x_stoart = xml_stoart[i].SelectSingleNode("get_folder_x");
                    destination_folder_stoart = xml_dest_folder_stoart.InnerText;
                    destination_folder_ekstra_stoart = xml_dest_folder_ekstra_stoart.InnerText;
                    get_folder_szczecin_stoart = xml_get_folder_stoart.InnerText;
                    get_folder_szn_ekstra_stoart = xml_get_folder_x_stoart.InnerText;
                }

                miesiac = dateTimePicker1.Value.Month.ToString();
                rok = dateTimePicker1.Value.Year.ToString();

                Radiocheck_ktory_folder();
                Radiocheck();

                if (error == true) return;

                List<string> array3 = new List<string>();
                List<string> array4 = new List<string>();
                czynadpisac = false;
                int z = 0;
                progressBar1.Value = 0;
                progressBar1.Step = 1;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = array2.Count;

                if (File.Exists(fname))
                {
                    DialogResult dr = MessageBox.Show("Raport już istnieje\nCzy chcesz go nadpisać?", "Raport Maker V2", MessageBoxButtons.YesNo);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            FileInfo fff = new FileInfo(fname);
                            fff.Delete();
                            czynadpisac = true;
                            break;
                        case DialogResult.No:
                            czynadpisac = false;
                            return;
                    }
                }
                else if (!File.Exists(fname)) czynadpisac = true;

                if (czynadpisac == true)
                {

                    foreach (string file in array2)
                    {
                        XslCompiledTransform xslt2 = new XslCompiledTransform();
                        xslt2.Load(f_xslt);
                        string path = Path.GetFileNameWithoutExtension(file);
                        string f_out2 = @"raport_maker_help\" + path + "_" + z + ".txt";
                        xslt2.Transform(file, f_out2);
                        z++;
                        progressBar1.PerformStep();
                        progressBar1.Refresh();
                    }
                    string[] array = Directory.GetFiles(@"raport_maker_help\", "*.txt", SearchOption.AllDirectories);

                    foreach (string file in array)
                    {
                        string[] lines = File.ReadAllLines(file);
                        for (int i = 0; i < lines.Length; i++)
                        {
                            array3.Add(lines[i]);
                        }
                    }
                    array3.Sort();
                    array4.Add("Data|Godz.aud.|Tytul audycji|Tytul utworu|Kompozytor|Autor tekstu|Tlumacz|Czas|Wykonawca|Producent|Wydawca|");
                    foreach (string s in array3)
                    {
                        StringBuilder ss = new StringBuilder(s);
                        ss.Remove(17,6);                        
                        array4.Add(ss.ToString());
                        //array4.Add(s);
                    }
                    File.WriteAllLines(fname, array4, Encoding.UTF8);

                    string dsa = @"raport_maker_help\";
                    DirectoryInfo di = new DirectoryInfo(dsa);
                    array2.Clear();
                    array3.Clear();
                    array4.Clear();
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                }
                else
                {
                    MessageBox.Show("Wybierz folder do zapisu!", "Raport Maker V2");
                    return;
                }
                progressBar1.Value = 0;
                FileInfo f1 = new FileInfo(fname);
                MessageBox.Show("Zakończono. Plik \n\n" + f1.Name + "\n\nzostał zapisany.", "Raport Maker V2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex);
                FileInfo ffff = new FileInfo(fname);
                ffff.Delete();
                array2.Clear();
            }
        }

        //Metoda analizująca radiobutton'y
        private void Radiocheck()
        {
            //Zaiks
            if (radioButton1.Checked == true)
            {
                if (szn_or_szn_ekstra ==1)
                {
                    fname = destination_folder_zaiks + @"raport_zaiks_" + fname_part;
                }
                else if (szn_or_szn_ekstra ==2)
                {
                    fname = destination_folder_ekstra_zaiks + @"raport_zaiks_" + fname_part;
                }

                f_xslt = @"raportdlazaikkopias.xslt";
                string[] folder_days_zaiks_dir = Directory.GetDirectories(get_folder_zaiks + rok + @"\" + miesiac + @"\");
                foreach (string dir in folder_days_zaiks_dir)
                {
                    day = Directory.GetFiles(dir, "*.xml", SearchOption.AllDirectories);
                    foreach (string d in day)
                    {
                        array2.Add(d);
                    }
                }
            }

            //Stoart
            else if (radioButton2.Checked == true)
            {
                if (szn_or_szn_ekstra == 1)
                {
                    fname = destination_folder_stoart + @"raport_stoart_" + fname_part;
                }
                else if (szn_or_szn_ekstra ==2)
                {
                    fname = destination_folder_ekstra_stoart + @"raport_stoart_" + fname_part;
                }

                f_xslt = @"raportdlastoartkapias.xslt";
                string[] folder_days_stoart_dir = Directory.GetDirectories(get_folder_stoart + rok + @"\" + miesiac + @"\");
                foreach (string dir in folder_days_stoart_dir)
                {
                    day = Directory.GetFiles(dir, "*.xml", SearchOption.AllDirectories);
                    foreach (string d in day)
                    {
                        array2.Add(d);
                    }
                }
            }

            //Brak
            else
            {
                MessageBox.Show("Wybierz rodzaj raportu!", "Raport Maker V2");
                error = true;
            }

        }

        private void Radiocheck_ktory_folder()
        {
            if (radioButton4.Checked ==true)
            {
                get_folder_zaiks = get_folder_szczecin_zaiks;
                get_folder_stoart = get_folder_szczecin_stoart;
                fname_part = @"szczecin_" + miesiac + "_" + rok + ".txt";
                szn_or_szn_ekstra = 1;

            }
            else if(radioButton3.Checked == true)
            {
                get_folder_zaiks = get_folder_szn_ekstra_zaiks;
                get_folder_stoart = get_folder_szn_ekstra_stoart;
                fname_part = @"szczecin_ekstra_" + miesiac + "_" + rok + ".txt";
                szn_or_szn_ekstra = 2;
            }
            else
            {
                MessageBox.Show("Wybierz który folder!", "Raport Maker V2");
                error = true;
            }
        }

        //Zmiana daty
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTimePicker1.Value.ToString("MMMM");
            textBox3.Text = dateTimePicker1.Value.ToString("yyyy");
            text_box_text_change = true;
        }

        //Informacji
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Autor:\tPatryk Szumielewicz\nNumer:\t797578683\nE-mail:\tszumielewiczpatryk@gmail.com\nWersja:\t" + System.Windows.Forms.Application.ProductVersion, "Raport Maker V2");
        }
    }
}