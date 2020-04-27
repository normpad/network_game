using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System.Threading;
using System.Runtime.InteropServices;

public class MultiplayerClient : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Transform localPlayer;

    #region NetworkPlayers
    [SerializeField]
    private Transform netWorkPlayer1;
    [SerializeField]
    private Transform netWorkPlayer2;
    [SerializeField]
    private Transform netWorkPlayer3;
    [SerializeField]
    private Transform netWorkPlayer4;
    [SerializeField]
    private Transform netWorkPlayer5;
    [SerializeField]
    private Transform netWorkPlayer6;
    [SerializeField]
    private Transform netWorkPlayer7;
    [SerializeField]
    private Transform netWorkPlayer8;
    [SerializeField]
    private Transform netWorkPlayer9;
    [SerializeField]
    private Transform netWorkPlayer10;
    [SerializeField]
    private Transform netWorkPlayer11;
    [SerializeField]
    private Transform netWorkPlayer12;
    [SerializeField]
    private Transform netWorkPlayer13;
    [SerializeField]
    private Transform netWorkPlayer14;
    [SerializeField]
    private Transform netWorkPlayer15;
    [SerializeField]
    private Transform netWorkPlayer16;
    [SerializeField]
    private Transform netWorkPlayer17;
    [SerializeField]
    private Transform netWorkPlayer18;
    [SerializeField]
    private Transform netWorkPlayer19;
    [SerializeField]
    private Transform netWorkPlayer20;
    [SerializeField]
    private Transform netWorkPlayer21;
    [SerializeField]
    private Transform netWorkPlayer22;
    [SerializeField]
    private Transform netWorkPlayer23;
    [SerializeField]
    private Transform netWorkPlayer24;
    [SerializeField]
    private Transform netWorkPlayer25;
    [SerializeField]
    private Transform netWorkPlayer26;
    [SerializeField]
    private Transform netWorkPlayer27;
    [SerializeField]
    private Transform netWorkPlayer28;
    [SerializeField]
    private Transform netWorkPlayer29;
    [SerializeField]
    private Transform netWorkPlayer30;
    #endregion
    #endregion

    #region Static Variables
    static Socket serverSocket;
    static byte clientNumber;

    static string host;
    static int port;

    static Dictionary<byte, Vector3> networkClientPositions;
    static Dictionary<byte, Vector3> networkClientRotations;
    static Mutex mut = new Mutex();
    #endregion

    #region Events
    // Start is called before the first frame update
    void Start()
    {
        host = "127.0.0.1";
        port = 12345;
        clientNumber = 255;

        serverSocket = ConnectSocket(host, port);

        networkClientPositions = new Dictionary<byte, Vector3>();
        networkClientRotations = new Dictionary<byte, Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        SocketSendReceive();

        mut.WaitOne();
        foreach (var netClient in networkClientPositions)
        {
            Vector3 rot = new Vector3();
            if (networkClientRotations.ContainsKey(netClient.Key))
            {
                rot = networkClientRotations[netClient.Key];
            }

            switch (netClient.Key)
            {
                case 0:
                    if (netWorkPlayer1 != null)
                    {
                        netWorkPlayer1.position = netClient.Value;
                        netWorkPlayer1.eulerAngles = rot;
                    }
                    break;
                case 1:
                    if (netWorkPlayer2 != null)
                    {
                        netWorkPlayer2.position = netClient.Value;
                        netWorkPlayer2.eulerAngles = rot;
                    }
                    break;
                case 2:
                    if (netWorkPlayer3 != null)
                    {
                        netWorkPlayer3.position = netClient.Value;
                        netWorkPlayer3.eulerAngles = rot;
                    }
                    break;
                case 3:
                    if (netWorkPlayer4 != null)
                    {
                        netWorkPlayer4.position = netClient.Value;
                        netWorkPlayer4.eulerAngles = rot;
                    }
                    break;
                case 4:
                    if (netWorkPlayer5 != null)
                    {
                        netWorkPlayer5.position = netClient.Value;
                        netWorkPlayer5.eulerAngles = rot;
                    }
                    break;
                case 5:
                    if (netWorkPlayer6 != null)
                    {
                        netWorkPlayer6.position = netClient.Value;
                        netWorkPlayer6.eulerAngles = rot;
                    }
                    break;
                case 6:
                    if (netWorkPlayer7 != null)
                    {
                        netWorkPlayer7.position = netClient.Value;
                        netWorkPlayer7.eulerAngles = rot;
                    }
                    break;
                case 7:
                    if (netWorkPlayer8 != null)
                    {
                        netWorkPlayer8.position = netClient.Value;
                        netWorkPlayer8.eulerAngles = rot;
                    }
                    break;
                case 8:
                    if (netWorkPlayer9 != null)
                    {
                        netWorkPlayer9.position = netClient.Value;
                        netWorkPlayer9.eulerAngles = rot;
                    }
                    break;
                case 9:
                    if (netWorkPlayer10 != null)
                    {
                        netWorkPlayer10.position = netClient.Value;
                        netWorkPlayer10.eulerAngles = rot;
                    }
                    break;
                case 10:
                    if (netWorkPlayer11 != null)
                    {
                        netWorkPlayer11.position = netClient.Value;
                        netWorkPlayer11.eulerAngles = rot;
                    }
                    break;
                case 11:
                    if (netWorkPlayer12 != null)
                    {
                        netWorkPlayer12.position = netClient.Value;
                        netWorkPlayer12.eulerAngles = rot;
                    }
                    break;
                case 12:
                    if (netWorkPlayer13 != null)
                    {
                        netWorkPlayer13.position = netClient.Value;
                        netWorkPlayer13.eulerAngles = rot;
                    }
                    break;
                case 13:
                    if (netWorkPlayer14 != null)
                    {
                        netWorkPlayer14.position = netClient.Value;
                        netWorkPlayer14.eulerAngles = rot;
                    }
                    break;
                case 14:
                    if (netWorkPlayer15 != null)
                    {
                        netWorkPlayer15.position = netClient.Value;
                        netWorkPlayer15.eulerAngles = rot;
                    }
                    break;
                case 15:
                    if (netWorkPlayer16 != null)
                    {
                        netWorkPlayer16.position = netClient.Value;
                        netWorkPlayer16.eulerAngles = rot;
                    }
                    break;
                case 16:
                    if (netWorkPlayer17 != null)
                    {
                        netWorkPlayer17.position = netClient.Value;
                        netWorkPlayer17.eulerAngles = rot;
                    }
                    break;
                case 17:
                    if (netWorkPlayer18 != null)
                    {
                        netWorkPlayer18.position = netClient.Value;
                        netWorkPlayer18.eulerAngles = rot;
                    }
                    break;
                case 18:
                    if (netWorkPlayer19 != null)
                    {
                        netWorkPlayer19.position = netClient.Value;
                        netWorkPlayer19.eulerAngles = rot;
                    }
                    break;
                case 19:
                    if (netWorkPlayer20 != null)
                    {
                        netWorkPlayer20.position = netClient.Value;
                        netWorkPlayer20.eulerAngles = rot;
                    }
                    break;
                case 20:
                    if (netWorkPlayer21 != null)
                    {
                        netWorkPlayer21.position = netClient.Value;
                        netWorkPlayer21.eulerAngles = rot;
                    }
                    break;
            }
        }
        mut.ReleaseMutex();
    }

    void OnDestroy()
    {
        serverSocket.Close();
    }

    #endregion

    #region Methods
    Socket ConnectSocket(string server, int port)
    {
        Socket s = null;
        //IPHostEntry hostEntry = null;
        IPAddress hostAddress = IPAddress.Parse(server);

        // Get host related information.
        //hostEntry = Dns.GetHostEntry(server);

        IPEndPoint ipe = new IPEndPoint(hostAddress, port);
        Socket tempSocket =
            new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        tempSocket.Connect(ipe);

        s = tempSocket;

        return s;
    }

    // This method requests the home page content for the specified server.
    void SocketSendReceive()
    {
        ClientData dataReceived = new ClientData();
        serverSocket.BeginReceive(dataReceived.raw, 0, ClientData.dataSize, 0,
            new AsyncCallback(ReceiveCallback), dataReceived);

        if (clientNumber != 255)
        {
            //Set up position data packets
            ClientData positionData = new ClientData(PacketType.POSITION_UPDATE);
            positionData.clientNumber = clientNumber;
            positionData.xPos = localPlayer.position.x;
            positionData.yPos = localPlayer.position.y;
            positionData.zPos = localPlayer.position.z;

            //Set up rotation data packets
            ClientData rotationData = new ClientData(PacketType.ROTATION_UPDATE);
            rotationData.clientNumber = clientNumber;
            rotationData.xRot = localPlayer.rotation.eulerAngles.x;
            rotationData.yRot = localPlayer.rotation.eulerAngles.y;
            rotationData.zRot = localPlayer.rotation.eulerAngles.z;

            // Create a socket connection with the specified server and port.
            try
            {
                // Send position to server
                serverSocket.Send(positionData.raw, positionData.raw.Length, 0);

                // Send rotation to server
                serverSocket.Send(rotationData.raw, rotationData.raw.Length, 0);
            }
            catch
            {
                serverSocket = ConnectSocket(host, port);
            }
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the state object and the client socket
            // from the asynchronous state object.  
            ClientData dataReceived = (ClientData)ar.AsyncState;
            dataReceived.deserialize();

            // Read data from the remote device.  
            int bytesRead = serverSocket.EndReceive(ar);

            if (bytesRead > 0)
            {
                if (dataReceived.dataType == PacketType.CLIENT_ID_UPDATE)
                {
                    clientNumber = dataReceived.clientNumber;
                }
                else if (dataReceived.dataType == PacketType.POSITION_UPDATE)
                {
                    byte clientNum = dataReceived.clientNumber;

                    if (clientNum != clientNumber)
                    {
                        Vector3 pos = new Vector3(dataReceived.xPos, dataReceived.yPos, dataReceived.zPos);
                        mut.WaitOne();
                        networkClientPositions[clientNum] = pos;
                        mut.ReleaseMutex();
                    }
                }
                else if (dataReceived.dataType == PacketType.ROTATION_UPDATE)
                {
                    byte clientNum = dataReceived.clientNumber;

                    if(clientNum != clientNumber)
                    {
                        Vector3 rot = new Vector3(dataReceived.xRot, dataReceived.yRot, dataReceived.zRot);
                        mut.WaitOne();
                        networkClientRotations[clientNum] = rot;
                        mut.ReleaseMutex();
                    }
                }

                serverSocket.BeginReceive(dataReceived.raw, 0, ClientData.dataSize, 0,
                    new AsyncCallback(ReceiveCallback), dataReceived);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    #endregion
}
