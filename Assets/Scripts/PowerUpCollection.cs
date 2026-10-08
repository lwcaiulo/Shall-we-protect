using UnityEngine;
using System.Collections;
public class PowerUpCollection : MonoBehaviour
{
    //Is placed on power ups on last level

    InvincibilityScript invincibleScript;

    AudioSource soundEffectSource;
    AudioList audioScript;
    private void Start()
    {
        //Retrieve audio script and source
        GameObject soundHolder = GameObject.FindWithTag("Sound Effects");
        audioScript = soundHolder.GetComponent<AudioList>();
        soundEffectSource = soundHolder.GetComponent<AudioSource>();
    }

    //When triggering with minion it deletes itself, plays a sound, and gives minion invincibility
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Minion"))
        {
            invincibleScript = other.GetComponent<InvincibilityScript>();
            invincibleScript.StartInvincibility();
            soundEffectSource.PlayOneShot(audioScript.pickUpSound, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
