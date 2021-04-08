using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

// remove
#if UNITY_EDITOR
using UnityEditor;
#endif
//

namespace DataTransfer
{
    public static class PlaylsitLoader
    {
        private const string ServerURL = "https://run.mocky.io/v3/81fab340-9550-4ab4-8859-836b01ee48ff";


        private static IEnumerator LoadRoutine(UnityAction<PlaylistResponse> onComplete)
        {
            using (var www = UnityWebRequest.Get(ServerURL))
            {
                yield return www.SendWebRequest(); 
                var responseJson = www.downloadHandler.text;
                var playlist = JsonUtility.FromJson<PlaylistResponse>(responseJson);
                onComplete(playlist);
                // todo - add check for error and add onError callback
            }
        }


// remove
#if UNITY_EDITOR
        [MenuItem("Project Tests/Load From Server")]
        public static void LoadFromServer()
        {
            CrossPromo.CrossPromoView mono = GameObject.FindObjectOfType<CrossPromo.CrossPromoView>();
            mono.StartCoroutine(LoadRoutine(playlist =>
            {
                Debug.Log(playlist);
            }));
            
        }
#endif
//
    }
}