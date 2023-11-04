using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [HideInInspector]
    public ProjectileStats projectileStats;

    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0,projectileStats.speed * 0.01f, 0);
        if (transform.position.y > 6) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(projectileStats.damage);
        }
        
    }
}

[System.Serializable]
public class ProjectileStats
{
    public float damage;
    public float speed;
    public float range;
    public float areaOfEffect;
}
