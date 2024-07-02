using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadPlayScene(){
        SceneManager.LoadScene(1);
    }

    public void TogglePlayPanel(Canvas canvas){
        if (canvas != null)
        {
            canvas.enabled = !canvas.enabled;
        }
        else
        {
            Debug.Log("Canvas referenced not assigned");
        }
    }

}
