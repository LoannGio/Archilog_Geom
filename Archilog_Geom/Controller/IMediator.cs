using Archilog_Geom.Model;

namespace Archilog_Geom.Controller
{
    public interface IMediator
    {
        void DrawCurrentShape(int x, int y);
        void EraseShape(IShape shape);
        void SaveShapesInToolbar();
        void DeleteCurrentShapeFromToolBar(int i);
        void ShapeEditMenu(IShape shape);
        void CreateGroup(GroupShapes gr);
        void DeleteGroup(GroupShapes gr);
        void CreateMemento();
        void RestoreState(Memento newState);
        void Undo();
        void Redo();
        void Export(string filename);
        void Import(string filename);
        void SaveBeforeAppClosure();
        void ClearDrawingPanel();
    }
}
