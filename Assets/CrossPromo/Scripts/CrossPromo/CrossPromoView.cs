using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace CrossPromo
{

    public class CrossPromoView : MonoBehaviour
    {
        [SerializeField]
        private string playerId;

        [SerializeField]
        private VideoPlayer videoPlayer;

        [SerializeField]
        private Button button;

        private bool hasURL;

        public string PlayerId
        {
            get
            {
                return playerId;
            }
        }

        private void Awake()
        {
            new CrossPromoController(this);
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            // remove
            if (videoPlayer.isPlaying)
            {
                PauseVideo();
            }
            else
            {
                PlayVideo();
            }
            //
        }

        public void LoadVideoAtURL(string url)
        {
            videoPlayer.url = url;
            hasURL = true;
            videoPlayer.Prepare();            
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
