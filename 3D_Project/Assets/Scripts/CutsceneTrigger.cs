using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// Trigger the camera for the cutscene
public class CutsceneTrigger : MonoBehaviour {

    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject cutsceneCam;
    [SerializeField] public PlayableDirector timeline;
    [SerializeField] public CharacterController controller;
    [SerializeField] private GameObject TextBox2;

    void Start() {
        timeline = GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            cutsceneCam.SetActive(true);
            controller.enabled = false;
            StartCoroutine(EndCutscene());
        }
    }

    IEnumerator EndCutscene() {
        yield return new WaitForSeconds(10);
        controller.enabled = true;
        cutsceneCam.SetActive(false);
        TextBox2.SetActive(true);
    }
    
}
