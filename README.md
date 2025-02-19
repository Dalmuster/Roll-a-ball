# Roll-a-ball

#Creacion del tablero

- Plataforma inicial

![imagen](https://github.com/user-attachments/assets/b172b7f6-20b5-4e8b-9017-fbefd542d0f5)

- Objetos

![imagen](https://github.com/user-attachments/assets/4cbd5757-2e3d-4f5e-8b7c-f37a3373beb9)

- Segunda Plataforma

![imagen](https://github.com/user-attachments/assets/322b22d4-9315-4264-97cc-e80af6d3a17a)

-Objetos   

![imagen](https://github.com/user-attachments/assets/af88ae10-2afe-45a0-a695-d6e9945806d2)


-Rampa

![imagen](https://github.com/user-attachments/assets/8189e561-fd4e-4b78-bea7-f15243aa3dcc)

-Objetos

![imagen](https://github.com/user-attachments/assets/a1eecb01-8ade-4a12-88b1-f67b71ac6df0)

-Tercer Nivel (Laberinto invisible)

![imagen](https://github.com/user-attachments/assets/6c4da8f4-2fe2-458c-85d2-49849f318543)

-Objetos

![imagen](https://github.com/user-attachments/assets/96d94293-b1d8-405c-a79a-3344b820d64f)

-Explicacion Primera Persona

Primero añadi las sensibilidades del raton

![imagen](https://github.com/user-attachments/assets/e30e90f6-cdea-4ad9-80ed-ca545412764e)

Y luego hice que la camara calculara en cada frame la posicion x e y del raton gracias a estas lineas:

-Primero calcula el movimiento del raton le suma las sensibilidad que es modificable y se asegura de que a pesar de que el juego este mal optimizado siga funcionando correctamente.

        _currentAngle.x -= mouseY * _mouseSensitivity * Time.deltaTime;
        _currentAngle.y += mouseX * _mouseSensitivity * Time.deltaTime;

Finalmente utilice:

-Esta linea lo que hace es utilizar los angulos previamente explicados y aplicarlos.
        
         transform.localRotation = Quaternion.Euler(_currentAngle.x, _currentAngle.y, 0);

-Utilice las teclas '1', '2', '3' para cambiar las camaras
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

-Mecanicas añadidas

-Sistema de vida que va menguando en función de las veces que pise la lava, en este caso hay dos niveles de lava que dependiendo de la velocidad o si pisa la lava dos veces seguidas el jugador perdera vida en caso de ser 0 el jugador perdera automaticamente.

        void OnTriggerEnter(Collider other){
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
    
![imagen](https://github.com/user-attachments/assets/bf2d7349-b62f-4200-899d-8a20779cb1bb)

- Condicion para ganar, uso de los pickups: En este caso se puede ver que cada vez que se obtiene un pickup este desaparecera de la vista del jugador y añadira un punto a la puntuación y en caso de que sea la ultima moneda (en este caso 27) mostrara un mensaje al jugador mostrando que a ganado y destruyendo al enemigo.

        void OnTriggerEnter(Collider other){
            if(other.gameObject.CompareTag("PickUp")){
                other.gameObject.SetActive(false);
                puntuacion++;
                mostrarPuntuacion();
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

  ![imagen](https://github.com/user-attachments/assets/7a3868cd-76bc-4973-a8f4-29b781082b28)

- Mecanica de reaparición, al comienzo del juego se obtiene la posicion inicial y en caso de que el jugador se caiga del mapa tocara un plano invisible que lo teletransportara al inicio del nivel.

         void Start()
            {
                Respawn = transform.position;
            }


          void OnTriggerEnter(Collider other){    
            if (other.gameObject.CompareTag("Respawn"))
            {
                transform.position = Respawn;
            }

![imagen](https://github.com/user-attachments/assets/106cc416-4a85-4471-9afd-2e77b595f5fd)

- Rampas propulsoras al tocar las rampas el jugador sera impulsado hacia adelante.

        void OnTriggerEnter(Collider other){
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
          }

- Enemigos en este proyecto e utilizado dos enemigos que se encargaran de perseguir al jugador en dos de los tres niveles creados, el primero sera ligeramente mas lento que el jugador y tendra colisiones, pero el segundo sera muchísimo mas lento que el jugador pero atravesara las paredes del laberinto invisible, en caso de que toquen al jugador este sera eliminado mostrando el mensaje de derrota.

            void Start()
            {
                navMeshAgent = GetComponent<NavMeshAgent>();
            }

            void Update()
            {
                if (player != null && navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.SetDestination(player.position);
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
            }

Como se puede ver el objeto player a desaparecido al ser tocado por el enemigo.
  
![imagen](https://github.com/user-attachments/assets/cc0ea6eb-f7ce-41ef-8962-42f566f9d177)
