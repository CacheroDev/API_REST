using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class ToDo           
{
    public int userId;
    public int id;
    public string title;
    public bool completed;
}
    
public class APIManager : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] int userId;
    [SerializeField] int id;
    [SerializeField] string title;
    [SerializeField] bool completed;
    //[SerializeField] TextMeshProUGUI userIdText;
    [SerializeField] TextMeshProUGUI idText;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI completedText;


    void Start()
    {
        StartCoroutine(GetData());
        
    }

    IEnumerator GetData()
    {
        url = "https://jsonplaceholder.typicode.com/todos/3";
        using (UnityWebRequest request = UnityWebRequest.Get(url))  // 1. Send the web request
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)   // 2. Check for network or HTTP errors
            {
                Debug.LogError("The error: " + request.error);
                yield break;
            }
            
            string jsonText = request.downloadHandler.text;         // 3. Get the raw text (JSON)
            Debug.Log(jsonText);

            ToDo todo = JsonUtility.FromJson<ToDo>(jsonText);       // 4. Automatically convert JSON text into your C# object

            userId = todo.userId;
            id = todo.id;
            title = todo.title;
            completed = todo.completed;

            Debug.Log(userId);
            Debug.Log(id);
            Debug.Log(title);
            Debug.Log(completed);

            //userIdText.text = $"{id}";
            idText.text = $"{id}";
            titleText.text = $"{title}";
            completedText.text = $"{completed}";
        }
        


    }
}
