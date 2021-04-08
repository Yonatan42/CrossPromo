using System;
using UnityEngine;

// remove
#if UNITY_EDITOR
using UnityEditor;
using System.Text;
#endif
//


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

        // remove
        public override string ToString()
        {
            string st = "";

            foreach(var result in results)
            {
                st += result.ToString()+"\n";
            }

            return st;
        }
        //

// remove
#if UNITY_EDITOR
        [MenuItem("Project Tests/CreateTextJson")]
        public static void CreateTestObject()
        {
            var myObj = JsonUtility.FromJson<Playlist>(GetTestJson());
            Debug.Log(myObj);

        }

        private static string GetTestJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("    \"results\": [");
            sb.AppendLine("        {");
            sb.AppendLine("            \"id\": \"1\",");
            sb.AppendLine("            \"video_url\": \"[the url]\",");
            sb.AppendLine("            \"click_url\": \"[the url]\",");
            sb.AppendLine("            \"tracking_url\": \"[the url]\"");
            sb.AppendLine("        },");
            sb.AppendLine("        {");
            sb.AppendLine("            \"id\": \"2\",");
            sb.AppendLine("            \"video_url\": \"[the url]\",");
            sb.AppendLine("            \"click_url\": \"[the url]\",");
            sb.AppendLine("            \"tracking_url\": \"[the url]\"");
            sb.AppendLine("        },");
            sb.AppendLine("        {");
            sb.AppendLine("            \"id\": \"3\",");
            sb.AppendLine("            \"video_url\": \"[the url]\",");
            sb.AppendLine("            \"click_url\": \"[the url]\",");
            sb.AppendLine("            \"tracking_url\": \"[the url]\"");
            sb.AppendLine("        }");
            sb.AppendLine("    ]");
            sb.AppendLine("}");

            return sb.ToString();


#endif
//

        }

    }
}

/*
{
    "results": [
        {
            "id": "1",
            "video_url": "[the url]",
            "click_url": "[the url]",
            "tracking_url": "[the url]"
        },
        {
            "id": "2",
            "video_url": "[the url]",
            "click_url": "[the url]",
            "tracking_url": "[the url]"
        },
        {
            "id": "3",
            "video_url": "[the url]",
            "click_url": "[the url]",
            "tracking_url": "[the url]"
        }
    ]
}
*/