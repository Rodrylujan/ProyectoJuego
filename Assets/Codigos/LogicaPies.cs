using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPies : MonoBehaviour
{

    public Logica_personaje logica_personaje;
    
    private void OnTriggerStay(Collider other)
    {
        logica_personaje.puedosaltar = true;
    }
    private void OnTriggerExit(Collider other)
    {
        logica_personaje.puedosaltar=false;
    }
}
