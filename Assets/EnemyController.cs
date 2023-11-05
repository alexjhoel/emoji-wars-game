using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AnimationCurve movementCurve;

    [SerializeField]
    GameObject ghost;

    [SerializeField]
    private float movementSpeed;



    float currentHealth = 21;
    float timer;
    bool attacking = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (attacking) return;
        timer += Time.deltaTime;
        transform.position = transform.position + new Vector3(0,-movementCurve.Evaluate(timer % 2 / 2f) * movementSpeed / 1000,0);
    }

    public void TakeDamage(float damage)
    {
        //Recibir daño por parte de proyectiles
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            //Efecto visual del fantasma
            GameObject newGhost = Instantiate(ghost, null);
            newGhost.transform.position = transform.position;
            Destroy(newGhost,2);
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entro");
        //Al entrar en colisión con tropas aliadas
        if (collision.gameObject.layer != 7) return;
        
        Animator animator = GetComponent<Animator>();
        //Attacking cambia la animación a atacar
        animator.SetBool("Attacking", true);
        attacking = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Salio");
        //Al entrar de colisión con tropas aliadas
        if (collision.gameObject.layer != 7) return;
        
        Animator animator = GetComponent<Animator>();
        
        animator.SetBool("Attacking", false);
        attacking = false;
    }
}

//
