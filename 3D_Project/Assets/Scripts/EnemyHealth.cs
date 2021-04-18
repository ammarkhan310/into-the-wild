using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// If the enemy comes into contact with the players sword, it loses health
public class EnemyHealth : MonoBehaviour {

    [SerializeField] public int enemyHealth = 100;
    [SerializeField] public int totalHealth = 100;
    [SerializeField] Animator animator;
    [SerializeField] public bool dead = false;

    void Update() {
        if (enemyHealth <= 0) {
            animator.SetTrigger("Dying");
            dead = true;
        }
    }

    void OnTriggerEnter( Collider other) {
        if(other.gameObject.tag == "Sword") {
            FindObjectOfType<AudioManager>().Play("EnemyHurt");
            enemyHealth -= 40;
        }
    }
}
