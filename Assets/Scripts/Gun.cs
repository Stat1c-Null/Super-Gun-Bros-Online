using UnityEngine;

public class Gun : MonoBehaviour
{

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
            bulletRotation = -1f;
        } else { 
            rotationX = 0.3f;
            bulletRotation = 1f;
        }

        if(Input.GetMouseButtonDown(0)) {
            Instantiate(Bullet, new Vector3(transform.position.x + rotationX, transform.position.y + 0.15f, transform.position.z), transform.rotation);
        }   
    }
}
