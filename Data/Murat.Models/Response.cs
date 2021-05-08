namespace Murat.Models
{
    public class Response
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public class ResponseSql
    {
        public int ID_ERR { get; set; }
        public string DESCR_ERR { get; set; }
        public string IDDATO { get; set; }
    }
}
