using Archilog_Geom.Model;

namespace Archilog_Geom.View
{
    public interface IGraphics
    {
        void InitializeToolBar();
        void RefreshView();
        void RefreshToolBar();
        void OpenRightClickPopUp();
        void OpenCircleEditMenu(Circle c);
        void OpenRectangleEditMenu(Rectangle r);
        void OpenGroupEditMenu(GroupShapes g);
    }
}