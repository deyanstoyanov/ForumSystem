# ForumSystem
Forum system written in ASP.NET MVC 5. Along with MVC it’s built using Entity Framework Code First and is easy to extend and add your own features.

## Live Demo

Check a live demo here http://demo-forumsystem.apphb.com

## Features

#### Sections

- Sections are organizational containers which house the individual categories
    
#### Categories

  - Last Post - list who made the last post in a particular category and when it was made.
  - Number of posts in a particular category.
  - Number of replies (answers and comments) in a particular category.
  
#### Posts

  - Last activity - list who made the last answer or comment in a particular post and when it was made.
  - Number of views for a particular post.
  - Number of likes for a particular post.
  - Number of reports for a particular post.

#### Posts, Answers and Comments

  - TinyMCE editor
  - Supports HTML
  -	Emoticons
  - Attachment images (.gif, .jpg., etc.)
  -	Attachment video
  -	Preview option

#### Users
  
  - Custom Avatars - specify a URL.
  - Custom Profile - every user gets a profile that they can customize with their personal information.
  - List user's forum activity (posts, answers, comments).
  - List user's notifications.
  
#### Likes

  -	Allows users to like posts, answers and comments.

#### Reports

  - Report system for posts, answers and comments.
  
#### Notifications

  - Real Time Notifications using SignalR.
  
#### Moderation
  - Reports - moderators have quick access to a list of posts, answers or comments which users have reported, and can deal with it efficiently.
    
  - Move Post - moderators have the ability to move post to a new category.
    
  - Lock Post - individual posts can be locked so that regular users can no longer reply.
    
  - Pin Post - moderators can pin a post to the top of the page.
    
  - Post, Answer and Comment Editing - moderators can edit the contents of a user's post, answer and comment.
    
  - Delete Post, Answer and Comment - moderators can delete a post, answer and comment.
    
#### Administration

  - Manage all data - administrators can add, edit, move, delete all data.
      
  - Administrators have access to Moderation area.
  
  - Manage users and their roles - administrators can add user in roles and remove user from roles.
    
#### Security Verification During Registration
  - [reCAPTCHA](https://www.google.com/recaptcha) by Google
