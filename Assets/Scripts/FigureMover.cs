using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FigureMover 
{
    private Camera _camera;
    private Vector3 touchPositionWorldFar;
    private Vector3 touchPositionWorldNear;
    private GameObject selectedFigure;
    private Rigidbody rb;
    private bool isDragable = true;
    private bool timerIsRunning;
    private bool isFall;

    private Slider _timeSlider;
    private float _timeLeft;
    private TextMeshProUGUI _loseText;
    private GameObject timeSliderObject;
    private GameObject _losePanel;
    private GameObject _winPanel;
    private int _figureCount;
    private List<GameObject> _figures;
    public void Begin(GameObject timerSlider, GameObject losePanel, GameObject winPanel)
    {
        timeSliderObject = timerSlider;
        _losePanel = losePanel;
        _winPanel = winPanel;
        _timeSlider = timeSliderObject.GetComponent<Slider>();
        _loseText = _losePanel.GetComponentInChildren<TextMeshProUGUI>();
        _camera = Camera.main;
        _figures = new List<GameObject>();
        _figures = GameObject.FindGameObjectsWithTag("dragable").ToList();
        _figureCount = _figures.Count;
    }
    public void Drag()
    {
        if (selectedFigure == null && isDragable)
        {
            RaycastHit hit = RaycastHit();
            if (hit.collider != null)
            {
                if (!hit.collider.CompareTag("dragable"))
                {
                    return;
                }
                selectedFigure = hit.collider.gameObject;
            }
        }
        if (selectedFigure != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.WorldToScreenPoint(selectedFigure.transform.position).z);
            Vector3 worldPosition = _camera.ScreenToWorldPoint(position);
            selectedFigure.transform.position = new Vector3(worldPosition.x, worldPosition.y, -29);
        }
    }
    public void EndDrag()
    {
        if (selectedFigure != null)
        {
            rb = selectedFigure.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            selectedFigure.tag = "Untagged";
            selectedFigure = null;
            timerIsRunning = true;
            isDragable = false;
        }
    }
    private RaycastHit RaycastHit()
    {
        Vector3 touchPositionFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.farClipPlane);
        Vector3 touchPositionNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane);
        touchPositionWorldFar = _camera.ScreenToWorldPoint(touchPositionFar);
        touchPositionWorldNear = _camera.ScreenToWorldPoint(touchPositionNear);
        RaycastHit hit;
        Physics.Raycast(touchPositionWorldNear, touchPositionWorldFar - touchPositionWorldNear, out hit);
        Debug.DrawRay(touchPositionWorldNear, touchPositionWorldFar - touchPositionWorldNear);
        return hit;
    }
    public void Timer()
    {
        if (rb != null && !isFall)
        {
            if (rb.velocity.y == 0 && timerIsRunning)
            {
                if (_timeLeft < 2f)
                {
                    _timeLeft += Time.deltaTime;
                    _timeSlider.value = _timeLeft;
                    timeSliderObject.SetActive(true);
                }
                else
                {
                    _timeLeft = 0f;
                    timerIsRunning = false;
                    isDragable = true;
                    timeSliderObject.SetActive(false);
                    CheckLastFigure();
                }
            }
        }
    }
    private void CheckLastFigure()
    {
        _figureCount -= 1;
        if (_figureCount == 0) Win();
    }
    public void FigureWet()
    {
        isFall = true;
        _losePanel.SetActive(true);
        _loseText.text = "do not fall into the water";

    }
    public void FigureOut()
    {
        isFall = true;
        _losePanel.SetActive(true);
        _loseText.text = "don't let it fall outside the bowl";
    }
    private void Win()
    {
        _winPanel.SetActive(true);
    }
}
