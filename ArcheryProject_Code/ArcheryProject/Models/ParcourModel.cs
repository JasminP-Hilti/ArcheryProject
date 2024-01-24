namespace ArcheryProject.Models
{
    public class ParcourModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Location { get; set; }

        public int CountAnimals { get; set; }
        public List<ParcourModel> AvailableParcours { get; set; }

        public string SelectedParcours { get; set; }
    }
}
