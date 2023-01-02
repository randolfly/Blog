namespace Blog.Shared.Render;

public class ComponentRenderItem
{
    public RenderElement RenderElement { get; set; }
    public RenderAttributes? RenderAttributes { get; set; }
    /// <summary>
    /// if a componentRenderItem has markupContent, it doesnt have contentItems
    /// </summary>
    public RenderMarkupContent? RenderMarkupContent { get; set; }
    public List<ComponentRenderItem?> ContentItems { get; set; } = new();
}