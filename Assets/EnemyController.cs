using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    GameObject ghost;



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
        transform.position = transform.position + new Vector3(0,-curve.Evaluate(timer % 2 / 2f) * 0.003f,0);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            GameObject newGhost = Instantiate(ghost, null);
            newGhost.transform.position = transform.position;
            Destroy(newGhost,2);
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != 8) return;
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Attacking", true);
        attacking = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 8) return;
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Attacking", false);
        attacking = false;
    }
}
