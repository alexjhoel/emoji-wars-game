using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [HideInInspector]
    public ProjectileStats projectileStats;

    [HideInInspector]
    public GameObject target;

    void Start()
    {
        transform.parent = null;
        if(projectileStats.type == ProjectileType.Melee) Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Diferentes tipos de movimientos segun projectiletype
        //TO DO: Destruir gameObject cuando este fuera de rango
        //TO DO: Crear mas comportamientos
        if (projectileStats.type == ProjectileType.Straight) transform.position = transform.position + new Vector3(0, projectileStats.speed * 0.01f, 0);
        if (transform.position.y > 6) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Colisionara dependiendo del layer establecido
        collision.gameObject.GetComponent<IDamageable>().TakeDamage(projectileStats.damage);
        Destroy(gameObject);
    }
}

[System.Serializable]
public class ProjectileStats
{
    
    //Estadisticas de proyectiles
    public float damage;
    public float speed;
    public float range;
    public float areaOfEffect;
    public ProjectileType type;
}

[System.Serializable]
public enum ProjectileType
{

    //Este tipo define los tipos de proyectiles
    Straight,
    Melee,
    Guided
}