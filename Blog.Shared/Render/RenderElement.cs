namespace Blog.Shared.Render;

public class RenderElement : RenderBase
{
    /// <summary>
    /// component element, eg <code>h2</code>
    /// </summary>
    public string Element { get; set; }

    public RenderElement(int sequenceId, string element) : base(sequenceId)
    {
        Element = element;
    }
}