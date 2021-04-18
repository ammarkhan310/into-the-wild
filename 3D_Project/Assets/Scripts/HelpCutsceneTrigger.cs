using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// Trigger the timeline for the cutscene
public class HelpCutsceneTrigger : MonoBehaviour {

    [SerializeField] public GameObject Player;
    [SerializeField] public PlayableDirector timeline;
    [SerializeField] public CharacterController controller;
    [SerializeField] public GameObject cutsceneCam1;
    [SerializeField] public GameObject cutsceneCam2;
    [SerializeField] public GameObject cutsceneCam3;
    [SerializeField] private GameObject TextBox3;

    void Start() {
        timeline = GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            cutsceneCam1.SetActive(true);
            cutsceneCam2.SetActive(true);
            cutsceneCam3.SetActive(true);
            TextBox3.SetActive(true);
            timeline.Play();
            controller.enabled = false;
            StartCoroutine(EndCutscene());
        }
    }

    IEnumerator EndCutscene() {
        yield return new WaitForSeconds(10);
        controller.enabled = true;
        timeline.Stop();
        cutsceneCam1.SetActive(false);
        cutsceneCam2.SetActive(false);
        cutsceneCam3.SetActive(false);
    }
    
}
