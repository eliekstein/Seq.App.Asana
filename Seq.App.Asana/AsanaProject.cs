namespace Seq.App.Asana
{
    public class AsanaProject : AsanaBaseObject<string>
    {
        public override string id { get; set; }
        public override string name { get; set; }
        public override string endpoint { get { return "projects"; } }
        public override string ToString()
        {
            return id;
        }

    }
}