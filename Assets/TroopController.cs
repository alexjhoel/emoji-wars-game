using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour
{
    [SerializeField]
     GameObject projectilePrefab;
    [SerializeField]
    GameObject ghost;

    [SerializeField]
    private float health;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private bool needsEnemy;
    [SerializeField]
    private string shootAnimationName;

    [SerializeField]
    private ProjectileStats stats;

    private float currentHealth;
    private float timer;
    private float fireTime;
    
    void Start()
    {
        projectilePrefab.GetComponent<ProjectileController>().projectileStats = stats;
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int layerMask = 1 << 6;
        RaycastHit2D ray;
        if (!needsEnemy || timer > fireTime && (ray = Physics2D.Raycast(transform.position, Vector2.up, stats.range, layerMask)))
        {
            GetComponent<Animator>().SetTrigger(shootAnimationName);
            fireTime = timer + attackSpeed;
        }
    }

    public void CreateProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform, true);
        projectile.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
