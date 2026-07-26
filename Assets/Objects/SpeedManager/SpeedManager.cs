using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private GameObject UserPlayer;
    [SerializeField] private GameObject BackGround;
    [SerializeField] private GameObject ObjectSpawner;

    // Ya no arrastramos los Prefabs aquí, los hemos eliminado.

    // 1. Creamos una variable ESTÁTICA. Al ser estática, cualquier script 
    // de tu juego puede leerla sin necesidad de arrastrar referencias.
    public static float velocidadExtraObstaculos = 0f;

    private float PlayerMeters;

    // 2. Controlamos exactamente cuándo subir la dificultad
    private int siguienteMeta = 100;

    void Start()
    {
        if (ObjectSpawner == null || BackGround == null) return;

        // Es vital reiniciar esto a 0 cada vez que arranca el nivel
        // para que al hacer "Retry" en el Game Over la velocidad vuelva a la normalidad.
        velocidadExtraObstaculos = 0f;
    }

    void Update()
    {
        if (UserPlayer == null) return;
        PlayerMeters = UserPlayer.GetComponent<PlayerComoponent>().getScore();

        // 3. Solo entramos al IF si superamos la meta actual (ej: 100)
        if (PlayerMeters >= siguienteMeta)
        {
            // Aumentamos la velocidad del fondo
            BackGround.GetComponent<Scroller>().scrollSpeed += 0.5f;

            // PRECAUCIÓN: Evitamos que el tiempo de spawn llegue a 0 o números negativos,
            // de lo contrario el juego crasheará intentando generar infinitos objetos.
            ObstaclesSpawner spawner = ObjectSpawner.GetComponent<ObstaclesSpawner>();
            if (spawner.tiempoEntreSpawns > 0.5f)
            {
                spawner.tiempoEntreSpawns -= 0.5f;
            }

            // Sumamos velocidad a nuestra variable global
            velocidadExtraObstaculos += 0.5f;

            // Calculamos la nueva meta para que la próxima subida sea a los 200, luego 300, etc.
            siguienteMeta += 100;

            Debug.Log("¡Dificultad Aumentada! Próxima meta a los: " + siguienteMeta + "m");
        }
    }
}