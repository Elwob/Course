namespace Logic
{
    public class MainController
    {
        private DocumentController documentController = DocumentController.GetInstance();

        public void AddToDatabase()
        {
            documentController.AddDataToDatabase();
        }
    }
}