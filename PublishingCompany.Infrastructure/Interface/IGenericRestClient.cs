using System.Threading.Tasks;

namespace PublishingCompany.Infrastructure.Interface
{
    public interface IGenericRestClient
    {
        Task<T> Get<T>(string apiUrl) where T : class;
        Task<T> PostRequest<T>(string apiUrl, T postObject) where T : class;
        Task<T> PutRequest<T>(string apiUrl, T putObject);
    }
}