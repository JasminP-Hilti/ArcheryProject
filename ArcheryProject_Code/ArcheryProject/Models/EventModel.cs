using artaimusDBlib;

namespace ArcheryProject.Models
{
    public class EventModel
    {
        public static Dictionary<CountType, string> countTypeNames =  new Dictionary<CountType, string>
        {
            { CountType.PfeilWertung3, "PfeilWertung3" },
            { CountType.PfeilWertung2, "PfeilWertung2" }
        };

        public static List<Player> tmpPlayerList = new List<Player> {  }

        public enum CountType { PfeilWertung3, PfeilWertung2 }

        public int? Id { get; set; }

        public int? ParcoursId { get; set; }

        public CountType CountTypeValue { get; set; } = CountType.PfeilWertung3;

        
    }
}
