using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

namespace CrossPromo.Data
{
    /// <summary>
    /// Responsible for creating server requests and firing their callbacks
    /// </summary>
    public static class Interactor
    {
        /// <summary>
        /// Method for downloading the playlist from the server
        /// </summary>
        /// <param name="onComplete">callback for when the request was successful</param>
        /// <param name="onError">callback for when the request failed</param>
        /// <returns>returns a routine for the server request to be run as Coroutine</returns>
        public static IEnumerator ReceivePlaylistRoutine(UnityAction<Playlist> onComplete, UnityAction<string> onError)
        {
            const string ServerURL = "https://run.mocky.io/v3/81fab340-9550-4ab4-8859-836b01ee48ff";
            using (var www = UnityWebRequest.Get(ServerURL))
            {
                yield return www.SendWebRequest();
                if (www.error != null)
                {
                    onError(www.error);
                }
                else
                {
                    var responseJson = www.downloadHandler.text;
                    var response = JsonUtility.FromJson<Playlist>(responseJson);
                    onComplete(response);
                }
            }
        }

        /// <summary>
        /// Method for sending the track request using the tracking URL
        /// </summary>
        /// <param name="rawTrackingUrl">the tracking URL as received in the playlist</param>
        /// <param name="playerId">the player id of the CrossPromo isntance</param>
        /// <param name="onComplete">callback for when the request was successful</param>
        /// <param name="onError">callback for when the request failed</param>
        /// <returns>returns a routine for the server request to be run as Coroutine</returns>
        public static IEnumerator SendTrackEntryRoutine(string rawTrackingUrl, string playerId, UnityAction onComplete, UnityAction<string> onError)
        {
            const string PlayerIdPlaceholder = "[PLAYER_ID]";
            string tractingUrl = rawTrackingUrl.Replace(PlayerIdPlaceholder, playerId);
            using(var www = UnityWebRequest.Get(tractingUrl))
            {
                yield return www.SendWebRequest();
                if (www.error != null)
                {
                    onError(www.error);
                }
                else
                {
                    onComplete();
                }
            }
        }
    }
}