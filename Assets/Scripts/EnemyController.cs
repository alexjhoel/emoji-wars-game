using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    AnimationCurve movementCurve;

    [SerializeField]
    GameObject ghost;

    [SerializeField]
    private float health;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private ProjectileStats stats;
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private string attackAnimationName;



    float currentHealth;
    float timer;
    float fireTime;
    

    void Start()
    {
        projectilePrefab.GetComponent<ProjectileController>().projectileStats = stats;
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Este operador << se conoce como bitshift, y es para crear mascaras de colisiones, esto es que definimos cuales layers
        //queremos detectar como validos para colisionar y disparar proyectil. En este caso decimos que queremos colisionar solo
        //con el layer 7, que corresponde a tropas alidas.
        int layerMask = 1 << 7;
        RaycastHit2D ray;
        timer += Time.deltaTime;
        if (ray = Physics2D.Raycast(transform.position, Vector2.down, stats.range, layerMask))
        {
            //Raycast manda un rayo de largo dependiendo del rango establecido en ProjectileStats, a la primera tropa que colisione
            //restablece el timer de ataque, y manda el trigger para la animación de ataque
            if (timer > fireTime)
            {
                GetComponent<Animator>().SetTrigger(attackAnimationName);
                fireTime = timer + attackSpeed;
            }

            return;

        }
        transform.position = transform.position + new Vector3(0, -movementCurve.Evaluate(timer % 2 / 2f) * movementSpeed / 1000, 0);
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
        //Debug.Log("Enemigo: " + currentHealth);
        //
        if (currentHealth <= 0)
        {
            //Efecto visual del fantasma
            GameObject newGhost = Instantiate(ghost, null);
            newGhost.transform.position = transform.position;
            Destroy(newGhost,2);
            Destroy(gameObject);

        }
    }
}

internal interface IDamageable
{
    //Interfaz para recibir daño
    public void TakeDamage(float damage);
}


