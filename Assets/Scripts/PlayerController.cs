using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour {

  public float speed;
  public float jumpForce;
  float mx;

  private ClientNetworkTransform _transform;
  private Rigidbody2D rb;

  public Transform feet;
  public LayerMask groundLayer;
  

  private void Start() {
    _transform = GetComponent<ClientNetworkTransform>();
    rb = GetComponent<Rigidbody2D>();
  }


  private void Update() {
    if (!IsOwner) return;

    mx = Input.GetAxisRaw("Horizontal");

    if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && IsGrounded())
    {
      Jump();
    }
  }

  private bool IsGrounded()
  {
    Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.1f, groundLayer);

    if(groundCheck != null) {
      return true;
    }

    return false;
  }

  private void FixedUpdate()
  {
    Vector2 movement = new Vector2(mx * speed, rb.linearVelocity.y);

    rb.linearVelocity = movement;
  }

  private void Jump()
  {
    Vector2 jumping = new Vector2(rb.linearVelocity.x, jumpForce);

    rb.linearVelocity = jumping;
  }

}

