using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Smash()
    {
        StartCoroutine(SmashCo());
    }
    private IEnumerator SmashCo()
    {
        anim.SetBool("IsDestroyed", true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
