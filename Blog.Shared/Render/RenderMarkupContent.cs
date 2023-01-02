namespace Blog.Shared.Render;

public class RenderMarkupContent : IRenderBase
{
    /// <summary>
    /// component markup content, eg <c>content of a label</c>
    /// NOTE: this can only be a property of Text Node, which is the leaf node of render tree
    /// </summary>
    public string? MarkupContent { get; set; }
    public int SequenceId { get; set; }


    public RenderMarkupContent(int sequenceId, string? markupContent)
    {
        SequenceId = sequenceId;
        MarkupContent = markupContent;
    }

}