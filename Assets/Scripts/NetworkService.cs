using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService 
{
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/forecast?q=Dnipro&mode=xml&appid=748e342d3543f5dcba5f30aed0f55584";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/forecast?q=Dnipro&appid=748e342d3543f5dcba5f30aed0f55584";
    private const string webImage = "https://www.rabstol.net/uploads/gallery/main/407/rabstol_net_world_of_warcraft_08.jpg";
    private const string localApi = "http://localhost/uia/api.php";
    private IEnumerator CallAPI(string url,WWWForm form, Action<string> callback)
    {
        using (UnityWebRequest request = (form == null) ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form)) 
        {
            yield return request.SendWebRequest();

            

            if (request.result == UnityWebRequest.Result.ConnectionError)
            { 
                Debug.LogError("Network problem: " + request.error);
            }else if(request.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                Debug.LogError("Responce problem: " + request.responseCode);
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(xmlApi, null, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(jsonApi, null, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("message", name);
        form.AddField("cloud value", cloudValue.ToString());
        form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

        return CallAPI(localApi, form, callback);
    }
}
