namespace Seq.App.Asana
{
    public class AsanaUser : AsanaBaseObject<string>
    {
        public override string id { get; set; }
        public override string endpoint { get { return "users"; } }
        public override string name { get; set; }
        public string email { get; set; }
    }
}
