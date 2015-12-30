using UnityEngine;
using System.Collections;

public class spikesActivate : MonoBehaviour {
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("activate", false);
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            animator.SetBool("activate", true);
            StartCoroutine(Activate());
        }
    }
    IEnumerator Activate()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("activate", false);
    }
}
