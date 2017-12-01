using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePhilipsHueLibTestApp
{
    public partial class Form1 : Form
    {
        PhilipsHue HueBridge;
        Color? color = null;
        int? group = null;
        int? bulp = null;
        bool turned_on = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("0");
            comboBox1.Items.Add("1");
            comboBox1.Items.Add("2");
            comboBox1.Items.Add("3");
            comboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
            textBox1.Text = SimplePhilipsHueLibTestApp.Properties.Settings.Default.ip;
            textBox2.Text = SimplePhilipsHueLibTestApp.Properties.Settings.Default.username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HueBridge = new PhilipsHue(textBox1.Text, textBox2.Text);
            SimplePhilipsHueLibTestApp.Properties.Settings.Default.ip = textBox1.Text;
            SimplePhilipsHueLibTestApp.Properties.Settings.Default.username = textBox2.Text;
            SimplePhilipsHueLibTestApp.Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            turned_on = true;
            HueBridge.TurnOn(true,color,group,bulp);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bulp = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            group = null;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            group = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            bulp = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                group = Convert.ToInt32(comboBox1.SelectedItem.ToString());
                bulp = null;
            }
            else
            {
                bulp = Convert.ToInt32(comboBox1.SelectedItem.ToString());
                group = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog1.Color;
                HueBridge.SetColor(color,group,bulp);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            turned_on = false;
            HueBridge.TurnOn(false, color, group, bulp);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HueBridge.Alert("lselect", color, group, bulp);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HueBridge.Effect("colorloop", group, bulp);
        }
    }
}
