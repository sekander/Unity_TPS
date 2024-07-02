using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    private int p1_score;
    private int p2_score;

    private int total_life = 6;
    private int killCount;

    private String server_ip;
    
    private bool is_player_host;
/*     
    public bool Is_Player_Host
    {
        get { return is_player_host; }
        set { is_player_host = value; }
    }
 */


    public enum PLAY_MODE
    {
        SINGLE_PLAYER,
        MULTI_PLAYER,
        ONLINE
    }

    private PLAY_MODE playMode = PLAY_MODE.SINGLE_PLAYER; 

    public PLAY_MODE GetPLAY_MODE() { return playMode; }   

    public void SetPlayMode(PLAY_MODE mode)
    {
        playMode = mode;
    }

    public void SetTotalLife(int _total_life) { total_life = _total_life;}
    public int GetTotalLife() { return total_life;} 


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public int GetPlayeOneScore()
    {
        return p1_score;
    }
    
    public int GetPlayeTwoScore()
    {
        return p2_score;
    }
    public void ResetPlayerOneScore()
    {
        p1_score = 0;
    }
    public void ResetPlayerTwoScore()
    {
        p2_score = 0;
    }

    public int GetKillCount()
    {
        return killCount;
    }

    public void IncreasePlayerOneScore(int amount)
    {
        p1_score += amount;
    }
    public void IncreasePlayerTwoScore(int amount)
    {
        p2_score += amount;
    }

    public void IncreaseKillCount(int amount)
    {
        killCount += amount;
    }

    public String GetServerIP() 
    {
        return server_ip;
    }

    public void SetServerIP(String ip)
    {
        server_ip = ip;
    }
    
    public bool GetIsPlayerHost()
    {
        return is_player_host;
    }

    public void SetIsPlayerHost(bool isHost)
    {
        is_player_host = isHost;
    }

    public void LoadPlayScene(){
        SceneManager.LoadScene(2);
    }
}
