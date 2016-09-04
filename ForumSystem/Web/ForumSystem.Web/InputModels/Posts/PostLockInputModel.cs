namespace ForumSystem.Web.InputModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using ForumSystem.Common.Constants;

    public class PostLockInputModel
    {
        public int PostId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(ValidationConstants.PostLockReasonMaxLenght, 
            MinimumLength = ValidationConstants.PostLockReasonMinLenght, 
            ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string LockReason { get; set; }
    }
}