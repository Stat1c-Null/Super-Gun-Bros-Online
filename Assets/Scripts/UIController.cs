using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class UIController : NetworkBehaviour {
  

    [SerializeField] private GameObject CreateSession;
    [SerializeField] private GameObject JoinSession;

    public override void OnNetworkSpawn() 
    {
      if(IsClient && IsOwner) { //Check if this is the local player instance
        HideLobbyUIClientRpc();
      }
    }

    [ClientRpc]//Run only for client who owns this instance
    private void HideLobbyUIClientRpc(ClientRpcParams clientRpcParams = default) 
    {
      if(IsOwner) {
        CreateSession.SetActive(false);
        JoinSession.SetActive(false);
      }
    }

}
