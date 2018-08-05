using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WysSMS
{
    public partial class Form1 : Form
    {
        System.IO.Ports.SerialPort port;
        string numer = "503......";
        string wiadomosc = "Testowa wiadomosc ...";
        string sPortName = "COM5";

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = numer;
            textBox2.Text = wiadomosc;
            textBox3.Text = sPortName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numer = textBox1.Text;
            wiadomosc = textBox2.Text;
            Wyslij(numer, wiadomosc);
            MessageBox.Show("SMS wysłany ...");
        }

        private void Pauza() { System.Threading.Thread.Sleep(1000); }

        private void Wyslij(string numer, string wiadomosc)
        {
            //inicjalizacja zmiennej port z domyślnymi wartościami
            port = new SerialPort();
            //ustawienie timeoutów aby program się nie wieszał
            port.ReadTimeout = 500;
            port.WriteTimeout = 500;

            sPortName = textBox3.Text;
            string sBaud = "9600";
            string sData = "8";
            string sParity = "None";
            string sStop = "One";

            port.PortName = sPortName;
            port.BaudRate = Int32.Parse(sBaud);
            port.DataBits = Int32.Parse(sData);
            port.Parity = (Parity)Enum.Parse(typeof(Parity), sParity);
            port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), sStop);

            port.Open();
            Pauza();
            port.Write("at" + (char)13); // TODO: przy pierwszej komendzie ATodem zwraca "ERROR" zamiast "OK" ?
            Pauza();
            port.Write("at+cmgf=1" + (char)13);
            Pauza();
            port.Write("at+cmgs=" + (char)34 + numer + (char)34 + (char)13);
            Pauza();
            port.WriteLine(wiadomosc + (char)26 + (char)13);
            Pauza();
            port.Close();
        }
    }
}
