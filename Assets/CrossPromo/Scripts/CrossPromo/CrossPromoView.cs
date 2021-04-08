using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Events;

namespace CrossPromo.Core
{

    public class CrossPromoView : MonoBehaviour
    {
        public string PlayerId;

        [SerializeField]
        private VideoPlayer videoPlayer;

        [SerializeField]
        private Button button;

        private bool hasURL;
        private UnityEvent videoEneded;
        private UnityEvent promoClicked;

        private void Awake()
        { 
            promoClicked = new UnityEvent();
            button.onClick.AddListener(OnButtonClicked);
            videoEneded = new UnityEvent();
            videoPlayer.loopPointReached += OnVideoEnded;
            new CrossPromoController(this);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClicked);
            videoPlayer.loopPointReached -= OnVideoEnded;
            promoClicked.RemoveAllListeners();
            videoEneded.RemoveAllListeners();
        }

        private void OnButtonClicked()
        {
            promoClicked.Invoke();
        }

        public void AddPromoClickedListener(UnityAction action)
        {
            promoClicked.AddListener(action);
        }

        public void RemovePromoClickedListener(UnityAction action)
        {
            promoClicked.RemoveListener(action);
        }

        private void OnVideoEnded(VideoPlayer player)
        {
            videoEneded.Invoke();
        }

        public void AddVideoEnededListener(UnityAction action)
        {
            videoEneded.AddListener(action);
        }

        public void RemoveVideoEnededListener(UnityAction action)
        {
            videoEneded.RemoveListener(action);
        }

        public void LoadVideoAtURL(string url)
        {
            videoPlayer.url = url;
            hasURL = true;
            videoPlayer.Prepare(); // todo - check if there is a prepate error?           
        }

        public void PlayVideo()
        {
            if (!hasURL) return; // todo - show error?
            videoPlayer.Play();
        }

        public void PauseVideo()
        {
            if (!hasURL) return; // todo - show error?
            videoPlayer.Pause();
        }
    }
}
