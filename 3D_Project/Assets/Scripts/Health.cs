using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    
    [SerializeField] public int health;
    [SerializeField] public int numOfHearts;

    [SerializeField] public Image[] hearts;
    [SerializeField] public Sprite fullHeart;
    [SerializeField] public Sprite emptyHeart;
    [SerializeField] Animator animator;

    // Update the player hearts
    void Update() {

        if (health > numOfHearts) {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++) {

            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    void OnTriggerEnter( Collider other) {
        if(other.gameObject.tag == "Weapon") {
            health--;
            animator.SetTrigger("Hit");
        }
    }
}
