using HSTUTU_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HSTUTU_HFT_2021221.WpfClient.ViewModels
{
    public class TagWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Tag> Tags { get; set; }


        private Tag selectedTag;

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

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
                        Name = value.Name,
                        Post = value.Post,
                        PostId = value.PostId
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

        public TagWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Tags = new RestCollection<Tag>("http://localhost:57125/", "tag", "hub");
                CreateBlogCommand = new RelayCommand(() =>
                {
                    Tags.Add(new Tag()
                    {
                        Name = selectedTag.Name,
                        Post = selectedTag.Post,
                        PostId = selectedTag.PostId
                    });
                });

                UpdateBlogCommand = new RelayCommand(() =>
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

                DeleteBlogCommand = new RelayCommand(() =>
                {
                    Tags.Delete(selectedTag.Id);
                },
                () =>
                {
                    return selectedTag != null;
                });
                selectedTag = new Tag();
            }

        }
    }
}