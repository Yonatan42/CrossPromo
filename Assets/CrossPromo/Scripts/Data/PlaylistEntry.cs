using System;
using UnityEngine;


namespace CrossPromo.Data
{
    /// <summary>
    /// Playlist Entry Data Object
    /// </summary>
    [Serializable]
    public class PlaylistEntry
    {
#pragma warning disable 0649
        [SerializeField]
        private string id;
        [SerializeField]
        private string video_url;
        [SerializeField]
        private string click_url;
        [SerializeField]
        private string tracking_url;
#pragma warning restore 0649

        public string Id
        {
            get
            {
                return id;
            }
        }

        public string VideoURL
        {
            get
            {
                return video_url;
            }
        }

        public string ClickURL
        {
            get
            {
                return click_url;
            }
        }

        public string TrackingURL
        {
            get
            {
                return tracking_url;
            }
        }

        public bool IsTracked { get; set; }

        public bool TrackPending { get; set; }
    }
}