namespace PromoPanel
{

    public class CrossPromoController
    {

        private readonly CrossPromoView view;

        public CrossPromoController(CrossPromoView view)
        {
            this.view = view;
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
    }
}