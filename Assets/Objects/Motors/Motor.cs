using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public struct motorParts
{
    public Refrigerante Refrigerante;
    public Hamster Hamster;
    public RopeMotor RopeMotor;
    public FuelPipe FuelPipe;
}



public class Motor : MonoBehaviour
{
    [Header("Motor Parts")]
    [SerializeField] public GameObject Refrigerante;
    [SerializeField] public GameObject Hamster;
    [SerializeField] public GameObject RopeMotor;
    [SerializeField] public GameObject FuelPipe;


    private int RepairedParts = 4;
    public motorParts PlayerMotorParts;
    public Dictionary<string,  PartsStates> motorPartsMap = new Dictionary<string, PartsStates>();

    void Start()
    {
        PlayerMotorParts.Refrigerante = Refrigerante.GetComponent<Refrigerante>();
        PlayerMotorParts.Hamster = Hamster.GetComponent<Hamster>();
        PlayerMotorParts.RopeMotor = RopeMotor.GetComponent<RopeMotor>();
        PlayerMotorParts.FuelPipe = FuelPipe.GetComponent<FuelPipe>();

        UpdateMap();

       
    }

    
    void Update()
    {
        UpdateMap();

        CheckMap();
        
    }


    public int getPartsInGodState()
    {
        return RepairedParts;
    }

    void CheckMap()
    {
        if (motorPartsMap == null) return;

        

        List<string> piezasARemover = new List<string>();

        // 2. Anotamos qué piezas hay que borrar sin tocar el diccionario original
        foreach (KeyValuePair<string, PartsStates> parts in motorPartsMap)
        {
            if (parts.Value == PartsStates.Damaged)
            {
                piezasARemover.Add(parts.Key);
            }
        }

        // 3. Ahora sí, borramos las piezas del diccionario de forma segura
        foreach (string nombrePieza in piezasARemover)
        {
            motorPartsMap.Remove(nombrePieza); // Solo necesitas pasarle la clave (Key)
        }

        RepairedParts = motorPartsMap.Count;
        //Debug.Log("Parts in good state: " + RepairedParts);
    }

    public void DamageAnyMotorPart(int index)
    {
        switch (index)
        {
            case 1:
                PlayerMotorParts.FuelPipe.PartDamaged();
                break;

            case 2:
                PlayerMotorParts.RopeMotor.PartDamaged();
                break;

            case 3:
                PlayerMotorParts.Refrigerante.PartDamaged();
                break;

            case 4:
                PlayerMotorParts.Hamster.PartDamaged();
                break;

        }
    }


    void UpdateMap()
    {
        motorPartsMap[PlayerMotorParts.Refrigerante.PartName] = PlayerMotorParts.Refrigerante.currentState;
        motorPartsMap[PlayerMotorParts.Hamster.PartName] = PlayerMotorParts.Hamster.currentState;
        motorPartsMap[PlayerMotorParts.FuelPipe.PartName] = PlayerMotorParts.FuelPipe.currentState;
        motorPartsMap[PlayerMotorParts.RopeMotor.PartName] = PlayerMotorParts.RopeMotor.currentState;
    }


  

}
