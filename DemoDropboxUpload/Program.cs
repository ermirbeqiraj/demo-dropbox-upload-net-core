using System.IO;
using System.Threading.Tasks;

namespace DemoDropboxUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            var dropboxApiToken = "<DROPBOX_TOKEN_HERE>";
            var bakupFolderPath = "<YOUR ABSOLUTE PATH TO THE BACKUP FOLDER IN HERE>";
            try
            {
                var dateNowString = System.DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                var backupName = $"<YOUR_DB_NAME>_db_bak{dateNowString}.sql.gz";
                var localFilePath = Path.Combine(bakupFolderPath, backupName);

                if (File.Exists(localFilePath))
                {
                    var dbs = new DropBoxService(dropboxApiToken);
                    Task.Run(async () =>
                    {
                        await dbs.Upload(backupName, localFilePath);
                    }).Wait();
                    File.Delete(localFilePath);
                }
                else
                {
                    // WARN
                }
            }
            catch (System.Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                // WARN
            }
        }
    }
}
