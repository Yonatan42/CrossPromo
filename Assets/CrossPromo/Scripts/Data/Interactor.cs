using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

// remove
#if UNITY_EDITOR
using UnityEditor;
#endif
//

namespace CrossPromo.Data
{
    public static class Interactor
    { 
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


        // remove
#if UNITY_EDITOR
        [MenuItem("Project Tests/Load From Server")]
        public static void LoadFromServer()
        {
            CrossPromo.Core.CrossPromoView mono = GameObject.FindObjectOfType<CrossPromo.Core.CrossPromoView>();
            mono.StartCoroutine(ReceivePlaylistRoutine(playlist =>
            {
                Debug.Log(playlist);
            }, err =>
            {
                Debug.Log(err);
            }));
            
        }
#endif
//
    }
}