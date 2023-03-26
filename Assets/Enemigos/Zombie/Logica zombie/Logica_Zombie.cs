using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logica_Zombie : MonoBehaviour
{
    public int rutina;
    public float cronometro=0;
    public Animator anim;
    public Quaternion angulo;
    public float grado;

    public bool detectorjugador;
    public float rangoAlerta=15;
    public LayerMask capadeljugador;
    public Transform jugador;
    public bool corriendo = false;
    public bool atacando = false;
    public float velocidad_persecucion = 7.0f;
    public float velocidad_caminar = 0.4f;

    private float distance = 0.0f;

    private bool acorrer = false;
    public int vida=100;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        detectorjugador = Physics.CheckSphere(transform.position, rangoAlerta, capadeljugador);
    }

    // Update is called once per frame
    void Update()
    {
        detectorjugador = Physics.CheckSphere(transform.position,rangoAlerta,capadeljugador);
        if (detectorjugador)
        {
            Vector3 posJugador = new Vector3(jugador.position.x, transform.position.y, jugador.position.z);
            transform.LookAt(posJugador);
            if (!corriendo)
            {
                anim.SetTrigger("premodohostil");
                corriendo = true;
            }
            else if(acorrer)
            {
                
                distance = Vector3.Distance(transform.position, jugador.position);
                transform.position = Vector3.MoveTowards(transform.position, posJugador, velocidad_persecucion * Time.deltaTime);

                if (distance>1.1f && !atacando)
                {
                    anim.SetBool("modohostil", true);                    
                    //transform.LookAt(posJugador);
                    
                    atacando = false;
                }
                else
                {
                    
                    anim.SetBool("atacando",true);
                    atacando=true;
                    if (distance>1.1  )
                    {
                        atacando = false;
                        anim.SetBool("atacando",false);
                    }
                }

            }
            else
            {
                acorrer = false;
            }
        }
        else
        {
            anim.SetBool("modohostil", false);
            corriendo = false;
            atacando = false;
            Comportamiento();
        }


    }
    public void ActualizarAtacando()
    {
        atacando = false;
        anim.SetBool("atacando", false);
    }
    
    public void Comportamiento()
    {
        cronometro += 1 * Time.deltaTime;
        if (cronometro>=10)
        {
            rutina = Random.Range(0, 3);
            cronometro = 0;
            anim.SetBool("caminar",false);
        }
        switch (rutina)
        {
            case 0:
                grado = Random.Range(0, 270);
                angulo = Quaternion.Euler(0, grado, 0);
                rutina++;
                break;
            case 1:
                anim.SetBool("caminar", true);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * velocidad_caminar * Time.deltaTime);
                
                break;
            case 2:
                anim.SetBool("caminar",false);
                if (Random.Range(0,3)==2)
                {
                    anim.SetTrigger("mirar");
                }
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    }
    public void Acorrer()
    {
        acorrer = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("punioHumano"))
        {
            vida -= 10;
            if (vida<=0)
            {
                anim.SetBool("muriendo",true);

            }
        }
    }
    public void DesactivarEnemigo()
    {
        gameObject.SetActive(false);
    }
}
