using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerControler : MonoBehaviour
{
    private Animator anim;
    public float speed = 5f;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public Camera PrimeraPersona;  
    public Camera Main_Camera;
    public Camera CamaraRotatoria;

    private Camera currentCamera; 

    public TextMeshProUGUI loseTextObject;
    public TextMeshProUGUI winTextObject;
    public TextMeshProUGUI MostrarPuntuacion;
    public TextMeshProUGUI Vida;

    public int puntuacion;
    public int vida = 5;
    public Vector3 Respawn;
    public Estado estado = Estado.Jugando;

    public enum Estado
{
    Jugando,
    Nivel1,
    Nivel2,
    Nivel3,
    Quemado
}
    
    void Start()
    {

        if (SystemInfo.supportsGyroscope) {
        Input.gyro.enabled = true;
    }

        rb = GetComponent<Rigidbody>();
        
        PrimeraPersona = Camera.main;  

        Respawn = transform.position;

        vida=5;

        Vida.text = "Vida: " + vida.ToString();

        anim = GetComponent<Animator>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
 
        Vector3 cameraForward = PrimeraPersona.transform.forward;
        cameraForward.y = 0; 
        cameraForward.Normalize(); 

        Vector3 cameraRight = PrimeraPersona.transform.right;

        Vector3 movement = (cameraForward * movementY + cameraRight * movementX).normalized;


        float temp = Input.acceleration.x;
    Mover (temp);

        rb.AddForce(movement * speed);


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCamera(Main_Camera);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCamera(PrimeraPersona);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCamera(CamaraRotatoria);
        }
        
    }

public float velocidadPosicion = 2.0f; 

public void Mover(float temp){
    transform.Translate (temp * velocidadPosicion * Time.deltaTime, 0, 0, Space.World);
}

  void SwitchCamera(Camera newCamera)
{
    if (currentCamera != null) 
    {
        currentCamera.gameObject.SetActive(false);
    }

    newCamera.gameObject.SetActive(true);

    currentCamera = newCamera;
}

void OnTriggerEnter(Collider other){
    if(other.gameObject.CompareTag("PickUp")){
        other.gameObject.SetActive(false);
        puntuacion++;
        mostrarPuntuacion();
    }
    if(other.gameObject.CompareTag("Lava")){
        if(vida>0){
        rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        vida--;
        loseTextObject.GetComponent<TextMeshProUGUI>().text = "Vidas: "+vida;
        Vida.text = "Vida: " + vida.ToString();
        }
    }
    if(other.gameObject.CompareTag("LavaV")){
        rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        anim.SetBool("Quemado", true);
        if(vida==0) 
        {
            Destroy(gameObject);

        loseTextObject.gameObject.SetActive(true);
        loseTextObject.GetComponent<TextMeshProUGUI>().text = "Perdiste!";
        }
        anim.SetBool("Quemado", false);
    }
   if (other.gameObject.CompareTag("Rampa"))
{
        Vector3 rampDirection = other.transform.forward;

        rb.AddForce(-rampDirection * 10f, ForceMode.VelocityChange); 
    }
   if (other.gameObject.CompareTag("Rampa2"))
    {
        Vector3 rampDirection = other.transform.right;

        rb.AddForce(-rampDirection * 20f, ForceMode.VelocityChange); 
    }
    if (other.gameObject.CompareTag("Respawn"))
    {
        transform.position = Respawn;
    }
}

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        Destroy(gameObject);

        loseTextObject.gameObject.SetActive(true);
        loseTextObject.GetComponent<TextMeshProUGUI>().text = "Perdiste!";
        
    }
     if (collision.gameObject.CompareTag("Nivel1"))
    {
        anim.SetBool("Nivel 1", true);
        
    }
    if (collision.gameObject.CompareTag("Nivel2"))
    {
        anim.SetBool("Nivel 1", false);
        anim.SetBool("Nivel 2", true);

    }
}

void mostrarPuntuacion ()
{
    MostrarPuntuacion.text = "Puntuacion " + puntuacion.ToString();
    if (puntuacion > 26)
        {
            winTextObject.gameObject.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
}
}
