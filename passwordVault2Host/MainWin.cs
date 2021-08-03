using Intel.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace passwordVault2Host
{  
    public partial class MainWin : Form
    {
        private static readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private bool mainTimer = false;

        public class web
        {
            public class user
            {
                public string name;
                public byte[] password;

                public user(string newName, byte[] newPass)
                {
                    name = newName;
                    password = new byte[16];
                    newPass.CopyTo(password, 0);
                }
            }
            public string webName;
            public List<user> users;

            public web(string name)
            {
                webName = name;
                users = new List<user>();
            }
        }

        private Jhi jhi;
        private JhiSession session;
        private List<web> websites;

        public MainWin(Jhi _jhi, JhiSession _session)
        {
            InitializeComponent();
            jhi = _jhi;

            mainTimer = true;
            timer.Tick += TimerEventProcessor;
            timer.Interval = 3000; //milisec
            timer.Start();

            session = _session;
            websites = new List<web>();
            readFile();
            foreach (web w in websites)
            {
                CB_mainWeb.Items.Add(w.webName);
                CB_secWeb.Items.Add(w.webName);
            }
        }

        private void readFile()
        {
            StreamReader sr = new StreamReader("PasswordVault.txt");
            string line = sr.ReadLine();
            line = sr.ReadLine();
            while (line != null)
            {
                string[] webLine = line.Split('#');
                web newWeb = new web(webLine[0]);
                for(int i = 1; i < webLine.Length-1; i++)
                {
                    string[] oneUser = webLine[i].Split('$');
                    byte[] bytesPass = Convert.FromBase64String(oneUser[1]);
                    web.user user = new web.user(oneUser[0], bytesPass);
                    newWeb.users.Add(user);
                }
                websites.Add(newWeb);
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void CB_mainWeb_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_users.Items.Clear();

            foreach (web w in websites)
                if(CB_mainWeb.Text == w.webName)
                {
                    foreach(web.user u in w.users)
                        CB_users.Items.Add(u.name);
                    break;
                }
        }

        private void selectWeb_Click(object sender, EventArgs e)
        {
            if (CB_mainWeb.Items.Count == 0)
            {
                MessageBox.Show("There are no websites");
                return;
            }
            if (CB_mainWeb.Text == "")
            {
                MessageBox.Show("Choose website");
                return;
            }

            if (CB_users.Items.Count == 0)
            {
                MessageBox.Show("There are no users");
                return;
            }
            if (CB_users.Text == "")
            {
                MessageBox.Show("Choose user");
                return;
            }
            
            foreach (web w in websites)
                if (CB_mainWeb.Text == w.webName)
                    foreach (web.user u in w.users)
                        if(CB_users.Text == u.name)
                        {
                            CB_users.SelectedItem = null;
                            CB_mainWeb.SelectedItem = null;

                            byte[] sendBuff = new byte[16];
                            u.password.CopyTo(sendBuff, 0);

                            byte[] recvBuff = new byte[2000];
                            int responseCode;
                            int cmdId = 3;
                            jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);

                            Thread thread = new Thread(() => Clipboard.SetText(UTF32Encoding.UTF8.GetString(recvBuff)));
                            thread.SetApartmentState(ApartmentState.STA);
                            thread.Start();
                            thread.Join();
                            MessageBox.Show("Password copied");
                            return;
                        }
        }

        private void buttonAddWeb_Click(object sender, EventArgs e)
        {
            if(TB_newWeb.Text == "")
            {
                MessageBox.Show("Please enter the website name");
                return;
            }
            foreach(web w in websites)
                if(TB_newWeb.Text == w.webName)
                {
                    MessageBox.Show("The name of the website already exists");
                    TB_newWeb.Text = "";
                    return;
                }

            CB_mainWeb.Items.Add(TB_newWeb.Text);
            CB_secWeb.Items.Add(TB_newWeb.Text); 
            websites.Add(new web(TB_newWeb.Text));
            using (StreamWriter sw = File.AppendText("PasswordVault.txt"))
            {
                sw.WriteLine(TB_newWeb.Text + "#" + TB_newWeb.Text);
                sw.Close();
            }
            TB_newWeb.Text = "";
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            if (TB_user.Text == "")
            {
                MessageBox.Show("Please provide UserName");
                return;
            }
            if(!checkBox1.Checked)
            {
                if (TB_pass.Text == "" || TB_verifyPass.Text == "")
                {
                    MessageBox.Show("Please provide Password");
                    return;
                }
                if (TB_pass.Text != TB_verifyPass.Text)
                {
                    MessageBox.Show("Passwords are not the same");
                    return;
                }
            }
            
            for (int i = 0; i < TB_pass.Text.Length; i++)
                if (TB_pass.Text[i] == '$' || TB_pass.Text[i] == '#')
                {
                    MessageBox.Show("Password cannot contain $ or # characters");
                    return;
                }

            foreach (web w in websites)
                if (CB_secWeb.Text == w.webName)
                {
                    foreach (web.user u in w.users)
                        if(u.name == TB_user.Text)
                        {
                            MessageBox.Show("User already exists");
                            TB_user.Text = "";
                            TB_pass.Text = "";
                            TB_verifyPass.Text = "";
                            return;
                        }

                    byte[] sendBuff;
             
                    if (checkBox1.Checked)
                    {
                        Random random = new Random();
                        int length = random.Next(10, 15);
                        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        sendBuff = UTF32Encoding.UTF8.GetBytes(new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray()));
                    }
                    else
                        sendBuff = UTF32Encoding.UTF8.GetBytes(TB_pass.Text);

                    byte[] recvBuff = new byte[2000];
                    int responseCode;
                    int cmdId = 4;
                    jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);

                    string passCrip = Convert.ToBase64String(recvBuff);

                    w.users.Add(new web.user(TB_user.Text, recvBuff));
                    string text = File.ReadAllText("PasswordVault.txt");
                    text = text.Replace("#" + w.webName, "#" + TB_user.Text + "$" + passCrip + "#" + w.webName);
                    File.WriteAllText("PasswordVault.txt", text);
                    TB_user.Text = "";
                    TB_pass.Text = "";
                    TB_verifyPass.Text = "";
                    CB_secWeb.SelectedItem = null;
                    return;
                }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                TB_pass.Text = "";
                TB_pass.Enabled = false;
                TB_pass.BackColor = Color.Gray;
                TB_verifyPass.Text = "";
                TB_verifyPass.Enabled = false;
                TB_verifyPass.BackColor = Color.Gray;
            }
            else
            {
                TB_pass.Enabled = true;
                TB_pass.BackColor = Color.White;
                TB_verifyPass.Enabled = true;
                TB_verifyPass.BackColor = Color.White;
            }
        }

        private void TimerEventProcessor(object myObject, EventArgs myEventArgs)
        {
            if (mainTimer)
            {               
                byte[] sendBuff = UTF32Encoding.UTF8.GetBytes("");
                byte[] recvBuff = new byte[2000];
                int responseCode;
                int cmdId = 9;
                jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
                if (!UTF32Encoding.UTF8.GetString(recvBuff).Equals("LimitOk"))
                {
                    mainTimer = false;
                    timer.Stop();
                    Thread thread = new Thread(() => Clipboard.Clear());
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    this.Close();
                }
            }
        }


        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (mainTimer)
            {
                base.OnFormClosed(e);
                byte[] sendBuff = UTF32Encoding.UTF8.GetBytes("");
                byte[] recvBuff = new byte[2000];
                int responseCode;
                int cmdId = 10;
                jhi.SendAndRecv2(session, cmdId, sendBuff, ref recvBuff, out responseCode);
            }

            timer.Stop();
            Thread thread = new Thread(() => Clipboard.Clear());
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
