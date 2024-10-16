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
    private BoxCollider2D box2d;

    // Start is called before the first frame update
    void Start()
    {
        box2d = transform.GetChild(0).GetComponent<BoxCollider2D>();
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
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
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

        isLive = true;
        return true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLive && collision.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(attack);

            // Remove projectile if it is single-target
            if (!isPiercing)
            {
                Destroy(gameObject);
            }
        }
    }
}
