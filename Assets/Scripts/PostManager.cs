using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

[System.Serializable]
public class PostData
{
    public int id;
    public string title;
    public bool completed;
}

public class PostManager : MonoBehaviour
{
    [SerializeField] string url = "https://reqbin.com/echo/post/json";
    [SerializeField] PostData data = new PostData { id = 1, title = "Test Post", completed = false };
    void Start()
    {
        StartCoroutine(PostRequest());
    }

    void Update()
    {
        
    }

    IEnumerator PostRequest()
    {
        string json = JsonUtility.ToJson(data);
        byte[] body = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Conntent-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"{request.result} | {request.responseCode} | {request.error}");
                yield break;
            }
            Debug.Log("Sent: " + json);
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}
