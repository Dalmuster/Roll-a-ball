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
