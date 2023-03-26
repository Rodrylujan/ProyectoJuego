using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlataforma : MonoBehaviour
{
    public Rigidbody plataformarb;
    public Transform pos1;
    public Transform pos2;
    public float velicidad;
    private bool movimiento = true;

    void Update()
    {
        if (movimiento)
        {
            plataformarb.MovePosition(Vector3.MoveTowards(plataformarb.position, pos2.position, velicidad * Time.deltaTime));
            if (Vector3.Distance(plataformarb.position, pos2.position) <= 0) movimiento = false;
        }
        else
        {
            plataformarb.MovePosition(Vector3.MoveTowards(plataformarb.position, pos1.position, velicidad * Time.deltaTime));
            if (Vector3.Distance(plataformarb.position, pos1.position) <= 0) movimiento = true;
        }
    }
}
