using Unity.Netcode;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private GameObject disconnectButton;
    [SerializeField] private GameObject connectManagerPlate;

    public void StartServer() => NetworkManager.Singleton.StartServer();

    public void StartHost() 
    {
        NetworkManager.Singleton.StartHost();
        disconnectButton.SetActive(true);
        connectManagerPlate.SetActive(false);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        disconnectButton.SetActive(true);
        connectManagerPlate.SetActive(false);
    }

    public void DisconnectServer()
    {
        NetworkManager.Singleton.Shutdown();
        connectManagerPlate.SetActive(true);
        disconnectButton.SetActive(false);
    }
}
