using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour, IDamageable
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
        //Este operador << se conoce como bitshift, y es para crear mascaras de colisiones, esto es que definimos cuales layers
        //queremos detectar como validos para colisionar y disparar proyectil. En este caso decimos que queremos colisionar solo
        //con el layer 6, que corresponde a tropas enemigas.
        int layerMask = 1 << 6;
        RaycastHit2D ray;
        timer += Time.deltaTime;
        if (!needsEnemy || timer > fireTime && (ray = Physics2D.Raycast(transform.position, Vector2.up, stats.range, layerMask)))
        {
            //Raycast manda un rayo de largo dependiendo del rango establecido en ProjectileStats, a la primera tropa que colisione
            //restablece el timer de ataque, y manda el trigger para la animación de ataque
            GetComponent<Animator>().SetTrigger(shootAnimationName);
            fireTime = timer + attackSpeed;
        }
    }

    public void CreateProjectile()
    {
        //Event de creación de proyectil mandado por la animación
        GameObject projectile = Instantiate(projectilePrefab, transform, true);
        projectile.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        //Recibir daño por parte de proyectiles
        currentHealth -= damage;
        Debug.Log("Tropa: " + currentHealth);
        if (currentHealth <= 0)
        {
            GameObject newGhost = Instantiate(ghost, null);
            newGhost.transform.position = transform.position;
            Destroy(newGhost, 2);
            Destroy(gameObject);
        }
    }
}
