using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using CrossPromo.Data;


namespace CrossPromo.Core
{

    public class CrossPromoManager : MonoBehaviour
    {

        public string PlayerId;

        [SerializeField]
        private VideoPlayer videoPlayer;

        [SerializeField]
        private Button button;

        private Playlist playlist;
        private int currentEntryIndex = -1;
        PlaylistEntry currentEntry;
        private bool hasPlaylist;
        private bool hasURL;

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
                Play();
            }
        }

        private void OnDisable()
        {
            if(hasPlaylist)
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

        private void OnButtonClicked()
        {
            if (!currentEntry.IsTracked && !currentEntry.TrackPending)
            {
                SendTrackRequest();
            }
            OpenClickURL();
        }

        private void OnVideoEnded(VideoPlayer player)
        {
            Next();
        }

        private void OnVideoError(VideoPlayer player, string err)
        {
            // todo - report error
            Debug.LogError($"error on id {currentEntry.Id}: {err}");
            Next();
        }


        private void LoadVideoAtURL(string url)
        {
            videoPlayer.url = url;
            hasURL = true;
            videoPlayer.Prepare(); // todo - check if there is a prepate error?           
        }

        private void Play()
        {
            if (!hasURL) return; // todo - show error?
            videoPlayer.Play();
        }

        private void PauseVideo()
        {
            if (!hasURL) return; // todo - show error?
            videoPlayer.Pause();
        }

        private void OnPlaylistLoaded(Playlist playlist)
        {
            this.playlist = playlist;
            hasPlaylist = true;
            Next();
        }

        private void OnPlaylistLoadFailed(string err)
        {
            // todo - handle error
        }

        public void Next()
        {
            if (!hasPlaylist) return; // todo - handle error?
            currentEntryIndex = (currentEntryIndex + 1) % playlist.Count;
            currentEntry = playlist.GetEntryAt(currentEntryIndex);
            LoadAndPlay(currentEntry.VideoURL); // todo - make sure that video url isn't null

        }

        public void Pevious()
        {
            if (!hasPlaylist) return; // todo - handle error?
            currentEntryIndex = (currentEntryIndex - 1) % playlist.Count;
            currentEntry = playlist.GetEntryAt(currentEntryIndex);
            LoadAndPlay(currentEntry.VideoURL); // todo - make sure that video url isn't null
        }

        public void Pause()
        {
            if (!hasURL || videoPlayer.isPaused) return; // todo - handle error?
            PauseVideo();
        }

        public void Resume()
        {
            if (!hasURL || videoPlayer.isPlaying) return; // todo - handle error?
            Play();
        }

        private void LoadAndPlay(string url)
        {
            LoadVideoAtURL(url);
            Play();
        }

        private void SendTrackRequest()
        {
            var entry = currentEntry;
            entry.TrackPending = true;
            StartCoroutine(Interactor.SendTrackEntryRoutine(currentEntry.TrackingURL, PlayerId,
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
    }
}