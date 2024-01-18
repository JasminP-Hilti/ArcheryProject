using ArcheryProject.Models;

namespace ArcheryProject
{
    public class ApiHelper
    {
        static Dictionary<string, PlayerModel> users = new Dictionary<string, PlayerModel>();

        static public void SetUser(string name, PlayerModel model)
        {
            if(!users.ContainsKey(name))
                users.Add(name, model);

        }

        static public PlayerModel GetUser(string name)
        {
            if(name != null)
            {
                if (users.ContainsKey(name))
                {
                PlayerModel tmpPlayer = users[name];

                //tmpPlayer.PlayedEvents = new List<artaimusDBlib.EventsHasPlayer> { };
                //tmpPlayer.Parcours = new List<artaimusDBlib.Parcour>();
                //tmpPlayer.Events = new List<artaimusDBlib.Event> { };

                return users[name];
                } 
            }
        
            

            return null;
        }

        static public void RemoveUser(PlayerModel player)
        {
            //hässlich dafuq
            if (player != null)
            {
                if (player.Nickname != null)
                {
                    if (users.ContainsKey(player.Nickname))
                    {
                        users.Remove(player.Nickname);
                    }
                }
                if (player.Email != null)
                {
                    if (users.ContainsKey(player.Email))
                    {
                        users.Remove(player.Email);
                    }
                }
            }
        }
    }
}
