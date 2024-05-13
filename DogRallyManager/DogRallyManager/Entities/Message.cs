using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogRallyManager.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(750)]
        public string MessageBody { get; set; }

        [ForeignKey("UserSenderId")]
        public RallyUser Sender { get; set; }

        //[ForeignKey("ChatRoomId")]
        public ChatRoom RecipientChatRoom { get; set; }
    }
}
