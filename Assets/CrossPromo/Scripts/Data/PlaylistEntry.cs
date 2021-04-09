﻿using System;
using UnityEngine;


namespace CrossPromo.Data
{
    [Serializable]
    public class PlaylistEntry
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private string video_url;
        [SerializeField]
        private string click_url;
        [SerializeField]
        private string tracking_url;


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

        // remove
        public override string ToString()
        {
            return string.Format("id: {0}, vid url: {1}, click url: {2}, track url: {3}", id, video_url, click_url, tracking_url);
        }
        //
    }
}