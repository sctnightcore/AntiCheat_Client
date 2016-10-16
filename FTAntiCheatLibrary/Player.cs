using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAntiCheatLibrary
{
    public class Player
    {
        #region Fields
        private string m_XUID;
        private string m_PlayerName;
        #endregion

        #region Properties
        public string XUID
        {
            get { return m_XUID; }
            set { m_XUID = value; }
        }

        public string Name
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }
        #endregion

        #region Constructors

        public Player()
        {
        }

        public Player(string XUID, string PlayerName)
        {
            this.XUID = XUID;
            this.Name = PlayerName;
        }

        #endregion

        #region Methods

        #endregion
    }
}
