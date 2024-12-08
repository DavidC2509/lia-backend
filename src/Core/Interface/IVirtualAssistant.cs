using Lia.Core.Models.OpenIa;
using Lia.Core.Models.Threads;

namespace Lia.Core.Interface
{
    public interface IVirtualAssistant
    {
        Task<ResponseCreateThreads> CreateThreads(CancellationToken cancellationToken);
        Task<ResponseAddMessageThreads> AddMessageThreads(AddMesaggeThreads data, string threadId, CancellationToken cancellationToken);
        Task<ResponseRunThreads> RunAssistanThreads(string threadId, CancellationToken cancellationToken);
        Task<ResponseRunThreads> RetriveRunAssistanThreads(string threadId, string runId, CancellationToken cancellationToken);
        Task<ResponseGetMessageThreads> GetMessageThread(string threadId, CancellationToken cancellationToken);
        Task<ListFilesOpenIA> GetListFiles(CancellationToken cancellationToken);
        //Task<FileContentResult> GetFileConten(CancellationToken cancellationToken);
        Task<ResponeAddFileOpenIa> AddFileContent(StreamContent file, string fileName, CancellationToken cancellationToken);
        Task<ResponeAddFileOpenIa> DynamycPromt(List<MessagesOutput> data, string filename, CancellationToken cancellationToken);
        Task<ResponseStoreAssistanIa> UpAssistan(RequestUpAssistan data, CancellationToken cancellationToken);

    }
}
