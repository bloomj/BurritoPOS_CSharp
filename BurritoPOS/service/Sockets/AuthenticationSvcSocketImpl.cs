using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using System.IO;
using System.Net;
using System.Net.Sockets;
using BurritoPOS.domain;

namespace BurritoPOS.service.Sockets
{
    /// <summary>
    /// 
    /// </summary>
    class AuthenticationSvcSocketImpl : IAuthenticationSvc
    {
        private static ILog dLog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Socket client;
        private byte[] receiveBuffer = new byte[4096];
        private byte[] writeBuffer = new byte[4096];
        String IAuthenticationSvc.NAME
        {
            get { return "IAuthenticationSvc"; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AuthenticationSvcSocketImpl()
        {
            XmlConfigurator.Configure(new FileInfo("config/log4net.properties"));
        }

        public Boolean login(Employee e, String password) 
        {
            Boolean result = false;

            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPEndPoint localEP = new IPEndPoint(ipHostInfo.AddressList[0], 8000);
                foreach (IPAddress ip in ipHostInfo.AddressList)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        localEP = new IPEndPoint(ip, 8000);

                dLog.Debug("Local address and port: " + localEP.Address.ToString() + " | " + localEP.Port.ToString());

                client = new Socket(localEP.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(localEP.Address, 8000);

                String inputStr = readObject();
                writeObject(e.firstName);
                inputStr = readObject();
                writeObject(password);
                inputStr = readObject();

                //check out input
                if (inputStr.Split(' ')[0].Equals("OK"))
                    result = true;

                writeObject("exit");
            }
            catch (Exception e1)
            {
                dLog.Error("Exception in login: " + e1.Message);
            }
            finally
            {
                if (client.Connected)
                    client.Shutdown(SocketShutdown.Both);

                if (client.IsBound)
                {
                    client.Close();
                    client = null;
                }
            }

            return result;
        }

        public Boolean login(Manager m, String password)
        {
            Boolean result = false;

            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPEndPoint localEP = new IPEndPoint(ipHostInfo.AddressList[0], 8000);
                foreach (IPAddress ip in ipHostInfo.AddressList)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        localEP = new IPEndPoint(ip, 8000);

                dLog.Debug("Local address and port: " + localEP.Address.ToString() + " | " + localEP.Port.ToString());

                client = new Socket(localEP.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(localEP.Address, 8000);

                String inputStr = readObject();
                writeObject(m.firstName);
                inputStr = readObject();
                writeObject(password);
                inputStr = readObject();

                //check out input
                if (inputStr.Split(' ')[0].Equals("OK"))
                    result = true;

                writeObject("exit");
            }
            catch (Exception e1)
            {
                dLog.Error("Exception in login: " + e1.Message);
            }
            finally
            {
                if (client.Connected)
                    client.Shutdown(SocketShutdown.Both);

                if (client.IsBound)
                {
                    client.Close();
                    client = null;
                }
            }

            return result;
        }

        private void writeObject(String msg)
        {
            try
            {
                writeBuffer = Encoding.ASCII.GetBytes(msg);
                client.Send(writeBuffer);
                dLog.Debug("OUPUT | " + msg);
            }
            catch (SocketException se)
            {

            }
            catch (Exception e)
            {
                dLog.Error("Exception in writeObject: " + e.Message + "\n" + e.StackTrace);
            }
        }

        private String readObject()
        {
            String msg = "";

            try
            {
                dLog.Debug("In readObject");
                while (isConnected() && msg == "")
                {
                    receiveBuffer = new byte[4096];
                    int recBytes = client.Receive(receiveBuffer);
                    msg = Encoding.ASCII.GetString(receiveBuffer, 0, recBytes).Trim();
                }

                dLog.Debug("INPUT | " + msg);
            }
            catch (SocketException se)
            {

            }
            catch (Exception e)
            {
                dLog.Error("Exception in readObject: " + e.Message + "\n" + e.StackTrace);
                msg = "";
            }

            return msg;
        }

        public Boolean isConnected()
        {
            Boolean result = false;

            try
            {
                if (client != null)
                {
                    bool part1 = client.Poll(1000, SelectMode.SelectRead);
                    bool part2 = (client.Available == 0);
                    if (part1 & part2)
                        result = false;
                    else
                        result = true;
                }
            }
            catch (Exception e)
            {
                dLog.Error("Exception in isConnected: " + e.Message + "\n" + e.StackTrace);
            }

            return result;
        }
    }
}
