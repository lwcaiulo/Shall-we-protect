using UnityEngine;
using System.Collections;
public class PowerUpCollection : MonoBehaviour
{
    InvincibilityScript invincibleScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Minion"))
        {
            invincibleScript = other.GetComponent<InvincibilityScript>();
            invincibleScript.StartInvincibility();
            Destroy(this.gameObject);
        }
    }
}
