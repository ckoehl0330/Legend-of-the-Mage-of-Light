using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransfer : MonoBehaviour
{
   
   
    public Vector3 playerChange;
    
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            
            other.transform.position += playerChange;
            if (needText = true)
            {
                StartCoroutine(PlaceNameCo());
            }
        }
    }
    private IEnumerator PlaceNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
