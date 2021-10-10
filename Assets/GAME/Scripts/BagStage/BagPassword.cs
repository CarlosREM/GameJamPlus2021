using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPassword : MonoBehaviour
{
    private string password = "203";
    public int[] codesInt;
    public int[] relativeCodes;
    public TMPro.TextMeshProUGUI[] numbers;
    public string levelName;


    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<numbers.Length; i++)
        {
            numbers[i].text = "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstNumUp()
    {
        if (codesInt[0] < 9)
        {
            codesInt[0]++;

        }
        else
        {
            codesInt[0] = 0;
        }
        relativeCodes[0] = codesInt[0] * 100;
        int currentNumber = codesInt[0];
        numbers[0].text = currentNumber.ToString();
        checkPassword();
    }
    public void SecNumUp()
    {
        if (codesInt[1] < 9)
        {
            codesInt[1]++;

        }
        else
        {
            codesInt[1] = 0;
        }
        relativeCodes[1] = codesInt[1] * 10;
        int currentNumber = codesInt[1];
        numbers[1].text = currentNumber.ToString();
        checkPassword();
    }
    public void ThirdNumUp()
    {
        if (codesInt[2] < 9)
        {
            codesInt[2]++;
        }
        else
        {
            codesInt[2] = 0;
        }
        relativeCodes[2] = codesInt[2];
        int currentNumber = codesInt[2];
        numbers[2].text = currentNumber.ToString();
        checkPassword();
    }

    private void checkPassword()
    {
        int currentResult = relativeCodes[0] + relativeCodes[1] + relativeCodes[2];
        string currentPassword = currentResult.ToString();
        Debug.Log(currentPassword);
        if (currentPassword == password)
        {
            GameObject.Find("Scene Manager").GetComponent<TransitionManager>()
        .ChangeScene(levelName);
        }
    }
}
