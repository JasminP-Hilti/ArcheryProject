namespace ArcheryProject.Models
{
    public class StatisticModel
    {
        public int EventsId { get; set; }
        public int PlayersId { get; set; }
        public int PointsTotal { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Nickname { get; set; }
        public int ParcoursId { get; set; }

        public string? ParcoursName { get; set; }


    }

  
}
