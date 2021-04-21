using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace SendEmail
{
    public partial class Form1 : Form
    {
        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            login = new NetworkCredential(txtUsername.Text, txtPassword.Text);
            client = new SmtpClient(txtSmtp.Text);
            client.Port = Convert.ToInt32(txtPort.Text);
            client.EnableSsl = chkSSL.Checked;
            client.Credentials =login ;
            msg = new MailMessage { From = new MailAddress(txtUsername.Text + txtSmtp.Text.Replace("smtp.", "@"), "Master", Encoding .UTF8 ) };
            msg.To.Add(new MailAddress(txtTo.Text));
            if (!string.IsNullOrEmpty(txtTo.Text)) ;
            msg.Subject = txtSubject.Text;
            msg.Body = txtMessage.Text;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            string userstate = "Sending....";
            client.SendAsync(msg, userstate);
        }
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        { 
            if(e.Cancelled)
                MessageBox .Show (string .Format ("{0} send canceled.", e.UserState ), "Message", MessageBoxButtons.OK,MessageBoxIcon .Information );
            if (e.Error != null)
                MessageBox.Show(string.Format("{0} {1}.", e.UserState, e.Error), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Your message has been successfully sent.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
