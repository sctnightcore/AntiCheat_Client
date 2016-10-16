using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FTAntiCheatLibrary
{
    public interface IFTAntiCheat
    {
        bool AddPlayerToList(string XUID, string PlayerName);
        bool IsPlayerOnTheList(string XUID);
        bool RemovePlayerFromList(string XUID);
        void RequestSS(string XUID);
        bool SSRequested(string XUID);
        void SSSended(string XUID);
        int GetPlayersTerminateState(string XUID);
        void TerminatePlayer(string XUID, int terminateValue);
        DataTable PlayerList();
    }
}
