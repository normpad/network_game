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
    [SerializeField]
    private Transform localPlayer;
    [SerializeField]
    private Transform netWorkPlayer1;

    static Socket serverSocket;
    static byte clientNumber;

    static string host;
    static int port;

    static Dictionary<byte, Vector3> networkClientPositions;
    static Dictionary<byte, Vector3> networkClientRotations;
    static Mutex mut = new Mutex();

    // Start is called before the first frame update
    void Start()
    {
        host = "138.88.148.113";
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
                print("This dude has location and rotation!");
                rot = networkClientRotations[netClient.Key];
            }
            netWorkPlayer1.position = netClient.Value;
            netWorkPlayer1.eulerAngles = rot;
        }
        mut.ReleaseMutex();
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
                        Vector3 pos = new Vector3(dataReceived.xPos, dataReceived.yPos, dataReceived.zPos);
                        mut.WaitOne();
                        networkClientPositions[clientNum] = pos;
                        mut.ReleaseMutex();
                    }
                }
                else if (dataReceived.dataType == PacketType.ROTATION_UPDATE)
                {
                    print("Got client rotation!");
                    byte clientNum = dataReceived.clientNumber;

                    if(clientNum != clientNumber)
                    {
                        Vector3 rot = new Vector3(dataReceived.xRot, dataReceived.yRot, dataReceived.zRot);
                        print(rot);
                        mut.WaitOne();
                        networkClientRotations[clientNum] = rot;
                        mut.ReleaseMutex();
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
