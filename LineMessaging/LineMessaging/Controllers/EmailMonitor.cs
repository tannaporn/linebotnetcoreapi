using crypto;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

public class EmailMonitor : IDisposable
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _username;
    private readonly string _password;
    private readonly Timer _timer;
    private ImapClient _client;
    private bool _isConnected;

    public event EventHandler<EmailEventArgs> NewEmailReceived;

    public EmailMonitor(string host, int port, string username, string password)
    {
        _host = "imap.gmail.com";
        _port = 993;
        _username = "tanaporn.fern1811@gmail.com";
        _password = "khxohkrkitrkyrd";
        _timer = new Timer(CheckEmails, null, Timeout.Infinite, Timeout.Infinite);
    }

    public async Task StartMonitoring(int checkIntervalInSeconds = 60)
    {
        try
        {
            await ConnectToEmail();
            _timer.Change(0, checkIntervalInSeconds * 1000);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to start email monitoring", ex);
        }
    }

    private async Task ConnectToEmail()
    {
        try
        {
            #region
            //IMAP(Internet Message Access Protocol) is a widely used email protocol that allows clients to access and manage email messages stored on a remote email server
            // The main steps in the IMAP workflow are:

            //The client connects to the IMAP server, typically using SSL/ TLS for security.
            //The client authenticates with the server using a username and password.
            //The client selects a mailbox(e.g., Inbox, Sent, Drafts) to access.
            //The client can then perform various operations on the emails in that mailbox, such as:

            //Fetching email headers, bodies, and attachments
            //Marking emails as read, unread, or flagged
            //Moving or copying emails between folders
            //Searching for emails based on criteria
            //Changes made by the client are synchronized with the server, ensuring consistent email state across devices.
            #endregion
          
            CancellationToken cancellationToken = default;
            _client = new ImapClient();
            await _client.ConnectAsync("imap.gmail.com", 993, true, cancellationToken);
            await _client.AuthenticateAsync("tanaporn.fern1811@gmail.com", "gkhxohkrkitrkyrd", cancellationToken);
            _isConnected = true;
        }
        catch (Exception ex)
        {

        }
    }

    private async void CheckEmails(object state)
    {
        try
        {
            if (!_isConnected)
            {
                await ConnectToEmail();
            }

            var inbox = _client.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadOnly);

            // Get the current message count
            int messageCount = inbox.Count;

            // Get the most recent message
            if (messageCount > 0)
            {
                var message = await inbox.GetMessageAsync(messageCount - 1);
                NewEmailReceived?.Invoke(this, new EmailEventArgs
                {
                    Subject = message.Subject,
                    From = message.From.ToString(),
                    Date = message.Date.DateTime
                });
            }
        }
        catch (Exception ex)
        {
            _isConnected = false;
            // Handle or log the error appropriately
            Console.WriteLine($"Error checking emails: {ex.Message}");
        }
    }

    public void StopMonitoring()
    {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
        _client?.Disconnect(true);
        _isConnected = false;
    }

    public void Dispose()
    {
        StopMonitoring();
        _timer?.Dispose();
        _client?.Dispose();
    }
}

public class EmailEventArgs : EventArgs
{
    public string Subject { get; set; }
    public string From { get; set; }
    public DateTime Date { get; set; }
}