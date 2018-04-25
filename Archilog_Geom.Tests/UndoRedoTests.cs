using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Archilog_Geom.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Archilog_Geom.Tests
{
    [TestClass]
    public class UndoRedoTests
    {
        private Memento reference;
        private Memento tmpReference;
        private IShape group;

        [TestInitialize]
        public void SetUp()
        {
            // Reference case instantiation
            IShape circle = new Circle();
            IShape rect = new Rectangle();

            IGraphics ig = new CsGraphics();
            Mediator m = Mediator.Instance;
            m.GetType().GetField("g", BindingFlags.NonPublic| BindingFlags.Instance | BindingFlags.Static).SetValue(m, ig);
            Mediator.ToolBar._toolBarShapes.Add(circle);
            Mediator.ToolBar._toolBarShapes.Add(rect);
            Mediator.Instance.LoadCurrentShape(0);
            Mediator.Instance.DrawCurrentShape(50, 50);
            Mediator.Instance.LoadCurrentShape(1);
            Mediator.Instance.DrawCurrentShape(100, 100);
            Mediator.Instance.LoadCurrentShape(0);
            Mediator.Instance.DrawCurrentShape(150, 150);
            Mediator.Instance.LoadCurrentShape(1);
            Mediator.Instance.DrawCurrentShape(200, 200);
            Mediator.Instance.AddRemoveSelectedShape(Mediator.DrawnShapes[2].X, Mediator.DrawnShapes[2].Y);
            Mediator.Instance.AddRemoveSelectedShape(Mediator.DrawnShapes[3].X, Mediator.DrawnShapes[3].Y);

            /*There, we have :
             Toolbar : 1 circle, 1 rect
             DrawnShapes : 1 circle, 1 rect, 1 group (made of 1 circle and 1 rect)
             every add of these shapes has created a Memento
             */
            reference = CareTaker.Instance.GetCurrentMemento();
        }

        [TestCleanup]
        public void TearDown()
        {
            reference = null;
            tmpReference = null;
            group = null;
        }

        [TestMethod]
        public void TestUndo()
        {
            /* As every operation is undone, the state of our objects is set back to the "reference". So every case starts with the same environment.*/

            //Undo a shape's drawing
            Mediator.Instance.LoadCurrentShape(1);
            Mediator.Instance.DrawCurrentShape(200, 200);
            Mediator.Instance.Undo();
            Assert.AreEqual(reference, CareTaker.Instance.GetCurrentMemento());

            //Undo a shape's deletion
            Mediator.Instance.AddRemoveSelectedShape(Mediator.DrawnShapes[0].X, Mediator.DrawnShapes[0].Y);
            Mediator.Instance.DeleteSelectedShapes();
            Mediator.Instance.Undo();
            Assert.AreEqual(reference, CareTaker.Instance.GetCurrentMemento());

            //Undo a toolbar item save
            Mediator.Instance.AddRemoveSelectedShape(Mediator.DrawnShapes[0].X, Mediator.DrawnShapes[0].Y);
            Mediator.Instance.SaveShapesInToolbar();
            Mediator.Instance.Undo();
            Assert.AreEqual(reference, CareTaker.Instance.GetCurrentMemento());

            //Undo a toolbar item delete
            int currentShapeIndex = 0;
            Mediator.Instance.LoadCurrentShape(currentShapeIndex);
            Mediator.Instance.DeleteCurrentShapeFromToolBar(currentShapeIndex);
            Mediator.Instance.Undo();
            Assert.AreEqual(reference, CareTaker.Instance.GetCurrentMemento());

            //Undo a rectangle's update
            Mediator.Instance.UpdateRectangle((Rectangle)Mediator.DrawnShapes.First(s => s.GetType() == typeof(Rectangle)), 200, 200, 100, 100, Color.Blue);
            Mediator.Instance.Undo();
            Assert.AreEqual(reference, CareTaker.Instance.GetCurrentMemento());

            //Undo a circle's update
            Mediator.Instance.UpdateCircle((Circle)Mediator.DrawnShapes.First(s => s.GetType() == typeof(Circle)), 200, 200, 100, Color.Blue);
            Mediator.Instance.Undo();
            Assert.AreEqual(reference, CareTaker.Instance.GetCurrentMemento());

            //Undo a group's update
            group = new GroupShapes();
            ((GroupShapes)group).Add(Mediator.DrawnShapes[2]);
            ((GroupShapes)group).Add(Mediator.DrawnShapes[3]);
            Mediator.Instance.CreateGroup((GroupShapes)group);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.UpdateGroup((GroupShapes)group, Color.Blue, 200, 200);
            Mediator.Instance.Undo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();

            //Undo a shapes groupage
            group = new GroupShapes();
            ((GroupShapes)group).Add(Mediator.DrawnShapes[2]);
            ((GroupShapes)group).Add(Mediator.DrawnShapes[3]);
            Mediator.Instance.CreateGroup((GroupShapes)group);
            Mediator.Instance.Undo();
            Assert.AreEqual(reference, CareTaker.Instance.GetCurrentMemento());

            //Undo a shapes ungroupage
            group = new GroupShapes();
            ((GroupShapes)group).Add(Mediator.DrawnShapes[2]);
            ((GroupShapes)group).Add(Mediator.DrawnShapes[3]);
            Mediator.Instance.CreateGroup((GroupShapes)group);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.DeleteGroup((GroupShapes)group);
            Mediator.Instance.Undo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();
        }

        [TestMethod]
        public void TestRedo()
        {
            /* As every operation is redone, the state of our objects is set back to the "reference" (by undoing). So every case starts with the same environment.*/

            //Redo a shape's drawing
            Mediator.Instance.LoadCurrentShape(1);
            Mediator.Instance.DrawCurrentShape(200, 200);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();

            //Redo a shape's deletion
            Mediator.Instance.AddRemoveSelectedShape(Mediator.DrawnShapes[0].X, Mediator.DrawnShapes[0].Y);
            Mediator.Instance.DeleteSelectedShapes();
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();

            //Redo a toolbar item save
            Mediator.Instance.AddRemoveSelectedShape(Mediator.DrawnShapes[0].X, Mediator.DrawnShapes[0].Y);
            Mediator.Instance.SaveShapesInToolbar();
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();

            //Redo a toolbar item delete
            int currentShapeIndex = 0;
            Mediator.Instance.LoadCurrentShape(currentShapeIndex);
            Mediator.Instance.DeleteCurrentShapeFromToolBar(currentShapeIndex);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();

            //Redo a rectangle's update
            Mediator.Instance.UpdateRectangle((Rectangle)Mediator.DrawnShapes.First(s => s.GetType() == typeof(Rectangle)), 200, 200, 100, 100, Color.Blue);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();

            //Redo a circle's update
            Mediator.Instance.UpdateCircle((Circle)Mediator.DrawnShapes.First(s => s.GetType() == typeof(Circle)), 200, 200, 100, Color.Blue);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();

            //Redo a group's update
            group = new GroupShapes();
            ((GroupShapes)group).Add(Mediator.DrawnShapes[2]);
            ((GroupShapes)group).Add(Mediator.DrawnShapes[3]);
            Mediator.Instance.CreateGroup((GroupShapes)group);
            Mediator.Instance.UpdateGroup((GroupShapes)group, Color.Blue, 200, 200);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();
            Mediator.Instance.Undo();

            //Redo a shapes groupage
            group = new GroupShapes();
            ((GroupShapes)group).Add(Mediator.DrawnShapes[2]);
            ((GroupShapes)group).Add(Mediator.DrawnShapes[3]);
            Mediator.Instance.CreateGroup((GroupShapes)group);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();

            //Redo a shapes ungroupage
            group = new GroupShapes();
            ((GroupShapes)group).Add(Mediator.DrawnShapes[2]);
            ((GroupShapes)group).Add(Mediator.DrawnShapes[3]);
            Mediator.Instance.CreateGroup((GroupShapes)group);
            Mediator.Instance.DeleteGroup((GroupShapes)group);
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Undo();
            Mediator.Instance.Redo();
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
            Mediator.Instance.Undo();
            Mediator.Instance.Undo();

            //Impossible redo, an action has been made after "undo"
            Mediator.Instance.LoadCurrentShape(0);
            Mediator.Instance.DrawCurrentShape(200, 200);
            Mediator.Instance.LoadCurrentShape(1);
            Mediator.Instance.DrawCurrentShape(200, 200);
            Mediator.Instance.Undo();
            Mediator.Instance.Undo();
            Mediator.Instance.AddRemoveSelectedShape(Mediator.DrawnShapes[0].X, Mediator.DrawnShapes[0].Y);
            Mediator.Instance.SaveShapesInToolbar();
            tmpReference = CareTaker.Instance.GetCurrentMemento();
            Mediator.Instance.Redo(); //fails, there's no memento to redo
            Assert.AreEqual(tmpReference, CareTaker.Instance.GetCurrentMemento());
        }

    }
}
