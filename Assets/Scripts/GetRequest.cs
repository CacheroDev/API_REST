using System.Collections;

using UnityEngine;
using UnityEngine.Networking;


public class GetRequest : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(FetchDataFromServer());
    }

    IEnumerator FetchDataFromServer()
    {
        string url = "https://jsonplaceholder.typicode.com/todos/";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            string jsonResponse = request.downloadHandler.text;

            Debug.Log(jsonResponse);
        }
    }
}
