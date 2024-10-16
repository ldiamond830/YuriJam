using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Fields
    public float speed;
    private Attack attack;
    private float lifetime = -1;
    private bool isPiercing = false;
    private bool isLive = false;
    private Collider2D box2d;
    private Rigidbody2D rigid2d;

    // Start is called before the first frame update
    void Start()
    {
        box2d = GetComponent<Collider2D>();
        rigid2d = GetComponent<Rigidbody2D>();
        if (isLive) rigid2d.velocity = Vector3.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
            lifetime -= Time.deltaTime;
        }
    }

    // Sets the projectile parameters for shooting
    public void Initialize(int power, float range, bool canHitMultiple = false, List<StatusEffect> effects = null)
    {
        attack = new Attack(power, effects);
        lifetime = range / speed;
        isPiercing = canHitMultiple;
    }

    // Activates the projectile, setting it in motion and enabling it to deal damage
    // Returns false if projectile cannot be fired or has already been fired
    public bool Fire()
    {
        // Ensure projectile is initialized and has not been fired
        if (isLive)
        {
            Debug.Log("Cannot fire: Projectile already in motion!");
            return false;
        }
        if (lifetime <= 0)
        {
            Debug.Log("Cannot fire: Projectile has not been initialized!");
            return false;
        }

        if (rigid2d) rigid2d.velocity = Vector3.right * speed;
        isLive = true;
        return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLive && collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().TakeDamage(attack);

            // Remove projectile if it is single-target
            // Should object pool at some point
            if (!isPiercing)
            {
                Destroy(gameObject);
            }
        }
    }
}
