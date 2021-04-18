using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plays the battle music when entering the battle area and stops it when leaving
public class BattleMusic : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            FindObjectOfType<AudioManager>().Play("BattleTheme");
            FindObjectOfType<AudioManager>().Stop("LevelTheme");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            FindObjectOfType<AudioManager>().Stop("BattleTheme");
            FindObjectOfType<AudioManager>().Play("LevelTheme");
        }
    }
    
}
