using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollaiderObjetos : MonoBehaviour
{
    public Inventario inventario;
    private float rotar = 0;
    void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
    }
    void Update()
    {
        if(rotar >= 360)
        {
            rotar = 0;
        }
        transform.Rotate(new Vector3(0,0, rotar * Time.deltaTime));
        rotar += 10;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            inventario.estrella += 1;
            Destroy(gameObject);
        }
    }
}
