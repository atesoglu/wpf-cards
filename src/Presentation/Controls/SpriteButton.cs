using System.Windows.Controls;
using Presentation.Models;

namespace Presentation.Controls
{
    public class SpriteButton : Button
    {
        public PostObjectModel Post { get; }
        public bool IsDisplayingUser { get; set; }

        public SpriteButton(PostObjectModel post)
        {
            Post = post;
        }
    }
}