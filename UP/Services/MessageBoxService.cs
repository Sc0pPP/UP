namespace UP.Services;

public class MessageBoxService
{
    public static void MessageBoxShow(string message)
    {
        WpfLikeAvaloniaMessageBox.MessageBox.Show(message);
    }
    
}