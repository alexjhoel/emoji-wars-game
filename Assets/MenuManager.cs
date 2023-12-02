using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using UnityEditor.SearchService;

public class MenuManager : MonoBehaviour
{
    public AudioSource sonidoClick;
    public AudioClip audioClip;

    public RawImage img;
    public float x;
    public float y;

    public void jugar(string escena)
    {
        StartCoroutine(esperarSonido(escena)); 
    }

    IEnumerator esperarSonido(string escena)
    {
        sonidoClick.clip = audioClip;
        sonidoClick.Play();

        while (sonidoClick.isPlaying) //Espera a que termine el sonido
        {
            yield return null;
        }
        
        SceneManager.LoadScene(escena);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y)*Time.deltaTime, img.uvRect.size);
    }
}
