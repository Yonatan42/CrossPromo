// todo - delete this class

using UnityEngine;
using CrossPromo.Data;
using System.Collections;

// remove
#if UNITY_EDITOR
using UnityEditor;
#endif
// remove

namespace CrossPromo.Core
{

    public class CrossPromoController
    {
        // remove
        private static CrossPromoController instance;
        //

        private readonly CrossPromoView view;
        private Playlist playlist;
        private int currentEntryIndex = -1;
        PlaylistEntry currentEntry;
        private bool isReady;

        public CrossPromoController(CrossPromoView view)
        {
            this.view = view;
            Initialize();
            // remove
            instance = this;
            //
        }

        public void Initialize()
        {
            SendRequest(Interactor.ReceivePlaylistRoutine(OnPlaylistLoaded, OnPlaylistLoadFailed));
            view.AddVideoEnededListener(OnEntryCompleted);
            view.AddPromoClickedListener(OnPromoClicked);
        }

        public void OnEntryCompleted()
        {
            Next();
        }

        private void SendRequest(IEnumerator routine)
        {
            view.StartCoroutine(routine);
        }

        private void OnPlaylistLoaded(Playlist playlist)
        {
            this.playlist = playlist;
            isReady = true;
            Next();
        }

        private void OnPlaylistLoadFailed(string err)
        {
            // todo - handle error
        }

        public void SetPlayerId(string playerId)
        {
            view.PlayerId = playerId;
        }

        public void Next()
        {
            if (!isReady) return; // todo - handle error?
            currentEntryIndex = (currentEntryIndex + 1) % playlist.Count;
            currentEntry = playlist.GetEntryAt(currentEntryIndex);
            LoadAndPlay(currentEntry.VideoURL); // todo - make sure that video url isn't null
     
        }

        public void Pevious()
        {
            if (!isReady) return; // todo - handle error?
            currentEntryIndex = (currentEntryIndex - 1) % playlist.Count;
            currentEntry = playlist.GetEntryAt(currentEntryIndex);
            LoadAndPlay(currentEntry.VideoURL); // todo - make sure that video url isn't null
        }

        public void Pause()
        {
            if (!isReady) return; // todo - handle error?
            view.PauseVideo();
        }

        public void Resume()
        {
            if (!isReady) return; // todo - handle error?
            view.PlayVideo();
        }

        private void LoadVideoAtURL(string url)
        {
            view.LoadVideoAtURL(url);
        }

        private void PlayVideo()
        {
            view.PlayVideo();
        }

        private void LoadAndPlay(string url)
        {
            LoadVideoAtURL(url);
            PlayVideo();
        }

        private void OnPromoClicked()
        {
            if (!currentEntry.IsTracked && !currentEntry.TrackPending)
            {
                SendTrackRequest();
            }
            OpenClickURL();
        }

        private void SendTrackRequest()
        {
            var entry = currentEntry;
            entry.TrackPending = true;
            SendRequest(Interactor.SendTrackEntryRoutine(currentEntry.TrackingURL, view.PlayerId,
                () =>
                {
                    entry.IsTracked = true;
                    entry.TrackPending = false;
                },
                err =>
                {
                    // todo - handle error
                    entry.TrackPending = false;
                }));
            
        }

        private void OpenClickURL()
        {
            Application.OpenURL(currentEntry.ClickURL);
        }


// remove
#if UNITY_EDITOR
        [MenuItem("Project Tests/play video")]
        public static void CreateTestObject()
        {
            string testVid = "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4";
            instance.LoadVideoAtURL(testVid);
            instance.PlayVideo();
        }
#endif
//
    }
}