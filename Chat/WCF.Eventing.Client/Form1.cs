using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace WCF.Eventing.Client
{
    public partial class Form1 : Form, ServiceReference1.IMyServiceCallback
    {
        ServiceReference1.MyServiceClient _client;
        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Disconnected.";
            groupBox2.Enabled = false;
            InstanceContext context = new InstanceContext(this);
            _client = new WCF.Eventing.Client.ServiceReference1.MyServiceClient(context);
            WSDualHttpBinding _binding = _client.Endpoint.Binding as WSDualHttpBinding;
            _binding.ClientBaseAddress = new Uri(_binding.ClientBaseAddress.ToString() + Guid.NewGuid().ToString()); 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a name.");
            }
            else
            {
                toolStripStatusLabel1.Text = "Connecting. Please wait...";
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                
                txtMsg.Focus();
                _client.Join(txtName.Text);
                toolStripStatusLabel1.Text = "Connected.";
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMsg.Text.Trim().Length != 0)
                {
                    _client.SendMessage(txtName.Text, txtMsg.Text);
                    txtMsg.Clear();
                }
            }
            catch
            {
                string errormsg = "The id you have chosen is already in use ! Please enter a different chat name and join.";
                MessageBox.Show(errormsg,"Identitity Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel1.Text = "Disconnected.";
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                txtName.Clear();
                InstanceContext context = new InstanceContext(this);
                _client = new WCF.Eventing.Client.ServiceReference1.MyServiceClient(context);
            }
            
        }

        #region IMyServiceCallback Members

        public void ReceiveMessage(string message)
        {
            lstMessageBoard.Items.Add(message);
        }

        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (groupBox2.Enabled)
                {

                    toolStripStatusLabel1.Text = "Disconnecting. Please Wait...";
                    _client.Leave(txtName.Text);
                    toolStripStatusLabel1.Text = "Disconnected.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(txtName.Text+" : "+ex.Message);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Disconnecting. Please Wait...";
            _client.Leave(txtName.Text);
            toolStripStatusLabel1.Text = "Disconnected.";
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
        }

        private void txtMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSend_Click(sender, e);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin_Click(sender, e);
            }
        }

       

        
    }
}
