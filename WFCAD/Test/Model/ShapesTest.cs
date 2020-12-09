using System.Collections.Generic;
using System.Drawing;
using Moq;
using NUnit.Framework;

namespace WFCAD {
    [TestFixture]
    public class ShapesTest {
        private (Mock<IShape> Mock, bool Result) CreateTestData(bool vIsSelected, bool vIsHit, bool vResult) {
            var wShape = new Mock<IShape>();
            wShape.Setup(m => m.IsSelected).Returns(vIsSelected);
            wShape.Setup(m => m.IsHit(It.IsAny<Point>())).Returns(vIsHit);
            return (wShape, vResult);
        }
        private void SelectTest(bool vIsMultiple, List<(Mock<IShape> Mock, bool Result)> vTestDatas) {
            var wShapes = new Shapes();
            vTestDatas.ForEach(x => wShapes.Add(x.Mock.Object));
            wShapes.Select(new Point(), vIsMultiple);
            vTestDatas.ForEach(x => x.Mock.VerifySet(y => y.IsSelected = x.Result));
        }

        [Test]
        public void 選択_前面にある図形が選択されること() {
            this.SelectTest(vIsMultiple: false, vTestDatas: new List<(Mock<IShape> Mock, bool Result)> {
                CreateTestData(vIsSelected: false, vIsHit: true, vResult: false),
                CreateTestData(vIsSelected: false, vIsHit: true, vResult: true),
            });
        }

        [Test]
        public void 選択_複数選択の場合は他の図形の選択状態が維持されること() {
            this.SelectTest(vIsMultiple: true, vTestDatas: new List<(Mock<IShape> Mock, bool Result)> {
                CreateTestData(vIsSelected: true, vIsHit: false, vResult: true),
                CreateTestData(vIsSelected: false, vIsHit: false, vResult: false),
                CreateTestData(vIsSelected: false, vIsHit: true, vResult: true),
            });
        }

        [Test]
        public void 選択_既に複数選択状態の場合は複数選択でなくても他の図形の選択状態が維持されること() {
            this.SelectTest(vIsMultiple: false, vTestDatas: new List<(Mock<IShape> Mock, bool Result)> {
                CreateTestData(vIsSelected: true, vIsHit: false, vResult: true),
                CreateTestData(vIsSelected: true, vIsHit: true, vResult: true),
                CreateTestData(vIsSelected: false, vIsHit: false, vResult: false),
            });
        }

        [Test]
        public void 選択_既に複数選択状態の場合は図形が選択されなかったらすべての選択状態が解除されること() {
            this.SelectTest(vIsMultiple: false, vTestDatas: new List<(Mock<IShape> Mock, bool Result)> {
                CreateTestData(vIsSelected: true, vIsHit: false, vResult: false),
                CreateTestData(vIsSelected: true, vIsHit: false, vResult: false),
                CreateTestData(vIsSelected: false, vIsHit: false, vResult: false),
            });
        }
    }
}
