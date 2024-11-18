using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using System.Collections;

public class Bullet : NetworkBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    Rigidbody2D rb;
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
        if(IsServer) {
            GetComponent<NetworkObject>().Despawn();
        }    
    }
}
