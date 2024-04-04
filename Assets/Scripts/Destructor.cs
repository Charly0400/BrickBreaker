using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
public class Destructor : MonoBehaviour
{
    public GameObject gameOverPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
}
