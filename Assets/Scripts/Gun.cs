using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;


public class Gun : NetworkBehaviour {
    
    public GameObject Bullet;
    private int bulletCount;
    [SerializeField] private int maxAmmo;
    private float rotationX = 0.3f;
    private float bulletRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletCount = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < 0f) {
            rotationX = -0.3f;
        } else {
            rotationX = 0.3f;
        }
        
        //Check if player is shooting from their client window, and don't invoke other players to shoot
        if(Input.GetMouseButtonDown(0) && IsClient && IsOwner) {
            Vector3 spawnPosition = new Vector3(transform.position.x + rotationX, transform.position.y + 0.15f, transform.position.z);
            Quaternion bulletRotation = GetBulletRotation();
            ShootServerRpc(spawnPosition, bulletRotation);
        }
    }

    // Get bullet rotation based on player direction
    private Quaternion GetBulletRotation() {
        return transform.localScale.x < 0f 
            ? Quaternion.Euler(0f, 180f, 0f) // Flip rotation for left-facing
            : Quaternion.identity;          // Default rotation for right-facing
    }

    //Send request to server to shoot bullet from specified player
    [ServerRpc]
    private void ShootServerRpc(Vector3 spawnPosition, Quaternion rotation, ServerRpcParams serverRpcParams = default) {
        ulong clientId = serverRpcParams.Receive.SenderClientId;

        var bullet = Instantiate(Bullet, spawnPosition, rotation);
        var bulletNetworkObject = bullet.GetComponent<NetworkObject>();

        if(bulletNetworkObject != null) {
            bulletNetworkObject.SpawnWithOwnership(clientId);
        } else {
            Debug.LogWarning("Bullet is Missing NetworkObject component");
        }
    }
}
