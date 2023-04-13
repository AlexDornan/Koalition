using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reg formReg = new reg();
            formReg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loggin loggin = new loggin();
            loggin.Show();
        }
    }
}
