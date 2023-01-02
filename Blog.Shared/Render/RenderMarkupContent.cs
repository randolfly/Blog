namespace Blog.Shared.Render;

public class RenderMarkupContent : RenderBase
{
    /// <summary>
    /// component markup content, eg <c>content of a label</c>
    /// </summary>
    public string? MarkupContent { get; set; }

    public RenderMarkupContent(int sequenceId, string? markupContent) : base(sequenceId)
    {
        MarkupContent = markupContent;
    }
}