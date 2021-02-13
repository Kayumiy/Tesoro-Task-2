using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Project : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string ID;
    public string nameOfProject;
    public Vector2 position;
    public string classType;
    public int sum;
    public int numberOfApartments;

    TMP_Text nameTxt;




    private void Start()
    {
        nameTxt = transform.Find("Name").GetComponent<TMP_Text>();
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        nameTxt.text = nameOfProject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nameTxt.text = "";
    }
}
