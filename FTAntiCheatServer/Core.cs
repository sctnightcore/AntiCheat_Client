using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FTAntiCheatLibrary;

namespace FTAntiCheatLibrary
{
    public class Core: MarshalByRefObject, IFTAntiCheat
    {
        private DataTable OnlinePlayers = new DataTable();

        Core()
        {
            OnlinePlayers.Columns.Add("XUID");
            OnlinePlayers.Columns.Add("PlayerName");
            OnlinePlayers.Columns.Add("SSRequested");
            OnlinePlayers.Columns.Add("TerminateState");
        }

        public bool AddPlayerToList(string XUID, string PlayerName)
        {
            DataRow tempDR = OnlinePlayers.NewRow();

            if (!IsPlayerOnTheList(XUID))
            {         
                tempDR["XUID"] = XUID;
                tempDR["PlayerName"] = PlayerName;
                tempDR["SSRequested"] = 0;
                tempDR["TerminateState"] = 0;
                OnlinePlayers.Rows.Add(tempDR);

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IsPlayerOnTheList(string XUID)
        {
            bool CheckBool = false;

            if (OnlinePlayers.Rows.Count > 0)
            {
                for (int i = 0; i < OnlinePlayers.Rows.Count; i++)
                {
                    if (OnlinePlayers.Rows[i]["XUID"].ToString() == XUID)
                    {
                        CheckBool = true;
                    }
                }
            }

            return CheckBool;
        }

        public bool RemovePlayerFromList(string XUID)
        {
            bool tempCheck = false;

            if (OnlinePlayers.Rows.Count > 0)
            {
                for (int i = 0; i < OnlinePlayers.Rows.Count; i++)
                {
                    if (OnlinePlayers.Rows[i]["XUID"].ToString() == XUID)
                    {
                        OnlinePlayers.Rows[i].Delete();
                        tempCheck = true;
                    }
                }
            }

            return tempCheck;
        }

        public void RequestSS(string XUID)
        {
            if (OnlinePlayers.Rows.Count > 0)
            {
                for (int i = 0; i < OnlinePlayers.Rows.Count; i++)
                {
                    if (OnlinePlayers.Rows[i]["XUID"].ToString() == XUID && Int32.Parse(OnlinePlayers.Rows[i]["SSRequested"].ToString()) == 0)
                    {
                        OnlinePlayers.Rows[i]["SSRequested"] = 1;
                    }
                }
            }
        }

        public bool SSRequested(string XUID)
        {
            bool tempCheck = false;

            if (OnlinePlayers.Rows.Count > 0)
            {
                for (int i = 0; i < OnlinePlayers.Rows.Count; i++)
                {
                    if (OnlinePlayers.Rows[i]["XUID"].ToString() == XUID && Int32.Parse(OnlinePlayers.Rows[i]["SSRequested"].ToString()) == 1)
                    {
                        tempCheck = true;
                    }
                }
            }

            return tempCheck;
        }

        public void SSSended(string XUID)
        {
            if (OnlinePlayers.Rows.Count > 0)
            {
                for (int i = 0; i < OnlinePlayers.Rows.Count; i++)
                {
                    if (OnlinePlayers.Rows[i]["XUID"].ToString() == XUID && Int32.Parse(OnlinePlayers.Rows[i]["SSRequested"].ToString()) == 1)
                    {
                        OnlinePlayers.Rows[i]["SSRequested"] = 0;
                    }
                }
            }
        }

        public DataTable PlayerList()
        {
            return OnlinePlayers;
        }

        public void TerminatePlayer(string XUID, int terminateValue)
        {
            if (OnlinePlayers.Rows.Count > 0)
            {
                for (int i = 0; i < OnlinePlayers.Rows.Count; i++)
                {
                    if (OnlinePlayers.Rows[i]["XUID"].ToString() == XUID)
                    {
                        OnlinePlayers.Rows[i]["TerminateState"] = terminateValue;
                    }
                }
            }
        }

        public int GetPlayersTerminateState(string XUID)
        {
            int tempInt = 0;

            if (OnlinePlayers.Rows.Count > 0)
            {
                for (int i = 0; i < OnlinePlayers.Rows.Count; i++)
                {
                    if (OnlinePlayers.Rows[i]["XUID"].ToString() == XUID)
                    {
                        tempInt = Int32.Parse(OnlinePlayers.Rows[i]["TerminateState"].ToString());
                    }
                }
            }

            return tempInt;
        }
    }
}
