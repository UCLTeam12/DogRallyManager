using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogRallyManager.Entities
{
    public class ChatRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? RoomName { get; set; }
        public ICollection<Message> ChatMessages { get; set; } = new List<Message>();

        public int NumberOfAssociatedUsers { get; set; }

        [ForeignKey("UserId")]
        public ICollection<RallyUser> ParticipatingUsers { get; set; } = new List<RallyUser>();
    }
}
