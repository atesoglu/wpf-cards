using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Presentation.Controls;
using Presentation.Models;
using RestSharp;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StageMainGrid();
            Loaded += OnLoaded;
        }

        private void StageMainGrid()
        {
            MainGrid.ShowGridLines = true;
            for (var i = 0; i < 10; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition { });
                MainGrid.RowDefinitions.Add(new RowDefinition { });
            }
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("posts");
            var response = await client.ExecuteAsync<List<PostObjectModel>>(request);

            if (response.StatusCode == HttpStatusCode.OK && response.Data != null)
                AddControls(response.Data);
        }

        private void AddControls(List<PostObjectModel> posts)
        {
            var index = 0;
            posts?.ForEach(post =>
            {
                var sprite = new SpriteButton(post) { Name = $"Sprite{post.Id}", Content = post.Id.ToString(), VerticalAlignment = VerticalAlignment.Stretch, HorizontalAlignment = HorizontalAlignment.Stretch };
                sprite.Click += SpriteOnClick;

                Grid.SetColumn(sprite, index % 10);
                Grid.SetRow(sprite, index / 10);

                MainGrid.Children.Add(sprite);

                index++;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpriteOnClick(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.OfType<SpriteButton>().ToList().ForEach(sprite =>
            {
                sprite.Content = sprite.IsDisplayingUser ? sprite.Post.Id.ToString() : sprite.Post.UserId.ToString();
                sprite.IsDisplayingUser = !sprite.IsDisplayingUser;
            });
        }
    }
}