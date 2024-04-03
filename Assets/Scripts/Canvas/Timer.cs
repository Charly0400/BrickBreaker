using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float initialTimerDuration = 10f; // Duración inicial del temporizador
    private float currentTimer; // Temporizador actual
    public GameObject gameOverPanel; // Referencia al panel de Game Over en el canvas

    private void Start()
    {
        currentTimer = initialTimerDuration; // Inicializar el temporizador al valor inicial
        UpdateTimerDisplay(); // Actualizar la pantalla del temporizador al inicio
    }

    public void ResetTimer()
    {
        currentTimer = initialTimerDuration; // Reiniciar el temporizador al valor inicial al cambiar de nivel
        UpdateTimerDisplay(); // Actualizar la pantalla del temporizador al reiniciarlo
    }

    private void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime; // Actualizar el temporizador cada cuadro
            UpdateTimerDisplay(); // Actualizar la pantalla del temporizador cada vez que cambie
        }
        else
        {
            GameOver(); // Llamar al método de Game Over cuando el temporizador llegue a cero
        }
    }

    private void GameOver()
    {
        //GetComponent<Animator>().SetTrigger("OpenGameOver");
        Time.timeScale = 0;

        gameOverPanel.SetActive(true); // Activar el panel de Game Over
        // Aquí puedes agregar cualquier otra lógica que quieras ejecutar cuando el juego termine
    }

    private void UpdateTimerDisplay()
    {
        // Obtener el componente TextMeshProUGUI del objeto
        TextMeshProUGUI timerLbl = GetComponent<TextMeshProUGUI>();
        if (timerLbl != null)
        {
            // Convertir el tiempo restante a minutos y segundos
            float minutes = Mathf.FloorToInt(currentTimer / 60);
            float seconds = Mathf.FloorToInt(currentTimer % 60);

            // Actualizar el texto del temporizador
            timerLbl.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
