// remove
#if UNITY_EDITOR
using UnityEditor;
#endif
// remove

namespace CrossPromo
{

    public class CrossPromoController
    {
        // remove
        private static CrossPromoController instance;
        //

        private readonly CrossPromoView view;

        public CrossPromoController(CrossPromoView view)
        {
            this.view = view;
            // remove
            instance = this;
            //
        }

        public void Next()
        {

        }

        public void Pevious()
        {

        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }



        private void LoadVideoAtURL(string url)
        {
            view.LoadVideoAtURL(url);
        }

        private void PlayVideo()
        {
            view.PlayVideo();
        }

// remove
#if UNITY_EDITOR
        [MenuItem("Project Tests/play video")]
        public static void CreateTestObject()
        {
            string testVid = "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4";
            instance.LoadVideoAtURL(testVid);
            instance.PlayVideo();
        }
#endif
//
    }
}