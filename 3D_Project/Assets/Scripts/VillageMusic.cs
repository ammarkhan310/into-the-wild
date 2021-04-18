using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// If the enemies are still alive, just play the music normally, otherwise
// if all the enemies are dead, play the credits
public class VillageMusic : MonoBehaviour {
    [SerializeField] GameObject Victory;
    [SerializeField] public PlayableDirector timeline;
    [SerializeField] public GameObject cutsceneCam;
    [SerializeField] Transition transition;
    void Start() {
        timeline = GetComponent<PlayableDirector>();
    }
    void OnTriggerEnter(Collider other) {
        if (!Victory.GetComponent<Victory>().enemiesDefeated) {
            if (other.gameObject.tag == "Player") {
                if (FindObjectOfType<AudioManager>().isPlaying("VillageTheme") && !FindObjectOfType<AudioManager>().isPlaying("LevelTheme")) {
                    FindObjectOfType<AudioManager>().Stop("VillageTheme");
                    FindObjectOfType<AudioManager>().Play("LevelTheme");
                } else if (!FindObjectOfType<AudioManager>().isPlaying("VillageTheme") && FindObjectOfType<AudioManager>().isPlaying("LevelTheme")) {
                    FindObjectOfType<AudioManager>().Stop("LevelTheme");
                    FindObjectOfType<AudioManager>().Play("VillageTheme");
                }
                
            }
        } else if (Victory.GetComponent<Victory>().enemiesDefeated) {
            if (other.gameObject.tag == "Player") {
                 FindObjectOfType<AudioManager>().Stop("LevelTheme");
                 FindObjectOfType<AudioManager>().Stop("VillageTheme");
                 FindObjectOfType<AudioManager>().Play("Credits");
                 cutsceneCam.SetActive(true);
                 timeline.Play();
            }
        }
    }

    IEnumerator Winner() {
        yield return new WaitForSeconds(20);
        transition.BackToMenu();
    }
}
