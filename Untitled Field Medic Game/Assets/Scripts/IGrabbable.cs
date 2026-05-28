public interface IGrabbable
{
    bool IsGrabbable { get; }
    void OnGrabbed();
    void OnDropped();
}