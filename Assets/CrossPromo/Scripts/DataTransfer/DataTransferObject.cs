using System;
using UnityEngine;

// remove
#if UNITY_EDITOR
using UnityEditor;
using System.Text;
#endif
//


namespace DataTransfer
{

    [Serializable]
    public struct DataTransferObject // change name later
    {
        [SerializeField]
        private Result[] results;


        public override string ToString()
        {
            string st = "";

            foreach(var result in results)
            {
                st += result.ToString()+"\n";
            }

            return st;
        }

// remove
#if UNITY_EDITOR
        [MenuItem("Project Tests/CreateTextJson")]
        public static void CreateTestObject()
        {
            var myObj = JsonUtility.FromJson<DataTransferObject>(GetTestJson());
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


        [Serializable]
        public struct Result
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

            public override string ToString()
            {
                return string.Format("id: {0}, vid url: {1}, click url: {2}, track url: {3}", id, video_url, click_url, tracking_url);
            }
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