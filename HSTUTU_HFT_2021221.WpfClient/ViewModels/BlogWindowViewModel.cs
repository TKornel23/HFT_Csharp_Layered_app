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


        private Blog selectedBlog;

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

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

                    OnPropertyChanged();
                    (DeleteBlogCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateBlogCommand as RelayCommand).NotifyCanExecuteChanged();
                }
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
                selectedBlog = new Blog();
            }

        }
    }
}
