namespace Blog.Shared.Render;

public interface IRenderBase
{
    /// <summary>
    /// indicate the id of render fragment
    /// </summary>
    public int SequenceId { get; set; }
}