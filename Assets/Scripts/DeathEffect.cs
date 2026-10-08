using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class DeathEffect : MonoBehaviour
{
    
    //Runs on death particle effects.
    //Just so it gets destroyed after 2 seconds of the animation running
    void Start()
    {
        StartCoroutine(DeletingEffect());
    }

    IEnumerator DeletingEffect()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

}
