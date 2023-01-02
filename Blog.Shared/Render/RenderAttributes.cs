namespace Blog.Shared.Render;

public class RenderAttributes : RenderBase
{
    /// <summary>
    /// component attributes, eg <code>class: table</code>
    /// </summary>
    public List<KeyValuePair<string, object>>? Attributes { get; set; }

    public RenderAttributes(int sequenceId, List<KeyValuePair<string, object>>? attributes) : base(sequenceId)
    {
        Attributes = attributes;
    }
}