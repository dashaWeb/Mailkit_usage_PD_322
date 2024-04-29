using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using Org.BouncyCastle.Crypto;
using System.Text;


internal class Program
{
    const string username = "dashakonopelko@gmail.com";
    const string password = "auru gqhx hqmg hmop";
    private static void Main(string[] args)
    {

        //Send Mails(SMTP)
        /*var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Dasha", username));
        message.To.Add(new MailboxAddress("Test user", "tokiti6835@picdv.com"));
        message.Subject = "Добрий вечір, ми з України! How you doin?";
        message.Importance = MessageImportance.High;
        message.Body = new TextPart()
        {
            Text = @"Hey Alice 
            orem ipsum dolor sit amet, consectetur adipiscing elit. Sed elementum, massa in efficitur volutpat, est neque vulputate arcu, ut vestibulum dui orci sit amet mi. Donec venenatis leo eget nibh maximus, at vulputate risus varius. Phasellus a lacus finibus, venenatis dui mollis, ultricies felis. Maecenas felis diam, iaculis ac mauris quis, molestie laoreet dui. Quisque ut justo sed ante posuere aliquam. Nulla viverra, ligula non tempus tristique, tellus nisl ornare ex, sit amet feugiat eros nibh ut dui. In posuere ante ac tellus eleifend tristique. Sed ut est quis elit vestibulum malesuada non vitae massa. Vivamus velit ligula, pulvinar nec enim sed, auctor mattis arcu. Sed eu elit dolor. Aliquam tortor dolor, aliquam eu nunc id, euismod consequat ex.

            Vivamus placerat diam elit, a scelerisque diam varius imperdiet. Curabitur fringilla quis dolor ut cursus. Cras ac tellus massa. Quisque maximus auctor orci nec varius. Maecenas in lectus sed justo efficitur aliquet. Curabitur et lorem vitae lectus posuere suscipit at vitae tortor. Integer ac ipsum suscipit, mollis leo sed, eleifend odio. Sed suscipit augue et est pharetra pretium. Suspendisse potenti. Morbi sed dui aliquet, vehicula elit ut, vestibulum odio. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Curabitur eget quam purus. Nullam cursus leo id ex porta rutrum.
                

            ---Bob
            "
        };

        using(var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            client.Authenticate(username, password);
            client.Send(message);
        }*/


        //Get Mails Imap
        Console.OutputEncoding = Encoding.UTF8;
        using (var client = new ImapClient())
        {

            client.Connect("imap.gmail.com", 993, MailKit.Security.SecureSocketOptions.SslOnConnect);
            client.Authenticate(username, password);

            var folders = client.GetFolders(client.PersonalNamespaces[0]);
            foreach (var folder in folders)
            {
                Console.WriteLine("Folder : " + folder.Name);
            }


            //var folder_ = client.GetFolder(MailKit.SpecialFolder.Sent);
            //folder_.Open(MailKit.FolderAccess.ReadOnly);
            //var id = folder_.Search(SearchQuery.All);

            //foreach (var item in id)
            //{
            //    var mail = folder_.GetMessage(item);
            //    Console.WriteLine(mail.Date + " " + mail.Subject);
            //}

            var folder_ = client.GetFolder(MailKit.SpecialFolder.Sent);
            //folder_.Open(MailKit.FolderAccess.ReadOnly);
            folder_.Open(MailKit.FolderAccess.ReadWrite);
            var id = folder_.Search(SearchQuery.All).LastOrDefault();

            var mail = folder_.GetMessage(id);
            Console.WriteLine(mail.Date + " " + mail.Subject + "\n" + mail.TextBody);

            // Deleted 
            //folder_.MoveTo(id,client.GetFolder(MailKit.SpecialFolder.Trash));
            folder_.AddFlags(id, MessageFlags.Deleted, true);
            folder_.Expunge();
        }
    }
}