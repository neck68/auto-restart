using AutoRestarter.Util;

namespace AutoRestarter.Managers;

public class ImageManager
{
    private static readonly Dictionary<string, (double threshold, Rectangle roi, CheckType checkType)> referenceImages = [];

    public static Dictionary<string, (double threshold, Rectangle roi, CheckType checkType)> GetReferenceImages()
    {
        return new Dictionary<string, (double threshold, Rectangle roi, CheckType checkType)>(referenceImages);
    }

    public static void AddReferenceImage(string imageName, double? threshold, Rectangle roi, CheckType checkType)
    {
        double newThreshold = (double)threshold;
        referenceImages[imageName] = (newThreshold, roi, checkType);
    }

    public static void RemoveReferenceImage(string imageName)
    {
        referenceImages.Remove(imageName);
    }

    public static void UpdateReferenceImage(string imageName, double threshold, Rectangle roi, CheckType checkType)
    {
        referenceImages[imageName] = (threshold, roi, checkType);
    }
}
