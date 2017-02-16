using Seq.Apps;
using Seq.Apps.LogEvents;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;

namespace Seq.App.Asana
{
    [SeqApp("Asana Task Creator", 
        Description = "Creates tasks for matching events.")]
    public class AsanaReactor : Reactor, ISubscribeTo<LogEventData>
    {
        private static readonly Regex PlaceholdersRegex = new Regex("(\\[(?<key>[^\\[\\]]+?)(\\:(?<format>[^\\[\\]]+?))?\\])", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private readonly ConcurrentDictionary<uint, DateTime> _lastSeen = new ConcurrentDictionary<uint, DateTime>();

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
            HelpText = "The user to get this task assigned to.")]
        public string Assignee { get; set; }

        [SeqAppSetting(
            DisplayName = "Suppression time (minutes)",
            IsOptional = true,
            HelpText = "Once an event type has been sent to asana, the time to wait before sending again. The default is zero.")]
        public int SuppressionMinutes { get; set; } = 0;

        //[SeqAppSetting(
        //    DisplayName = nameof(TaskName),
        //    IsOptional = true,
        //    HelpText = "The name of the task to be created, by default will be the first 30 characters of the rendered message.")]
        //public string TaskName { get; set; }

        #endregion

        public void On(Event<LogEventData> evt)
        {
            bool added = false;
            var lastSeen = _lastSeen.GetOrAdd(evt.EventType, k => { added = true; return DateTime.UtcNow; });
            if (!added)
            {
                if (lastSeen > DateTime.UtcNow.AddMinutes(-SuppressionMinutes)) return;
                _lastSeen[evt.EventType] = DateTime.UtcNow;
            }

            //get authentication
            var auth = new Authentication(AccessToken);
            //get workspace
            var workspace = AsanaWorkspace.Retreive<AsanaWorkspace>(Workspace,auth);
            //get project
            AsanaProject project = null;
            if (!string.IsNullOrEmpty(Project))
                project = AsanaProject.Retreive<AsanaProject>(Project, auth);
            //get assignee
            AsanaUser assignee = null;
            if(!string.IsNullOrEmpty(Assignee))
                assignee = AsanaUser.Retreive<AsanaUser>(Assignee, auth);

            var task = new AsanaTask
            {
                workspace = workspace,
                projects = project != null ? new[] { project } : null,
                assignee = assignee ?? null,
                name = evt.Data.RenderedMessage,
                notes = renderProperties(evt),
            };

            task.Create(auth);
        }

        private string renderProperties(Event<LogEventData> evt)
        {
            var props = evt.Data.Properties
                .Select(s => string.Format("{0}: {1}", s.Key, s.Value));

            return string.Join("\n", props);
        }
    }
}
