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
        Vector3 direction = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        //Vector2 shooting = new Vector2(speed, rb.linearVelocity.y);
        //rb.linearVelocity = shooting;
        Vector3 movement = direction * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnCollisionEnter(Collision other) {
        if(IsServer) {
            GetComponent<NetworkObject>().Despawn();
        }    
    }
}
