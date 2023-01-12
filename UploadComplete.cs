namespace PolymorphicJson
{
    public class UploadComplete : TriggerDetails
    {
        public int UploadBeginId { get; set; }

        public DateTime TimeOfFinishArrival { get; set; }

        public UploadComplete()
        {
        }
    }
}