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
    public class BlogWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Blog> Blogs { get; set; }
        public RestCollection<Post> Posts { get; set; }
        public RestCollection<Tag> Tags { get; set; }

        private Post selectedPost;

        public Post SelectedPost
        {
            get { return selectedPost; }
            set
            {
                if (value != null)
                {
                    selectedPost = new Post()
                    {
                        BlogId = value.BlogId,
                        Id = value.Id,
                        Likes = value.Likes,
                        PostContent = value.PostContent,
                        Title = value.Title
                    };

                    DesiredTags = new ObservableCollection<Tag>(Tags.Where(x => x.PostId == selectedPost.Id).ToList());

                    OnPropertyChanged();
                    (DeletePostCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdatePostCommand as RelayCommand).NotifyCanExecuteChanged();
                }

            }
        }

        private Tag selectedTag;

        public Tag SelectedTag
        {
            get { return selectedTag; }
            set
            {
                if (value != null)
                {
                    selectedTag = new Tag()
                    {
                        Id = value.Id,
                        PostId = selectedPost.Id,
                        Name = value.Name
                    };

                    OnPropertyChanged();
                    (DeleteTagCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateTagCommand as RelayCommand).NotifyCanExecuteChanged();
                }

            }
        }
        public ICommand CreateTagCommand { get; set; }

        public ICommand DeleteTagCommand { get; set; }

        public ICommand UpdateTagCommand { get; set; }

        private ObservableCollection<Tag> desiredTags;
        public ObservableCollection<Tag> DesiredTags
        {
            get { return desiredTags; }
            set
            {
                desiredTags = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreatePostCommand { get; set; }

        public ICommand DeletePostCommand { get; set; }

        public ICommand UpdatePostCommand { get; set; }


        private ObservableCollection<Post> desiredPosts;
        public ObservableCollection<Post> DesiredPosts
        {
            get { return desiredPosts; }
            set
            {
                desiredPosts = value;
                OnPropertyChanged();
            }
        }


        private Blog selectedBlog;

        public Blog SelectedBlog
        {
            get { return selectedBlog; }
            set
            {
                if (value != null)
                {
                    selectedBlog = new Blog()
                    {
                        Title = value.Title,
                        ID = value.ID
                    };

                    DesiredPosts = new ObservableCollection<Post>(Posts.Where(x => x.BlogId == selectedBlog.ID).ToList());

                    OnPropertyChanged();
                    (DeleteBlogCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateBlogCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public ICommand CreateBlogCommand { get; set; }

        public ICommand DeleteBlogCommand { get; set; }

        public ICommand UpdateBlogCommand { get; set; }

        public BlogWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Blogs = new RestCollection<Blog>("http://localhost:57125/", "blog", "hub");
                Posts = new RestCollection<Post>("http://localhost:57125/", "post", "hub");
                Tags = new RestCollection<Tag>("http://localhost:57125/", "tag", "hub");

                CreateBlogCommand = new RelayCommand(() =>
                {
                    Blogs.Add(new Blog()
                    {
                        Title = selectedBlog.Title
                    });
                });

                UpdateBlogCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Blogs.Update(selectedBlog);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteBlogCommand = new RelayCommand(() =>
                {
                    Blogs.Delete(selectedBlog.ID);
                },
                () =>
                {
                    return selectedBlog != null;
                });

                CreatePostCommand = new RelayCommand(() =>
                {
                    Posts.Add(new Post()
                    {
                        Title = selectedPost.Title,
                        BlogId = selectedPost.BlogId,
                        Likes = selectedPost.Likes,
                        PostContent = selectedPost.PostContent
                    });
                });

                UpdatePostCommand = new RelayCommand(() =>
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

                DeletePostCommand = new RelayCommand(() =>
                {
                    Posts.Delete(selectedPost.Id);
                },
                () =>
                {
                    return selectedPost != null;
                });


                CreateTagCommand = new RelayCommand(() =>
                {
                    Tags.Add(new Tag()
                    {
                       Name = selectedTag.Name, PostId = selectedTag.PostId
                    });
                });

                UpdateTagCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Tags.Update(selectedTag);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteTagCommand = new RelayCommand(() =>
                {
                    Tags.Delete(selectedTag.Id);
                },
                () =>
                {
                    return selectedTag != null;
                });

                selectedBlog = new Blog();
                selectedPost = new Post();
                selectedTag = new Tag();
                desiredTags = new ObservableCollection<Tag>();
                desiredPosts = new ObservableCollection<Post>();
            }

        }
    }
}
