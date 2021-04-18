using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// Since the intro cutscene is automatically played, end it here and switch to the player camera
public class EndIntro : MonoBehaviour {

    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject cutsceneCam;
    [SerializeField] public PlayableDirector timeline;

    void Start() {
        timeline = GetComponent<PlayableDirector>();
        StartCoroutine(EndCutscene());
    }

    IEnumerator EndCutscene() {
        yield return new WaitForSeconds(15);
        timeline.Stop();
        Player.SetActive(true);
        cutsceneCam.SetActive(false);
    }
    
}