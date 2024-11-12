using Assets.GameCore.Events;
using Assets.Scripts.Utility;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : IWebRequest
{
    public async Task FetchDataAsync(string url)
    {
        //check if the URL is valid
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogError("URL is empty or null.");
            return;
        }

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            var operation = request.SendWebRequest();

            //wait for completion of the request
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            //handle potential errors
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.DataProcessingError)
            {
                EventBus.Instance.Publish(TypeOfEvent.Error, new ErrorEvent($"Error fetching data: {request.error}"));
                return;
            }

            //send json text in event
            string jsonResult = request.downloadHandler.text;
            EventBus.Instance.Publish(TypeOfEvent.ItemListServerResponse, new ServerResponseEvent(jsonResult));
        }
    }
}
