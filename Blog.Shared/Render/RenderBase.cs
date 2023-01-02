namespace Blog.Shared.Render; 

public class RenderBase {
    /// <summary>
    /// indicate the id of render fragment
    /// </summary>
    public int SequenceId { get; set; } = 0;

    protected RenderBase(int sequenceId)
    {
        SequenceId = sequenceId;
    }
}
