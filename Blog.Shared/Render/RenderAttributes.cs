namespace Blog.Shared.Render;

public class RenderAttributes : IRenderBase
{
    /// <summary>
    /// component attributes, eg <code>class: table</code>
    /// </summary>
    public List<KeyValuePair<string, object>>? Attributes { get; set; }
    public int SequenceId { get; set; }

    public RenderAttributes(int sequenceId, List<KeyValuePair<string, object>>? attributes)
    {
        SequenceId = sequenceId;
        Attributes = attributes;
    }

}