﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using CrossPromo.Data;
using Logger = CrossPromo.Logging.Logger;

namespace CrossPromo.Core
{
    /// <summary>
    /// Main class and MonoBehaviour responsible for the CrossPromo prefabs
    /// </summary>
    public class CrossPromoManager : MonoBehaviour
    {
#pragma warning disable 0649
        public string PlayerId;

        [SerializeField]
        private VideoPlayer videoPlayer;

        [SerializeField]
        private Button button;
#pragma warning disable 0649

        private Playlist playlist;
        private int currentEntryIndex = -1;
        PlaylistEntry currentEntry;
        private bool hasPlaylist;
        private bool hasURL;
        private double pausedTime;

        #region Unity Lifecycle Event Methods

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClicked);
            videoPlayer.loopPointReached += OnVideoEnded;
            videoPlayer.errorReceived += OnVideoError;

            StartCoroutine(Interactor.ReceivePlaylistRoutine(OnPlaylistLoaded, OnPlaylistLoadFailed));
        }

        private void OnEnable()
        {
            if(hasPlaylist)
            {
                Resume();
            }
        }

        private void OnDisable()
        {
            if (hasPlaylist)
            {
                Pause();
            }
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClicked);
            videoPlayer.loopPointReached -= OnVideoEnded;
            videoPlayer.errorReceived -= OnVideoError;
        }

        #endregion

        #region UI Event Methods

        private void OnButtonClicked()
        {
            if (!currentEntry.IsTracked && !currentEntry.TrackPending)
            {
                SendTrackRequest();
            }
            OpenClickURL();
        }

        #endregion


        #region Video Event Methods

        private void OnVideoEnded(VideoPlayer player)
        {
            pausedTime = 0;
            Next();
        }

        private void OnVideoError(VideoPlayer player, string err)
        {
            Debug.LogError($"Video error occurred for playlist entry with id {currentEntry.Id}: {err}");
            Debug.LogWarning("Skipping current playlist entry");
            Next();
        }

        #endregion

        #region Playlist Loading Callback Methods

        private void OnPlaylistLoaded(Playlist playlist)
        {
            this.playlist = playlist;
            hasPlaylist = true;
            Next();
        }

        private void OnPlaylistLoadFailed(string err)
        {
            Logger.LogError("Error occurred while downloading playlist: " + err);
        }

        #endregion

        #region Public API Methods

        public void Next()
        {
            if (!hasPlaylist)
            {
                Logger.LogWarning("Attempted to invoke Next method without a loaded playlist");
                return;
            }
            currentEntryIndex = (currentEntryIndex + 1) % playlist.Count;
            currentEntry = playlist.GetEntryAt(currentEntryIndex);
            LoadAndPlay(currentEntry.VideoURL);

        }

        public void Pevious()
        {
            if (!hasPlaylist)
            {
                Logger.LogWarning("Attempted to invoke the Pervious method without a loaded playlist");
                return;
            }
            currentEntryIndex = (currentEntryIndex - 1) % playlist.Count;
            currentEntry = playlist.GetEntryAt(currentEntryIndex);
            LoadAndPlay(currentEntry.VideoURL);
        }

        public void Pause()
        {
            if (!hasURL)
            {   Logger.LogWarning("Attempted to invoke the Pause method without a loaded video url");
                return;
            }
            if (videoPlayer.isPaused) return;

            videoPlayer.Pause();
            pausedTime = videoPlayer.time;
        }

        public void Resume()
        {
            if (!hasURL)
            {
                Logger.LogWarning("Attempted to invoke the Resume method without a loaded video url");
                return;
            }
            if (videoPlayer.isPlaying) return;
            videoPlayer.Play();
            videoPlayer.time = pausedTime;
        }

        #endregion

        #region Auxillary Video Methods

        private void LoadVideoAtURL(string url)
        {
            videoPlayer.url = url;
            Logger.Log("Video URL set to: " + url);
            hasURL = true;
            videoPlayer.Prepare();
        }

        private void LoadAndPlay(string url)
        {
            LoadVideoAtURL(url);
            Resume();
        }

        #endregion

        #region Misc Methods

        private void SendTrackRequest()
        {
            var entry = currentEntry;
            entry.TrackPending = true;
            StartCoroutine(Interactor.SendTrackEntryRoutine(currentEntry.TrackingURL, PlayerId,
                () =>
                {
                    entry.IsTracked = true;
                    entry.TrackPending = false;
                    Logger.Log($"Playlist entry with id {entry.Id} marked as tracked");
                },
                err =>
                {
                    entry.TrackPending = false;
                    Logger.LogError($"Error occurred while tracking entry with id {entry.Id}: {err}");
                }));

        }

        private void OpenClickURL()
        {
            string url = currentEntry.ClickURL;
            Logger.Log($"Openeing URL {url} in browser");
            Application.OpenURL(url);
        }

        #endregion
    }
}