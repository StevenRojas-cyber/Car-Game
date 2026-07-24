using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Refrigerante : MotorPart, IPointerDownHandler, IPointerUpHandler
{
    [Header("Refrigerante Properties")]
    [SerializeField] private float FillSpeed = 1f;
    [SerializeField] private const float MaxRefrigeranteLevel = 10f;
    [SerializeField] private int CurrentRefrigerantePercentage = 100;
    [SerializeField] private TMP_Text Level;

    private bool isFilling = false;
    private float CurrentLevelFiled = 10f;

    void Start()
    {
        PartName = "Refrigerante";
        currentState = PartsStates.Repaired;

        CurrentRefrigerantePercentage = Mathf.FloorToInt((CurrentLevelFiled / MaxRefrigeranteLevel) * 100f);

        Level.text = CurrentRefrigerantePercentage.ToString() + " %";
    }


    void Update()
    {
        Debug.Log("Refrigerante State: " + currentState.ToString());

        //En base al booleano isFilling, se aumenta o disminuye el nivel de refrigerante y se actualiza el porcentaje correspondiente
        if (isFilling)
        {
            CurrentLevelFiled += FillSpeed * Time.deltaTime;
            CurrentRefrigerantePercentage = Mathf.FloorToInt((CurrentLevelFiled / MaxRefrigeranteLevel) * 100f);
            Level.text = CurrentRefrigerantePercentage.ToString() + " %";
        }
        else
        {
            CurrentLevelFiled -= FillSpeed * Time.deltaTime;
            CurrentRefrigerantePercentage = Mathf.FloorToInt((CurrentLevelFiled / MaxRefrigeranteLevel) * 100f);
            Level.text = CurrentRefrigerantePercentage.ToString() + " %";
        }


        //Para que el porcentaje del refrigerante no sea mayor a 100
        if (CurrentLevelFiled >= MaxRefrigeranteLevel)
        {
            CurrentLevelFiled = MaxRefrigeranteLevel;
            CurrentRefrigerantePercentage = 100;
            Level.text = CurrentRefrigerantePercentage.ToString() + " %";
        }


        //Para que el porcentaje del refrigerante no sea negativo
        if (CurrentLevelFiled <= 0f)
        {
            CurrentLevelFiled = 0f;
            CurrentRefrigerantePercentage = 0;
            Level.text = CurrentRefrigerantePercentage.ToString() + " %";
        }


        //Actualiza el estado del refrigerante dependiendo de su porcentaje actual
        if (CurrentRefrigerantePercentage <= 20)
        {
            currentState = PartsStates.Damaged;
        }
        else
        {
            currentState = PartsStates.Repaired;
        }

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"You clicked on {PartName}");
        
        isFilling = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"You released the click on {PartName}");
        
        isFilling = false;
    }

}
