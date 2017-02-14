using Seq.Apps;
using Seq.Apps.LogEvents;
using System;
using System.Text.RegularExpressions;

namespace Seq.App.Asana
{
    public class AsanaReactor : Reactor, ISubscribeTo<LogEventData>
    {
        private static readonly Regex PlaceholdersRegex = new Regex("(\\[(?<key>[^\\[\\]]+?)(\\:(?<format>[^\\[\\]]+?))?\\])", RegexOptions.CultureInvariant | RegexOptions.Compiled);
       
        #region Public Properties

        [SeqAppSetting(
            DisplayName = "Personal Access Token",
            HelpText = "Used for authenticating with asana. go here to find out how to get a Personal Access Token",
        IsOptional = false)]
        public string AccessToken { get; set; }

        [SeqAppSetting(
            DisplayName = "Workspace",
            HelpText = "The workspace ID.",
            IsOptional =false)]
        public string Workspace { get; set; }

        [SeqAppSetting(
            DisplayName = "Project",
            HelpText = "The project to be used for the asana task.",
            IsOptional = true)]
        public string Project { get; set; }

        [SeqAppSetting(
            DisplayName = "Assignee",
            IsOptional = true,
            HelpText = "The username that Seq uses when posting to Slack. If not specified, uses the Webhook default. Username can also be a PropertyKey in the format [PropertyKey].")]
        public string Assignee { get; set; }

        [SeqAppSetting(
            DisplayName = "Suppression time (minutes)",
            IsOptional = true,
            HelpText = "Once an event type has been sent to Slack, the time to wait before sending again. The default is zero.")]
        public int SuppressionMinutes { get; set; } = 0;

        [SeqAppSetting(
            DisplayName = "Exclude Properties",
            IsOptional = true,
            HelpText = "Should the event include the property information as attachments to the message. The default is to include")]
        public bool ExcludePropertyInformation { get; set; }

        [SeqAppSetting(
            HelpText = "The message template to use when writing the message to Slack. Refer to https://api.slack.com/docs/formatting for formatting options. Event property values can be added in the format [PropertyKey]. The default is \"[RenderedMessage]\"",
            IsOptional = true)]
        public string MessageTemplate { get; set; }

        [SeqAppSetting(
            HelpText = "The image to show in the room for the message. The default is https://getseq.net/images/nuget/seq.png",
            IsOptional = true)]
        public string IconUrl { get; set; }

        #endregion

        public void On(Event<LogEventData> evt)
        {
            throw new NotImplementedException();
        }
    }
}
