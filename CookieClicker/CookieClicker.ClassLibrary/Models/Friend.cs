using System.ComponentModel.DataAnnotations;

namespace CookieClicker.ClassLibrary.Models
{
    public partial class Friend
    {
        [Key]
        public int IdFriend { get; set; }
        public int Inviting { get; set; }
        public int Invited { get; set; }
        public System.DateTime FriendshipDate { get; set; }
        public bool BeFriends { get; set; }
    }
}