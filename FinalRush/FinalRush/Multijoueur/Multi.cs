using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;
using System.Collections;

namespace FinalRush
{
    class Multi : IPhotonPeerListener
    {
        public PhotonPeer peer;
        private bool connected;

        public Multi()
        {
            Global.Multi = this;
            peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        }

        public bool Connect()
        {
            if (peer.Connect("app-eu.exitgamescloud.com:5055", "5a97fca2-aca3-4f00-84cc-ce6ab3de6258"))
            {
                //Console.WriteLine("Status: Connected !");
                return true;
            }

            // connect might fail if e.g. the address format is bad.
            return false;
        }

        public void CreateLobby()
        {
            //peer.OpCreateRoom("Room 42", true, true, 2, new Hashtable(), new string[1], new Hashtable(), "test", LobbyType.Default);
        }

        public void JoinLobby()
        {
            // join random rooms easily, filtering for specific room properties, if needed
            Hashtable expectedCustomRoomProperties = new Hashtable();
            expectedCustomRoomProperties["map"] = 1; // custom props can have any name but the key must be string
            //peer.OpJoinRandomRoom(expectedCustomRoomProperties, 2, new Hashtable(), MatchmakingMode.RandomMatching, "test", LobbyType.Default, null);
        }

        void IPhotonPeerListener.DebugReturn(DebugLevel level, string message)
        {

        }

        void IPhotonPeerListener.OnEvent(EventData eventData)
        {
            Console.WriteLine(eventData.Code.ToString());
        }

        void IPhotonPeerListener.OnOperationResponse(OperationResponse operationResponse)
        {

        }

        public void OnStatusChanged(StatusCode statusCode)
        {
            Console.WriteLine("Status: " + statusCode);
        }
    }
}
