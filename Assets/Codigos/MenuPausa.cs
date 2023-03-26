using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    private void Start()
    {
        Reaunudar();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausa();
        }
    }
    public void Pausa()
    {
        Time.timeScale = 0;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        Cursor.visible = true;
    }
    public void Reaunudar()
    {
        Time.timeScale = 1;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        Cursor.visible = false;
    }
}
