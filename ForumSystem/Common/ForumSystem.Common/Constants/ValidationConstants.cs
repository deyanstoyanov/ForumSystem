namespace ForumSystem.Common.Constants
{
    public class ValidationConstants
    {
        // Account
        public const int UserNameMinLength = 6;
        public const int UserNameMaxLength = 32;

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;

        public const int EmailMinLength = 6;
        public const int EmailMaxLength = 80;

        public const string EmailRegEx = @"^[A-Za-z0-9]+[\._A-Za-z0-9-]+@([A-Za-z0-9]+[-\.]?[A-Za-z0-9]+)+(\.[A-Za-z0-9]+[-\.]?[A-Za-z0-9]+)*(\.[A-Za-z]{2,})$";

        // User
        public const int OccupationMinLength = 3;
        public const int OccupationMaxLength = 100;

        public const int InterestsMinLength = 3;
        public const int InterestsMaxLength = 500;

        public const int AboutMeMinLength = 6;
        public const int AboutMeMaxLength = 500;

        public const int CountryMinLength = 2;
        public const int CountryMaxLength = 60;

        public const int CityMinLength = 3;
        public const int CityMaxLength = 60;

        public const int SkypeProfileMinLength = 2;
        public const int SkypeProfileMaxLength = 32;

        public const string SkypeProfileRegEx = @"^$|[a-zA-Z][a-zA-Z0-9_\-\,\.]{5,31}";

        public const int WebsiteUrlMinLength = 4;
        public const int WebsiteUrlMaxLength = 100;

        public const string WebsiteUrlRegEx = @"^$|^(http|https)?(:(\/\/|\\\\))?(www\.)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,7}(/\S*)?$";

        public const string GitHubProfileRegEx = @"^$|^(([Hh][Tt][Tt][Pp]|[Hh][Tt][Tt][Pp][Ss])?(:(\/\/|\\\\))?(www\.)?[gG][iI][tT][hH][uU][bB]\.[cC][oO][mM]\/)(.)+$";

        public const string StackOverflowProfileRegEx = @"^$|^((http|https)?(\:\/\/)?(www\.)?stackoverflow.com\/)(.)+$";

        public const string LinkedInProfileRegEx = @"^$|^(([Hh][Tt][Tt][Pp]|[Hh][Tt][Tt][Pp][Ss])?(:(\/\/|\\\\))?(www\.)?([a-zA-Z]+\.)?[lL][iI][nN][kK][eE][dD][iI][nN]\.[cC][oO][mM]\/)(.)+$";

        public const string FacebookProfileRegEx = @"^$|^(([Hh][Tt][Tt][Pp]|[Hh][Tt][Tt][Pp][Ss])?(:(\/\/|\\\\))?(www\.)?[fF][aA][cC][eE][bB][oO][oO][kK]\.[cC][oO][mM]\/)(.)+$";

        public const string TwitterProfileRegEx = @"^$|^(([Hh][Tt][Tt][Pp]|[Hh][Tt][Tt][Pp][Ss])?(:(\/\/|\\\\))?(www\.)?[tT][wW][iI][tT][tT][eE][rR]\.[cC][oO][mM]\/)(.)+$";

        // Section
        public const int SectionTitleMinLength = 2;
        public const int SectionTitleMaxLength = 200;

        // Category
        public const int CategoryTitleMinLength = 3;
        public const int CategoryTitleMaxLength = 200;

        public const int CategoryDescriptionMaxLength = 200;

        // Post
        public const int PostTitleMinLength = 7;
        public const int PostTitleMaxLength = 200;

        public const int PostContentMinLength = 12;
        public const int PostContentMaxLength = 100000;

        public const int PostLockReasonMinLenght = 12;
        public const int PostLockReasonMaxLenght = 100000;

        // Answer
        public const int AnswerContentMinLength = 12;
        public const int AnswerContentMaxLength = 100000;

        // Comment
        public const int CommentContentMinLength = 12;
        public const int CommentContentMaxLength = 100000;

        // Report
        public const int ReportDescriptionMinLength = 12;
        public const int ReportDescriptionMaxLength = 100000;

        // Updates
        public const int UpdateReasonMinLength = 7;
        public const int UpdateReasonMaxLength = 100000;
    }
}