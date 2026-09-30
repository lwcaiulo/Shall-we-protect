using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class DeathEffect : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
