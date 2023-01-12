namespace PolymorphicJson
{
    public class UploadBegin : TriggerDetails
    {
        public string FileName { get; set; }

        public DateTime TimeOfBeginArrival { get; set; }

        public UploadBegin()
        {
            FileName = string.Empty;
        }
    }
}