namespace Project1
{
    public class Star
    {
        public string Name { get; set; } = null;
        public DateTime? Dob { get; set; }
        public string? Description { get; set; }
        public bool? Male { get; set; }
        public string? Nationnality { get; set; }

        public Star(string name, DateTime? dob, string? description, bool? male, string? nationality)
        {
            this.Name = name;
            this.Dob = dob;
            this.Description = description;
            this.Male = male;
            this.Nationnality = nationality;
        }

        public Star()
        {

        }

        public void Clear()
        {
            Name = null;
            Dob = null; Description = null; Male = null; Nationnality = null;
        }

        public override string ToString()
        {
            return $"{Name} {Dob} {Description} {Male} {Nationnality}";
        }
    }
}
