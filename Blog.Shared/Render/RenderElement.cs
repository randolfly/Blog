namespace Blog.Shared.Render;

public class RenderElement : IRenderBase
{
    /// <summary>
    /// component element, eg <code>h2</code>
    /// pure text element will be convert to element "#text"
    /// </summary>
    public string Element { get; set; }
    public int SequenceId { get; set; }


    public RenderElement(int sequenceId, string element)
    {
        SequenceId = sequenceId;
        Element = element;
    }

}