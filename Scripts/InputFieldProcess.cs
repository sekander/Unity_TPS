using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InputFieldProcess : MonoBehaviour
{   public TMP_InputField inputField;
    public Toggle   toggle;
    
    void Start()
    {
        // Get a reference to the InputField component
        inputField = GetComponent<TMP_InputField>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Toggle is ON");
            // Perform actions when the toggle is ON
            GameManger.instance.SetIsPlayerHost(true);
        }
        else
        {
            Debug.Log("Toggle is OFF");
            GameManger.instance.SetIsPlayerHost(false);
            // Perform actions when the toggle is OFF
        }
    }

    public void ProcessInput(string userInput)
    {
       // Check if the input matches the IP address pattern
        if (IsValidIPAddress(userInput))
        {
            Debug.Log("Valid IP address: " + userInput);
            // Do something with the valid IP address

            //Store server ip address 
            GameManger.instance.SetServerIP(userInput);
        }
        else
        {
            Debug.Log("Invalid IP address: " + userInput);
            // Handle invalid input
        }
    }

        // Function to validate IP address using regular expression
    bool IsValidIPAddress(string ipAddress)
    {
        // Regular expression pattern for IP address (IPv4)
        string pattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        // Create regex object
        Regex regex = new Regex(pattern);

        // Check if input matches the pattern
        return regex.IsMatch(ipAddress);
    }
}
