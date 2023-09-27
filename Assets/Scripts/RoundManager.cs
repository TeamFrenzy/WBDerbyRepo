using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] internal FloorManager floorManager;
    
    [SerializeField] private GameObject powerBoxPrefab;

    [SerializeField] private float powerBoxSpawnTime;
    [SerializeField] private float powerBoxSpawnHeight;

    [SerializeField] private GameObject waterBody;

    [SerializeField] private int amountOfPlayers;
    [SerializeField] private GameObject[] playerCharacters;
    [SerializeField] private Vector3[] player1StartPositions;
    [SerializeField] private Vector3[] player1StartRotations;
    [SerializeField] private Vector3[] player2StartPositions;
    [SerializeField] private Vector3[] player2StartRotations;
    [SerializeField] private Vector3[] player3StartPositions;
    [SerializeField] private Vector3[] player3StartRotations;
    [SerializeField] private Vector3[] player4StartPositions;
    [SerializeField] private Vector3[] player4StartRotations;
    
    public float boxSpawnTimer;
    void Awake()
    {
        floorManager= GetComponent<FloorManager>();
        boxSpawnTimer = powerBoxSpawnTime;
        SpawnPlayers();
    }
    void Update()
    {
        boxSpawnTimer -= Time.deltaTime;
        if (boxSpawnTimer < 0f)
        {
            SpawnPowerBox();
            boxSpawnTimer = powerBoxSpawnTime;
        }
    }

    private void SpawnPlayers()
    {
        
        
        if (amountOfPlayers == 2)
        {
            playerCharacters[0].transform.SetPositionAndRotation(player1StartPositions[0], Quaternion.Euler(player1StartRotations[0]));
            playerCharacters[1].transform.SetPositionAndRotation(player2StartPositions[0], Quaternion.Euler(player2StartRotations[0]));
        }
        else if (amountOfPlayers == 3)
        {
            playerCharacters[0].transform.SetPositionAndRotation(player1StartPositions[1], Quaternion.Euler(player1StartRotations[1]));
            playerCharacters[1].transform.SetPositionAndRotation(player2StartPositions[1], Quaternion.Euler(player2StartRotations[1]));
            playerCharacters[2].transform.SetPositionAndRotation(player3StartPositions[0], Quaternion.Euler(player3StartRotations[0]));
            
        }
        else if (amountOfPlayers == 4)
        {
            playerCharacters[0].transform.SetPositionAndRotation(player1StartPositions[2], Quaternion.Euler(player1StartRotations[2]));
            playerCharacters[1].transform.SetPositionAndRotation(player2StartPositions[2], Quaternion.Euler(player2StartRotations[2]));
            playerCharacters[2].transform.SetPositionAndRotation(player3StartPositions[1], Quaternion.Euler(player3StartRotations[1]));
            playerCharacters[2].transform.SetPositionAndRotation(player4StartPositions[0], Quaternion.Euler(player4StartRotations[0]));
        }
    }

    /*
    void reshuffle(string[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++ )
        {
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }
    */
    
    void SpawnPowerBox()
    {
        Vector3 fallPosition = Random.insideUnitCircle * floorManager.floorLayerChangingScales[floorManager.currentRow];
        fallPosition = new Vector3(fallPosition.x, 5, fallPosition.z);
        GameObject powerBoxSpawn = Instantiate(powerBoxPrefab, fallPosition, Quaternion.identity);
        powerBoxSpawn.GetComponent<PowerUpBox>().SetFallingSpeed(-2f);
    }
    
}
