// Referenced from https://forum.unity.com/threads/one-two-attack-combo.417184/
using UnityEngine;
using System.Collections;
 
public class ComboHits : MonoBehaviour {
    //Cooldown time between attacks (in seconds)
    public float cooldown = 0.5f;
    //Max time before combo ends (in seconds)
    public float maxTime = 0.8f;
    //Max number of attacks in combo
    public int maxCombo = 3;
    //Current combo
    int combo = 0;
    //Time of last attack
    float lastTime;
    [SerializeField] Animator animator;
 
    // Use this for initialization
    void Start () {
        //Starts the looping coroutine
        StartCoroutine("Melee");
    }
   
    IEnumerator Melee ()
    {
        //Constantly loops so you only have to call it once
        while (true)
        {
            //Checks if attacking and then starts of the combo
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                combo++;
                string attackNo = "Attack" + combo;
                //Debug.Log(attackNo);
                animator.SetTrigger(attackNo);

                lastTime = Time.time;
 
                //Combo loop that ends the combo if you reach the maxTime between attacks, or reach the end of the combo
                while ((Time.time - lastTime) < maxTime && combo < maxCombo)
                {
                    //Attacks if your cooldown has reset
                    if (Input.GetKeyDown(KeyCode.Mouse0) && (Time.time - lastTime) > cooldown)
                    {
                        combo++;
                        //Debug.Log("Attack " + combo);
                        animator.SetTrigger(attackNo);
                        lastTime = Time.time;
                    }
                    yield return null;
                }
                //Resets combo and waits the remaining amount of cooldown time before you can attack again to restart the combo
                combo = 0;
                yield return new WaitForSeconds(cooldown - (Time.time - lastTime));
            }
            yield return null;
        }
    }
}

