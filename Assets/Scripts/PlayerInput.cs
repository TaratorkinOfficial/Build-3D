using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerInput : MonoBehaviour, IDragHandler,IEndDragHandler
{
    private FigureMover figureMover = new FigureMover();
    [SerializeField] private GameObject timeSlider;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    private void Start()
    {
        figureMover.Begin(timeSlider, losePanel, winPanel);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        figureMover.Drag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        figureMover.EndDrag();
    }
    public void FigureOut()
    {
        figureMover.FigureOut();

    }
    public void FigureWet()
    {
        figureMover.FigureWet();
    }

    private void Update()
    {
       figureMover.Timer();
    }
}
