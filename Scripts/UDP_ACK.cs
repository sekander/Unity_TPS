using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UDP_ACK : MonoBehaviour
{

    private static UDP_server instance;
    private UdpClient udpServer;
    private Thread serverThread;
    private bool isServerRunning = false;
    //public int hostingPort = 8080;
    private int hostingPort;
    //public int sendingPort = 8081;
    private int sendingPort;

    public Text text;

    public string messagePack = "Searching For Connection";

    public string sendMessage;
    IPAddress remoteAddress = IPAddress.Parse("192.168.2.15");

    string receivedMessage;

    bool startTransmission = false;

    // Singleton instance property
    public static UDP_server Instance
    {
        get
        {
            if (instance == null)
            {
                // Create a new GameObject with the UDP_server script attached

                GameObject singletonObject = new GameObject("UDP_server");
                instance = singletonObject.AddComponent<UDP_server>();
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }


    private void Start()
    {
        //       StartServer();
    }

    private void OnDestroy()
    {
        StopServer();
    }

    //private void StartServer()
    public void StartServer()
    {
        //udpServer = new UdpClient(12345);
        udpServer = new UdpClient(hostingPort);
        serverThread = new Thread(ServerThreadFunction);
        serverThread.Start();

        //// Start a new thread for transmitting data
        //Thread transmitThread = new Thread(() =>
        //{
        //    // Perform any non-Unity-related tasks here
        //    // ...

        //    // Call the transmitData method
        //    transmitData(sendMessage);
        //});

        // Start the thread
        //transmitThread.Start();
        isServerRunning = true;
    }
    public void StartHostServer()
    // private void StartHostServer()
    {
        // hostingPort = 8080;
        hostingPort = 8080;
        // hostingPort = 12345;
        sendingPort = 8081;
        //udpServer = new UdpClient(12345);
        udpServer = new UdpClient(hostingPort);
        serverThread = new Thread(ServerThreadFunction);
        serverThread.Start();

        isServerRunning = true;

        Debug.Log($"Server IP: {GameManger.instance.GetServerIP()}");
        Debug.Log($"Server PORT: {sendingPort}");


        transmitData("CONNECTION_REQUEST");
    }
    public void StartClientServer()
    // private void StartClientServer()
    {
        hostingPort = 8081;
        sendingPort = 8080;
        //udpServer = new UdpClient(12345);
        udpServer = new UdpClient(hostingPort);
        serverThread = new Thread(ServerThreadFunction);
        serverThread.Start();

        isServerRunning = true;
    }
    
    public void Is_User_Host()
    {
        if (GameManger.instance.GetIsPlayerHost() == true)
        {
            Debug.Log("USER IS HOST");
            StartHostServer();
        }
        else
        {
            Debug.Log("USER IS CLIENT");
            StartClientServer();
        }
    } 


    private void StopServer()
    {
        if (isServerRunning)
        {
            udpServer.Close();
            serverThread.Join(); // Wait for the server thread to finish
            isServerRunning = false;
        }
    }

    public void transmitData(string nd)
    {
        udpServer.Send(Encoding.UTF8.GetBytes(nd), nd.Length, GameManger.instance.GetServerIP(), sendingPort);
        //udpServer.Send(Encoding.UTF8.GetBytes(nd), nd.Length, "192.168.2.137", sendingPort);
        //udpServer.Send(Encoding.UTF8.GetBytes(nd.ToJsonString()), nd.ToJsonString().Length, "127.0.0.1", 12346);
        //udpServer.Send(Encoding.UTF8.GetBytes(nd.ToJsonString()), nd.ToJsonString().Length, "127.0.0.1", 12345);
        //StartCoroutine(TransmitDataCoroutine(nd));
    }

    
    private void ServerThreadFunction()
    {
        try
        {
            while (isServerRunning)
            {
                Debug.Log($"UDP ACK Server thread ID: {Thread.CurrentThread.ManagedThreadId}");

                //IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpServer.Receive(ref remoteEndPoint);

                //Check message 
                string message = System.Text.Encoding.UTF8.GetString(data);
                Debug.Log($"UDP ACK Received from {remoteEndPoint}: {message}");

                receivedMessage = message;

                ProcessReceivedData(remoteEndPoint, data);
                //ProcessJSONData(remoteEndPoint, data);

                //Thread.Sleep(1000);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in server thread: {e.Message}");
        }
    }
    private void ProcessReceivedData(IPEndPoint remoteEndPoint, byte[] data)
    {
        string message = System.Text.Encoding.UTF8.GetString(data);
        Debug.Log($"Received from {remoteEndPoint}: {message}");


        if (message == "CONNECT_REQUEST")
        {
            string acknowledgmentMessage = "CONNECT_ACK";
            receivedMessage = acknowledgmentMessage;
            sendMessage = acknowledgmentMessage;
            Thread.Sleep(1000);
            SendAcknowledgment(remoteEndPoint, acknowledgmentMessage);
        }
        else if (message == "CONNECT_ACK")
        {
            Debug.Log($"Connection established with {remoteEndPoint}");
            receivedMessage = message;
            sendMessage = "READY";
            Thread.Sleep(1000);
            SendAcknowledgment(remoteEndPoint, "READY");
            // Handle further communication with the client as needed
        }
        else if (message == "READY")
        {
            if(startTransmission)
            {
                receivedMessage = "Begin Transmission";

                //ProcessJSONData(remoteEndPoint, data);
            }
            else
            {
                sendMessage = "CONNECT_REQUEST";
                Thread.Sleep(1000);
                SendAcknowledgment(remoteEndPoint, "READY");
                startTransmission = true;
                // Debug.Log("START GAME");
                //Start Game Here Lan Mode
                //SceneManager.LoadScene(1);
            }
                //transmitData("CONNECTION_REQUEST");
        }
    }

    private void SendAcknowledgment(IPEndPoint clientEndPoint, string acknowledgmentMessage)
    {
        try
        {
            // Convert acknowledgment message to bytes
            byte[] acknowledgmentData = Encoding.UTF8.GetBytes(acknowledgmentMessage);


            // Send acknowledgment back to the client
            udpServer.Send(acknowledgmentData, acknowledgmentData.Length, clientEndPoint);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error sending acknowledgment: {e.Message}");
        }
    }

    //private void ProcessReceivedData(IPEndPoint remoteEndPoint, byte[] data)
      public void Update()
    {
        //text.text = receivedMessage;
        if (startTransmission)
        {

            StopServer();
            Debug.Log("START GAME");
            GameManger.instance.LoadPlayScene();


            //Intiate countdown
            //level.Online_Player();
        }

    }

}
