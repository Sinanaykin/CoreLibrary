using Hangfire.Web.Services;

namespace Hangfire.Web.BackgroundJob
{
    public class FireAndForgetJob
    {

        public static void EmailSendJobToUser(string userId, string message)
            //statik yapıyoruz çünkü statik metodlara direk class üzerinden erişebiliyoruz
        {
            Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(userId, message));//tek sefer job çalıştırmak istediğimizde Enqueue yu kullanırız

        }
    }
}
