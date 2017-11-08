using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTester
{
    /// <summary>
    /// Pinging a specific address and returning the status
    /// </summary>
    internal class Pinger
    {
        #region Local Vars
        private readonly Ping _ping;
        #endregion
        /// <summary>
        /// Default Constructor initializing the pinger
        /// </summary>
        public Pinger()
        {
            _ping = new Ping();
        }
        /// <summary>
        /// Ping to the requested adress and give the status back
        /// </summary>
        /// <param name="address"></param>
        public PingReply PingTo(string address)
        {
            try
            {
                return _ping.Send(address);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}