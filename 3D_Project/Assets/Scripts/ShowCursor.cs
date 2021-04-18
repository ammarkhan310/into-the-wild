using UnityEngine;
using System.Collections;

// Show cursor again for the game over screen
// Oddly enough you still need to press escape to move it around
// so this doesn't help that much
public class ShowCursor : MonoBehaviour
{
    void Start() {
        Cursor.visible = true;
    }
}