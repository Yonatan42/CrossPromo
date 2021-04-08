using UnityEngine;
using UnityEngine.Video;

namespace PromoPanel
{

    public class CrossPromoView : MonoBehaviour
    {
        [SerializeField]
        private string playerId;

        [SerializeField]
        private VideoPlayer videoPlayer;


        public string PlayerId
        {
            get
            {
                return playerId;
            }
        }

        public void LoadVideoAtURL(string url)
        {

        }
    }
}
