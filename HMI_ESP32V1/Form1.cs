using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace HMI_ESP32V1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_Open.Enabled = true;
            btn_Close.Enabled = false;
            pictureBox1.BackColor = Color.Red;
            comboBox2.Text = "9600";
            progressBar1.Value = 0;

            string[] portList = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(portList);
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort1.Open();

                btn_Open.Enabled = false;
                btn_Close.Enabled = true;
                progressBar1.Value = 100;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;

                pictureBox1.BackColor = Color.Red;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                
            }
           

        }

        private void btn_LedOff_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    pictureBox1.BackColor = Color.Red;
                    serialPort1.WriteLine("#Off");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }

            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.WriteLine("#Off");
                    btn_Open.Enabled = true;
                    btn_Close.Enabled = false;
                    progressBar1.Value = 0;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;

                    pictureBox1.BackColor = Color.Red;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);

                }
            }
        }

        private void btn_LedOn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    pictureBox1.BackColor = Color.Lime;
                    serialPort1.WriteLine("#On");
                }catch(Exception error)
                {
                    MessageBox.Show(error.Message);
                }

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    pictureBox1.BackColor=Color.Red;
                    serialPort1.WriteLine("#Off");
                    serialPort1.Close();
                }catch(Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }
    }
}
