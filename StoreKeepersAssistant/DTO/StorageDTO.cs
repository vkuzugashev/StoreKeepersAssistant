namespace StoreKeepersAssistant.ViewModels
{
    public class StorageDTO
    {
        private int _i;
        public StorageDTO() { }
        public StorageDTO(int i) => _i = i;
        public string Id { get; set; }
        public string Name { get; set; }
    }
}