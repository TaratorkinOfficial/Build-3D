using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("water"))
        {
            playerInput.FigureWet();
            print("wet");
        }
        if (other.CompareTag("out"))
        {
            print("out");

            playerInput.FigureOut();
        }
    }
}
