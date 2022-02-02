using UnityEngine;

public interface IDraggable 
{
    void onStartDrag();
    void onEndDrag();
    bool InDragAnchor(Vector2 vector2);
}
