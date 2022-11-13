using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LetsChatApplication.Model
{
    public class MessageInfo
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MsgId { get; set; }
        [StringLength(350)]
        public string text { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
