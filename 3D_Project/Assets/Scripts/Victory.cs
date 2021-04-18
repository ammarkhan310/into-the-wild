using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check if all the enemies are dead
public class Victory : MonoBehaviour {

    [SerializeField] GameObject Goblin1;
    [SerializeField] GameObject Goblin2;
    [SerializeField] GameObject Goblin3;
    [SerializeField] GameObject TextBox4;
    private bool onlyOnce = true;
    [SerializeField] public bool enemiesDefeated = false;

    void Update() {
        if (Goblin1.GetComponent<EnemyHealth>().enemyHealth <= 0 
         && Goblin2.GetComponent<EnemyHealth>().enemyHealth <= 0
         && Goblin3.GetComponent<EnemyHealth>().enemyHealth <= 0) {

             if (onlyOnce) {
                enemiesDefeated = true;
                TextBox4.SetActive(true);
                FindObjectOfType<AudioManager>().Play("Victory");
                FindObjectOfType<AudioManager>().Stop("BattleTheme");
                FindObjectOfType<AudioManager>().Play("LevelTheme");
                onlyOnce = false;
             }
         }
        
    }
}
