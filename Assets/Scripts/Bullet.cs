using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using System.Collections;

public class Bullet : NetworkBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    Rigidbody2D rb;
    [SerializeField] private float knockbackForce;
    Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(IsServer) {
            StartCoroutine(DespawnBullet());
        }

        if(transform.rotation.y > 0) {
            speed = -speed;
        }
    }

    private IEnumerator DespawnBullet() {
        yield return new WaitForSeconds(lifeTime);

        //Despawn bullet after some time
        if(IsServer) {
            GetComponent<NetworkObject>().Despawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 shooting = new Vector2(speed, rb.linearVelocity.y);
        rb.linearVelocity = shooting;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(IsServer && other.gameObject.CompareTag("Player")) {
            ApplyKnockback(other.gameObject, other.contacts[0].point);
            
        }    
    }

    private void ApplyKnockback(GameObject player, Vector2 collisionPoint) {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if(rb != null) {
            
            Vector2 knockbackDirection = (rb.position - collisionPoint).normalized;
            if(knockbackDirection.x > 0) {
                direction = new Vector2(-1f, 0.5f);
            } else {
                direction = new Vector2(1f, 0.5f);
            }
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            KnockbackClientRpc(player.GetComponent<NetworkObject>().NetworkObjectId, direction);
            //GetComponent<NetworkObject>().Despawn();
        }
    }

    [ClientRpc]
    private void KnockbackClientRpc(ulong playerId, Vector2 knockDirection){
        //Check if player exists in the game
        if(NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerId, out var networkObject)) {
            GameObject player = networkObject.gameObject;
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if(playerRb != null) {
                rb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
        
    }
}
