using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Only used once to instantiate the intro text
public class TextBoxInstatiator : MonoBehaviour {

    [SerializeField] private GameObject TextBox1;
    private int[] onlyOnce = {1, 1, 1};

    void Update() {
        if (onlyOnce[0] == 1) {
            TextBox1.SetActive(true);
            onlyOnce[0] = 0;
        }
    }
}
