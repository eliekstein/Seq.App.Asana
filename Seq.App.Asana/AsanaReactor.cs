using Seq.Apps;
using Seq.Apps.LogEvents;
using System;
using System.Text.RegularExpressions;

namespace Seq.App.Asana
{
    [SeqApp("Asana Task Creator", 
        Description = "Creates tasks for matching events.")]
    public class AsanaReactor : Reactor, ISubscribeTo<LogEventData>
    {
        private static readonly Regex PlaceholdersRegex = new Regex("(\\[(?<key>[^\\[\\]]+?)(\\:(?<format>[^\\[\\]]+?))?\\])", RegexOptions.CultureInvariant | RegexOptions.Compiled);
       
        #region Public Properties

        [SeqAppSetting(
            DisplayName = "Personal Access Token",
            InputType = SettingInputType.Password,
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
            HelpText = "Once an event type has been sent to asana, the time to wait before sending again. The default is zero.")]
        public int SuppressionMinutes { get; set; } = 0;

        [SeqAppSetting(
            DisplayName = "Exclude Properties",
            IsOptional = true,
            HelpText = "Should the event include the property information as attachments to the message. The default is to include")]
        public bool ExcludePropertyInformation { get; set; }

        #endregion

        public void On(Event<LogEventData> evt)
        {
            throw new NotImplementedException();
        }
    }
}
