namespace BookManagement.Models.ApiModels
{
    public class ResponseModel
    {
        public object Data {  get; set; }
        public bool IsSuccess {  get; set; }
        public string Message {  get; set; }
    }
}
