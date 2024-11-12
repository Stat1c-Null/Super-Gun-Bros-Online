using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 shooting = new Vector2(speed, rb.linearVelocity.y);
        rb.linearVelocity = shooting;
    }
}
