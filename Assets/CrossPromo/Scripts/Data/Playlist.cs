using System;
using UnityEngine;

namespace CrossPromo.Data
{

    [Serializable]
    public struct Playlist
    {
        [SerializeField]
        private PlaylistEntry[] results;

        public int Count
        {
            get
            {
                if (results == null) return -1;
                return results.Length;
            }
        }

        public PlaylistEntry GetEntryAt(int index)
        {
            if (results == null || index < 0 || index >= results.Length) return default;
            return results[index];
        }
    }
}
