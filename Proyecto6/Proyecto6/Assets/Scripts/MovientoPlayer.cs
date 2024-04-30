using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovientoPlayer : MonoBehaviour
{
    private Vector2 dirMov;
    public float VelMov, VelSalto;
    public Rigidbody2D rb;
    public Animator anim;

    private float movX = 0;
    public static int dirIdle = 1; 

    private int numSaltos;

    private void Start()
    {
        anim.SetBool("saltando", false);
        anim.SetBool("saltandoIzq", false);
        numSaltos = 0;
    }

    private void FixedUpdate()
    {
        Movimiento();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")){
            if (numSaltos <= 1) {
                numSaltos++;
                ColisionSuelo.estaEnSuelo = false;
                rb.velocity = new Vector2(rb.velocity.x, VelSalto);
                if(dirIdle == 1)
                {
                    anim.SetBool("saltando", true);
                }else if (dirIdle == -1){
                    anim.SetBool("saltandoIzq", true);
                }
            }
        }else
        {
            anim.SetBool("saltando", false);
            anim.SetBool("saltandoIzq", false);
            if(ColisionSuelo.estaEnSuelo == true){
                numSaltos = 0;
            }
        }
    }
    private void Movimiento()
    {
        movX = Input.GetAxisRaw("Horizontal");
        dirMov = new Vector2(movX, 0).normalized;
        rb.velocity = new Vector2(dirMov.x * VelMov, rb.velocity.y);

        if(movX != 0)
        {
            anim.SetBool("corriendo", true);
            AnimacionesPlayer(dirMov.x);
            if(movX > 0)
            {
                dirIdle = 1;
            }
            else
            {
                dirIdle = -1;
            }
        }
        else
        {
            anim.SetBool("corriendo", false);
            if(dirIdle == 1)
            {
                AnimacionesPlayer(0);
            }
            else if (dirIdle == -1)
            {
                AnimacionesPlayer(-10);
            }
        }
        anim.SetBool("saltando", false);
    }
    void AnimacionesPlayer(float n )
    {
        anim.SetFloat("movX", n);
    }
}
