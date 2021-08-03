using Intel.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace passwordVault2Host
{
    public partial class LoginWin : Form
    {
        private Jhi jhi;
        private JhiSession session;
        private string name = "";

        public LoginWin(Jhi _jhi, JhiSession _session)
        {
            InitializeComponent();
            jhi = _jhi;
            session = _session;

            if (!File.Exists("PasswordVault.txt"))
            {
                StreamWriter sw = File.CreateText("PasswordVault.txt");
                sw.Close();
            }
            ifIsNotFirstTime();
        }

        
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (txt_Password.Text == "")
            {
                MessageBox.Show("Please provide Password");
                return;
            }
            try
            {
                byte[] sendBuff = UTF32Encoding.UTF8.GetBytes(txt_Password.Text);
                byte[] recvBuff = new byte[2000];
                int responseCode;
                int cmdId = 5;
                jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
                
                if (UTF32Encoding.UTF8.GetString(recvBuff) == "true")
                {
                    txt_Password.Text = "";
                    MainWin fm = new MainWin(jhi, session);
                    fm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Wrong password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            if (name_txt.Text == "")
            {
                MessageBox.Show("Please provide your name");
                return;
            }
            if (signPassword_txt.Text == "" || signVerifyPassword_txt.Text == "")
            {
                MessageBox.Show("Please provide Password");
                return;
            }
            if (signPassword_txt.Text.Length < 9)
            {
                MessageBox.Show("Password must be at least 9 characters long");
                return;
            }
            if (signPassword_txt.Text != signVerifyPassword_txt.Text)
            {
                MessageBox.Show("Passwords are not the same");
                return;
            }
            for(int i = 0; i < signPassword_txt.Text.Length; i++)
                if(signPassword_txt.Text[i] == '$' || signPassword_txt.Text[i] == '#')
                {
                    MessageBox.Show("Password cannot contain $ or # characters");
                    return;
                }

            byte[] sendBuff = UTF32Encoding.UTF8.GetBytes(signPassword_txt.Text);
            byte[] recvBuff = new byte[2000];
            int responseCode;
            int cmdId = 2;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);

            if (name == "")
            {
                name = name_txt.Text;
                StreamWriter sw = new StreamWriter("PasswordVault.txt");
                sw.WriteLine(name);
                sw.Close();
            }


            userName.Text = "Hello, " + name + "!";

            signPassword_txt.Visible = false;
            signVerifyPassword_txt.Visible = false;
            name_txt.Visible = false;
            labelName.Visible = false;
            labelPass.Visible = false;
            labelVerifyPass.Visible = false;
            buttonSignup.Visible = false;

            dataGridView1.Visible = true;
            userName.Visible = true;
            labelEnterPass.Visible = true;
            txt_Password.Visible = true;
            buttonLogin.Visible = true;           
        }

        private void ifIsNotFirstTime()
        {
            byte[] sendBuff = UTF32Encoding.UTF8.GetBytes("");
            byte[] recvBuff = new byte[2000];
            int responseCode;
            int cmdId = 1;
            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);

            if (UTF32Encoding.UTF8.GetString(recvBuff) == "true")
            {
                StreamReader sr = new StreamReader("PasswordVault.txt");
                name = sr.ReadLine();
                sr.Close();

                userName.Text = "Hello, " + name + "!";

                signPassword_txt.Visible = false;
                signVerifyPassword_txt.Visible = false;
                name_txt.Visible = false;
                labelName.Visible = false;
                labelPass.Visible = false;
                labelVerifyPass.Visible = false;
                buttonSignup.Visible = false;

                dataGridView1.Visible = true;
                userName.Visible = true;
                labelEnterPass.Visible = true;
                txt_Password.Visible = true;
                buttonLogin.Visible = true;
            }           
        }
    }
}
