using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Would have represented one save file to determine which data to save and pass to SaveAndLoadSystem.
//Part of the saving and loading system that was never fully implemented and was removed from the final game.

[System.Serializable]
public class SaveData
{
    public GameStateData gameState;
    public PlayerMovementData playerMove;
    public CameraData camera;
    public InputData input;
    //public VideoSettingsData video;
    //public AudioSettingsData audio;
    public Scene currentLevel;

    public SaveData(Player player)
    {
        //gameState = player.gameState;
        playerMove = player.playerMove;
        //camera = player.camera;
        //input = player.input;
        //video = player.video;
        //audio = player.audio;
        currentLevel = player.currentLevel;
    }
}
