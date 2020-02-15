using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//A class that was intended to hold player data to be serialized.
//Part of the saving and loading system that was never fully implemented and was removed from the final game.

public class Player : MonoBehaviour
{
    public GameStateData gameState;
    public PlayerMovementData playerMove;
    public CameraData camera;
    public InputData input;
    //public VideoSettingsData video;
    //public AudioSettingsData audio;
    public Scene currentLevel;

    void Update()
    {
        //gameState = GameObject.FindObjectOfType<GameStateManager>().setGameStateData();
        playerMove = GameObject.FindObjectOfType<PlayerMovement>().setPlayerData();
        //camera = GameObject.FindObjectOfType<CameraManager>().setCameraData();
        //input = GameObject.FindObjectOfType<KeyManager>().setInputData();
        //video = GameObject.FindObjectOfType<SettingsVideoMenu>().setVideoSettings();
        //audio = GameObject.FindObjectOfType<SettingsAudioMenu>().setAudioData();

        currentLevel = SceneManager.GetActiveScene();
    }

    public void savePlayerData()
    {
        SaveAndLoadSystem.Save(this);
        Debug.Log("SAVING player data...");
        Debug.Log("Current scene: " + currentLevel.name);
    }

    public void loadPlayerData()
    {
        SaveData data = SaveAndLoadSystem.Load();

        //gameState = data.gameState;
        playerMove = data.playerMove;
        //camera = data.camera;
        //input = data.input;
        //video = data.video;
        //audio = data.audio;
        currentLevel = data.currentLevel;

        Debug.Log("LOADING player data...");
        Debug.Log("Current scene: " + currentLevel.name);
    }
}
