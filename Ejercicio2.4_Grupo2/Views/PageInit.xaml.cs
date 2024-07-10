using System;
using System.Threading.Tasks;

namespace Ejercicio2._4_Grupo2.Views;

public partial class PageInit : ContentPage
{
    Controllers.VideoController controller;
    public PageInit()
	{
		InitializeComponent();
        controller = new Controllers.VideoController();
    }

    private async void OnRecordVideoButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var video = await MediaPicker.CaptureVideoAsync();

            if (video != null)
            {
                var newFile = Path.Combine(FileSystem.AppDataDirectory, video.FileName);

                using (var stream = await video.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }

                var videoRecord = new Models.Video
                {
                    VideoData = newFile
                };

                if (await controller.Store(videoRecord) > 0)
                {
                    await DisplayAlert("Success", "Video saved successfully!", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to save video", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private async void OnPlayVideoButtonClicked(object sender, EventArgs e)
    {
        var videos = await controller.GetList();
        if (videos.Count > 0)
        {
            var latestVideo = videos[videos.Count - 1];
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(latestVideo.VideoData)
            });
        }
        else
        {
            await DisplayAlert("No Videos", "No videos available to play", "OK");
        }
    }

    private async void OnDownloadVideoButtonClicked(object sender, EventArgs e)
    {
        var videos = await controller.GetList();
        if (videos.Count > 0)
        {
            var latestVideo = videos[videos.Count - 1];
            var destinationPath = Path.Combine(FileSystem.CacheDirectory, Path.GetFileName(latestVideo.VideoData));

            File.Copy(latestVideo.VideoData, destinationPath, true);

            await DisplayAlert("Success", $"Video downloaded to: {destinationPath}", "OK");

            // Open the file using the default file viewer
            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(destinationPath)
            });
        }
        else
        {
            await DisplayAlert("No Videos", "No videos available to download", "OK");
        }
    }

}