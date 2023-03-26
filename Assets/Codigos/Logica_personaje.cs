using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logica_personaje : MonoBehaviour
{
    //movimiento
    public float velocidad_movimiento = 3.0f;
    public float vel_mouse_x;
    private float contadorCorrer;
    private bool puedoCorrer = true;
    private bool corriendo = false;
    public float contadorCorrerMax = 5;
    public Image barraCorrer; 

    
    private Animator animacion;
    private float x, y,mx,my;
    private Rigidbody rb;

    //salto
    public float fuerza_salto = 4.0f;
    public bool puedosaltar ;

    //GOLPE PUÑOS
    private bool estoyAtacando;
    private bool avanzosolo;
    //vida
    private float vida = 100;
    public float vidaActual;
    public Image barradevida;


    // Start is called before the first frame update
    void Start()
    {
        puedosaltar = false;
        animacion = GetComponent<Animator>();  
        rb = GetComponent<Rigidbody>();
        vidaActual = vida;
        contadorCorrer = contadorCorrerMax;
        vel_mouse_x = 5000.0f;
    }

    private void FixedUpdate()
    {
        if (!estoyAtacando)
        {
            transform.Translate(x * Time.deltaTime * velocidad_movimiento, 0, y * Time.deltaTime * velocidad_movimiento);
        }
        /*if (avanzosolo)
        {
            rb.velocity = transform.forward * inpulsoGolpe;
        }*/
    }
    

    void Update()
    {
        //captura de teclas de movimiento
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0) && puedosaltar)
        {
            animacion.SetTrigger("Golpe_pu");
            estoyAtacando = true;
        }

        animacion.SetFloat("VelocidadX", x);
        animacion.SetFloat("VelocidadY", y);
        Movimientos();

        //captura de mouse
        mx = Input.GetAxis("Mouse X");
        transform.Rotate(0,mx*Time.deltaTime*vel_mouse_x,0);
        if(contadorCorrer<10 & !corriendo)
        {
            contadorCorrer += 0.5f*Time.deltaTime;
            barraCorrer.fillAmount = contadorCorrer / contadorCorrerMax;
            if (contadorCorrer>=8)
            {
                puedoCorrer = true;
            }
        }

    }

    private void Movimientos()
    {
        //saltar
        if (puedosaltar)
        {
            //FinSalto();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animacion.SetBool("Saltando", true);
                rb.AddForce(new Vector3(0, fuerza_salto, 0), ForceMode.Impulse);
            }
            else if (Input.GetKey(KeyCode.LeftShift) & y > 0 &  puedoCorrer & puedosaltar) //Correr
            {
                animacion.SetBool("PuedoC", true);
                velocidad_movimiento = 8;
                contadorCorrer -= Time.deltaTime;
                corriendo = true;
                barraCorrer.fillAmount = contadorCorrer / contadorCorrerMax;
                if (contadorCorrer <= 0)
                {
                    puedoCorrer = false;
                    corriendo = false;
                }

            }
            else
            {
                animacion.SetBool("PuedoC", false);
                velocidad_movimiento = 3;
                corriendo = false;
            }
            animacion.SetBool("TocoSuelo", true);
        }
        else
        {
            Cayendo();
        }
        
    }

    private void Cayendo()
    {
        animacion.SetBool("TocoSuelo",false);
        animacion.SetBool("Saltando", false);
    }
    public void DejarDeGolpear()
    {
        estoyAtacando=false;
    }
    public void AvansoSolo()
    {
        avanzosolo = true;
    }
    public void DejoDeAvanzar()
    {
        avanzosolo=false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("punioZombi"))
        {
            vidaActual -= 30;
            barradevida.fillAmount = vidaActual / vida;
            print("Daño zombie");
            print(vidaActual / vida);
        }
        
    }
    public void InicioSalto()
    {
        velocidad_movimiento=3;
    }
}
