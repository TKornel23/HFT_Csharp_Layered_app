using HSTUTU_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HSTUTU_HFT_2021221.WpfClient
{
    public class PostWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Post> Posts { get; set; }


        private Post selectedPost;

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public Post SelectedPost
        {
            get { return selectedPost; }
            set
            {
                if (value != null)
                {
                    selectedPost = new Post()
                    {
                        Title = value.Title,
                        BlogId = value.BlogId,
                        Id = value.Id,
                        Likes = value.Likes,
                        PostContent = value.PostContent
                    };

                    OnPropertyChanged();
                    (DeleteBlogCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateBlogCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateBlogCommand { get; set; }

        public ICommand DeleteBlogCommand { get; set; }

        public ICommand UpdateBlogCommand { get; set; }

        public PostWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Posts = new RestCollection<Post>("http://localhost:57125/", "post", "hub");
                CreateBlogCommand = new RelayCommand(() =>
                {
                    Posts.Add(new Post()
                    {
                        Title = selectedPost.Title,
                        BlogId = selectedPost.BlogId,
                        Likes = selectedPost.Likes,
                        PostContent = selectedPost.PostContent,
                    });
                });

                UpdateBlogCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Posts.Update(selectedPost);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteBlogCommand = new RelayCommand(() =>
                {
                    Posts.Delete(selectedPost.Id);
                },
                () =>
                {
                    return selectedPost != null;
                });
                selectedPost = new Post();
            }

        }
    }
}