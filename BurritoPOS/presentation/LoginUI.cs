using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using System.IO;
using Spring;
using Spring.Context;
using Spring.Context.Support;
using BurritoPOS.domain;
using BurritoPOS.service;
using BurritoPOS.service.Sockets;

namespace BurritoPOS.presentation
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LoginUI : Form
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IAuthenticationSvc authSvc;

        /// <summary>
        /// 
        /// </summary>
        public LoginUI()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
            InitializeComponent();

            //will comment out after Spring.NET implementation
            //authSvc = (IAuthenticationSvc)factory.getService("IAuthenticationSvc");

            //Spring.NET
            XmlApplicationContext ctx = new XmlApplicationContext("config/spring.cfg.xml");
            authSvc = (IAuthenticationSvc)ctx.GetObject("authSvc");

            //authSvc = new AuthenticationSvcSocketImpl();

            this.AcceptButton = loginBtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authSvc"></param>
        public void setAuthenticationSvc(IAuthenticationSvc authSvc)
        {
            this.authSvc = authSvc;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (userTxt.Text != "" && passTxt.Text != "")
                {
                    dLog.Debug("User/Pass are good");
                    Employee emp = new Employee(userTxt.Text, userTxt.Text, 1);

                    //if (authSvc.login(emp, BCrypt.HashPassword(passTxt.Text, BCrypt.GenerateSalt())))
                    if(authSvc.login(emp, passTxt.Text))
                    {
                        dLog.Debug("User authenticated; launching Neato Burrito App");

                        this.Hide();

                        MainUI mainUI = new MainUI();
                        mainUI.ShowDialog();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception e1)
            {
                dLog.Error("Exception in loginBtn_Click: " + e1.Message + "\n" + e1.StackTrace);
            }
        }
    }
}
