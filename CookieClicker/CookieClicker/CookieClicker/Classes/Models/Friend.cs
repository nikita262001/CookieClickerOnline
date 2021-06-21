
namespace CookieClicker.Classes.Models
{
    public partial class Friend
    {
        public int IdFriend { get; set; }
        public int Inviting { get; set; }
        public int Invited { get; set; }
        public System.DateTime FriendshipDate { get; set; }
        public bool BeFriends { get; set; }
    }
}