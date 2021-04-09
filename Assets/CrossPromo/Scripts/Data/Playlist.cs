using System;
using UnityEngine;

namespace CrossPromo.Data
{
    /// <summary>
    /// Playlist Data Object
    /// </summary>
    [Serializable]
    public struct Playlist
    {
#pragma warning disable 0649
        [SerializeField]
        private PlaylistEntry[] results;
#pragma warning restore 0649

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
