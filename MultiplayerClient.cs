using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerClient : MonoBehaviour
{
    [SerializeField]
    private Transform localPlayer;
    [SerializeField]
    private Transform netWorkPlayer1;

    static Socket serverSocket;
    static byte clientNumber;

    static string host;
    static int port;

    static Dictionary<byte, Vector3> networkClientPositions;

    // Start is called before the first frame update
    void Start()
    {
        host = "127.0.0.1";
        port = 12345;
        clientNumber = 255;

        serverSocket = ConnectSocket(host, port);

        networkClientPositions = new Dictionary<byte, Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        SocketSendReceive();

        foreach(var netClient in networkClientPositions)
        {
            netWorkPlayer1.SetPositionAndRotation(netClient.Value, new Quaternion());
        }
    }

    void OnDestroy()
    {
        serverSocket.Close();
    }

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
            ClientData sendData = new ClientData();
            sendData.clientNumber = clientNumber;
            sendData.dataType = PacketType.POSITION_UPDATE;

            sendData.x = localPlayer.position.x;
            sendData.y = localPlayer.position.y;
            sendData.z = localPlayer.position.z;

            // Create a socket connection with the specified server and port.
            try
            {
                // Send request to the server.
                serverSocket.Send(sendData.raw, sendData.raw.Length, 0);
            }
            catch (Exception e)
            {
                serverSocket = ConnectSocket(host, port);
            }
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        print("Waiting for incoming data...");
        try
        {
            // Retrieve the state object and the client socket
            // from the asynchronous state object.  
            ClientData dataReceived = (ClientData)ar.AsyncState;
            dataReceived.deserialize();

            // Read data from the remote device.  
            int bytesRead = serverSocket.EndReceive(ar);

            print("Got stuff... BytesRead: " + bytesRead + ". " + dataReceived.dataType);

            if (bytesRead > 0)
            {
                if (dataReceived.dataType == PacketType.CLIENT_ID_UPDATE)
                {
                    clientNumber = dataReceived.clientNumber;
                    print("My client Id is " + clientNumber + "!");
                }
                else if (dataReceived.dataType == PacketType.POSITION_UPDATE)
                {
                    print("Got client position!");
                    byte clientNum = dataReceived.clientNumber;

                    if (clientNum != clientNumber)
                    {
                        Vector3 pos = new Vector3(dataReceived.x, dataReceived.y, dataReceived.z);
                        networkClientPositions[clientNum] = pos;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
