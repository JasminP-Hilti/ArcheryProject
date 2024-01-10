namespace ArcheryProject.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Nickname { get; set; }

        public ulong Admin { get; set; }

        public int? LoginsId { get; set; }
    }
}
