using System.Drawing;
using Moq;
using NUnit.Framework;

namespace WFCAD {
    [TestFixture]
    public class ShapesTest {
        [TestCase(false, false, true, false, false, true, false, false, true, true, TestName = "選択_前面にある図形が選択されること")]
        [TestCase(true, true, false, true, false, false, false, false, true, true, TestName = "選択_複数選択の場合は他の図形の選択状態が維持されること")]
        [TestCase(false, true, false, true, true, true, true, false, false, false, TestName = "選択_既に複数選択状態の場合は複数選択でなくても他の図形の選択状態が維持されること")]
        [TestCase(false, true, false, false, true, false, false, false, false, false, TestName = "選択_既に複数選択状態の場合は図形が選択されなかったらすべての選択状態が解除されること")]
        public void 選択(bool vIsMultiple, bool vIsSelected1, bool vIsHit1, bool vResult1, bool vIsSelected2, bool vIsHit2, bool vResult2, bool vIsSelected3, bool vIsHit3, bool vResult3) {
            Mock<IShape> CreateShapeMock(bool vIsSelected, bool vIsHit) {
                var wShape = new Mock<IShape>();
                wShape.Setup(m => m.IsSelected).Returns(vIsSelected);
                wShape.Setup(m => m.IsHit(It.IsAny<Point>())).Returns(vIsHit);
                return wShape;
            }
            Mock<IShape> wShape1 = CreateShapeMock(vIsSelected1, vIsHit1);
            Mock<IShape> wShape2 = CreateShapeMock(vIsSelected2, vIsHit2);
            Mock<IShape> wShape3 = CreateShapeMock(vIsSelected3, vIsHit3);

            var wShapes = new Shapes();
            wShapes.Add(wShape1.Object);
            wShapes.Add(wShape2.Object);
            wShapes.Add(wShape3.Object);
            wShapes.Select(new Point(), vIsMultiple);

            wShape1.VerifySet(x => x.IsSelected = vResult1);
            wShape2.VerifySet(x => x.IsSelected = vResult2);
            wShape3.VerifySet(x => x.IsSelected = vResult3);
        }

    }
}
