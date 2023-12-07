//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Unity.Netcode;

//using Unity.Netcode.Transports.UTP;
//using System.Net;
//using System.Net.Sockets;

//public class NetworkManagerUI : MonoBehaviour
//{

//    [SerializeField] private Button serverButton;
//    [SerializeField] private Button hostButton;
//    [SerializeField] private Button clientButton;

//    void Awake()
//    {
//        serverButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
//        hostButton.onClick.AddListener(() => { Do(); });
//        clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
//    }

//    private void Do()
//    {
//        NetworkManager.Singleton.StartHost();
//        Debug.Log(GetLocalIPAddress());
//    }



//    public string GetLocalIPAddress()
//    {
//        var host = Dns.GetHostEntry(Dns.GetHostName());
//        foreach (var ip in host.AddressList)
//        {
//            if (ip.AddressFamily == AddressFamily.InterNetwork)
//            {
//                //ipAddressText.text = ip.ToString();
//                //ipAddress = ip.ToString();
//                return ip.ToString();
//            }
//        }
//        throw new System.Exception("No network adapters with an IPv4 address in the system!");
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net;
using System.Net.Sockets;

public class NetworkManagerUI : MonoBehaviour
{
	private bool pcAssigned;

	[SerializeField] TextMeshProUGUI ipAddressText;
	[SerializeField] TMP_InputField ip;

	[SerializeField] string ipAddress;
	[SerializeField] UnityTransport transport;

	void Start()
	{
		ipAddress = "0.0.0.0";
		SetIpAddress(); // Set the Ip to the above address
		pcAssigned = false;
		//InvokeRepeating("assignPlayerController", 0.1f, 0.1f);
	}

	// To Host a game
	public void StartHost()
	{
		NetworkManager.Singleton.StartHost();
		GetLocalIPAddress();
	}

	// To Join a game
	public void StartClient()
	{
		ipAddress = ip.text;
		SetIpAddress();
		NetworkManager.Singleton.StartClient();
	}

	/* Gets the Ip Address of your connected network and
	shows on the screen in order to let other players join
	by inputing that Ip in the input field */
	// ONLY FOR HOST SIDE 
	public string GetLocalIPAddress()
	{
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				ipAddressText.text = ip.ToString();
				ipAddress = ip.ToString();
				return ip.ToString();
			}
		}
		throw new System.Exception("No network adapters with an IPv4 address in the system!");
	}

	/* Sets the Ip Address of the Connection Data in Unity Transport
	to the Ip Address which was input in the Input Field */
	// ONLY FOR CLIENT SIDE
	public void SetIpAddress()
	{
		transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
		transport.ConnectionData.Address = ipAddress;
	}



	

}
