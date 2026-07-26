using System;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private GameObject UserPlayer;
    [SerializeField] private GameObject BackGround;
    [SerializeField] private GameObject ObjectSpawner;
    [SerializeField] private GameObject ObstacleType1;
    [SerializeField] private GameObject ObstacleType2;
    
    private float BackGroundSpeed;
    private float ObjectSpawnerSpeed;
    private float PlayerMeters;

    void Start()
    {
        if (ObjectSpawner == null) return;
        if (BackGround == null) return;
        if(ObstacleType1 == null) return;
        if(ObstacleType2 == null) return;

        BackGroundSpeed = BackGround.GetComponent<Scroller>().scrollSpeed;
        ObjectSpawnerSpeed = ObjectSpawner.GetComponent<ObstaclesSpawner>().tiempoEntreSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if(UserPlayer == null) return;
        PlayerMeters = UserPlayer.GetComponent<PlayerComoponent>().getScore();
        
        if(PlayerMeters%100 == 0 )
        {
            BackGround.GetComponent<Scroller>().scrollSpeed += 0.5f;
            ObjectSpawner.GetComponent<ObstaclesSpawner>().tiempoEntreSpawns -= 0.5f;

            ObstacleType1.GetComponent<Obstacle>().moveMultiplier += 0.5f;
            ObstacleType2.GetComponent<Obstacle>().moveMultiplier += 0.5f;
        }

    }
}
