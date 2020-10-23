using Photon.Pun;
using UnityEngine;
using System.IO;

public class GameSetupController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), new Vector3(0,1,0), Quaternion.identity); // "PhotonPlayer = player gameobject name"

    }

}
