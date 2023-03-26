using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void botonIniciar()
    {
        SceneManager.LoadScene(1);
    }
    public void botonSalir()
    {
        print("Saliendo....");
        Application.Quit();
    }
}
