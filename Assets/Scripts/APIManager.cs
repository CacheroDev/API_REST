using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class ToDo           
{
    public int id;
    public string title;
    public bool completed;
}
    
public class APIManager : MonoBehaviour
{
    [SerializeField] string url = "https://jsonplaceholder.typicode.com/todos/1";
    [SerializeField] int id;
    [SerializeField] string title;
    [SerializeField] bool completed;

    void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))  // 1. Send the web request
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)   // 2. Check for network or HTTP errors
            {
                Debug.LogError("The error: " + request.error);
                yield break;
            }
            //else Debug.Log("Succcessful");

            string jsonText = request.downloadHandler.text;         // 3. Get the raw text (JSON)
            //Debug.Log("Raw JSON received: " + jsonText);

            ToDo todo = JsonUtility.FromJson<ToDo>(jsonText);       // 4. Automatically convert JSON text into your C# object

            id = todo.id;
            title = "Title: " + todo.title;
            completed = todo.completed;

            Debug.Log(id);
            Debug.Log(title);
            Debug.Log(completed);
        }
        


    }
}
