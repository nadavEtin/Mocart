using System.Threading.Tasks;

public interface IWebRequest
{
    Task FetchDataAsync(string url);
}