using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour {
  public float speed;

  private ClientNetworkTransform _transform;
  private Rigidbody2D rb;
  float mx;

  private void Start() {
    _transform = GetComponent<ClientNetworkTransform>();
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update() {
    mx = Input.GetAxisRaw("Horizontal");
    if(!IsOwner) return;
    //var movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    //_transform.transform.position += movement * speed * Time.deltaTime;
  }

  private void FixedUpdate()
    {
        Vector2 movement = new Vector2(mx * speed, rb.linearVelocity.y);

        rb.linearVelocity = movement;
    }
}

