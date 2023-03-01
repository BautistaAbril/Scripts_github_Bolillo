using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int fresas = 0;
    [SerializeField] private Text fresasText;
    [SerializeField] private AudioSource collectionSoundEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.gameObject.CompareTag("Fresa"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            fresas++;
            fresasText.text = "Fresas:" + fresas;



        }
    }


}
