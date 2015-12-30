using UnityEngine;
using System.Collections;

public class mineExplode : MonoBehaviour {
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("explode", false);
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            animator.SetBool("explode", true);
            StartCoroutine(Explode());
        }
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("explode", false);
    }
}
