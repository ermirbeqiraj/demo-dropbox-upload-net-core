using Dropbox.Api;
using Dropbox.Api.Files;
using System.IO;
using System.Threading.Tasks;

namespace DemoDropboxUpload
{
    public class DropBoxService
    {
        private string ApiToken { get; set; }
        public DropBoxService(string token)
        {
            ApiToken = token;
        }

        public async Task Upload(string remoteFileName, string localFilePath)
        {
            using (var dbx = new DropboxClient(this.ApiToken))
            {
                using (var fs = new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
                {
                    await dbx.Files.UploadAsync($"/{remoteFileName}", WriteMode.Overwrite.Instance, body: fs);
                }
            }
        }
    }
}
