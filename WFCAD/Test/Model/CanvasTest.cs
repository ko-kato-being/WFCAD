using System.Collections.Generic;
using System.Drawing;
using Moq;
using NUnit.Framework;
using WFCAD.Model;

namespace WFCAD.Test.Model {
    [TestFixture]
    public class CanvasTest {

        #region 選択

        private (IShape Shape, bool Result) CreateSelectTestData(bool vIsSelected, bool vIsHit, bool vResult) {
            var wShape = new Mock<IShape>();
            wShape.SetupAllProperties();
            wShape.Setup(m => m.IsSelected).Returns(vIsSelected);
            wShape.Setup(m => m.IsHit(It.IsAny<PointF>())).Returns(vIsHit);
            return (wShape.Object, vResult);
        }

        private void SelectTest(bool vIsMultiple, List<(IShape Shape, bool Result)> vTestDatas) {
            var wShapes = new Canvas(new Bitmap(1, 1), Color.White);
            vTestDatas.ForEach(x => wShapes.Add(x.Shape));
            wShapes.Select(new Point(), vIsMultiple);
            for (int i = 0; i < vTestDatas.Count; i++) {
                Assert.AreEqual(vTestDatas[i].Result, vTestDatas[i].Shape.IsSelected, message: $"図形{i + 1}");
            }
        }

        [TestCase(true, true, true, true)]
        [TestCase(true, true, true, false)]
        [TestCase(true, true, false, false)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, true, true)]
        [TestCase(false, true, true, true)]
        [TestCase(false, true, true, false)]
        [TestCase(false, true, false, false)]
        [TestCase(false, false, true, false)]
        [TestCase(false, false, true, true)]
        public void 選択_図形が選択されなかったらすべての選択状態が解除されること(bool vIsMultiple, bool vIsSelected1, bool vIsSelected2, bool vIsSelected3) {
            this.SelectTest(vIsMultiple: vIsMultiple, vTestDatas: new List<(IShape Mock, bool Result)> {
                CreateSelectTestData(vIsSelected: vIsSelected1, vIsHit: false, vResult: false),
                CreateSelectTestData(vIsSelected: vIsSelected2, vIsHit: false, vResult: false),
                CreateSelectTestData(vIsSelected: vIsSelected3, vIsHit: false, vResult: false),
            });
        }

        [Test]
        public void 選択_前面にある図形が選択されること() {
            this.SelectTest(vIsMultiple: false, vTestDatas: new List<(IShape Mock, bool Result)> {
                CreateSelectTestData(vIsSelected: false, vIsHit: true, vResult: false),
                CreateSelectTestData(vIsSelected: false, vIsHit: true, vResult: true),
            });
        }

        [Test]
        public void 選択_複数選択モードの場合は他の図形の選択状態が維持されること() {
            this.SelectTest(vIsMultiple: true, vTestDatas: new List<(IShape Mock, bool Result)> {
                CreateSelectTestData(vIsSelected: true, vIsHit: false, vResult: true),
                CreateSelectTestData(vIsSelected: false, vIsHit: false, vResult: false),
                CreateSelectTestData(vIsSelected: false, vIsHit: true, vResult: true),
            });
        }

        [TestCase(true, TestName = "選択_複数選択モードの場合は選択状態の図形を選択したら未選択状態になること")]
        [TestCase(false, TestName = "選択_複数選択モードの場合は未選択状態の図形を選択したら選択状態になること")]
        public void 選択_複数選択モードの場合は図形を選択したら選択状態が反転すること(bool vIsSelected) {
            this.SelectTest(vIsMultiple: true, vTestDatas: new List<(IShape Mock, bool Result)> {
                CreateSelectTestData(vIsSelected: true, vIsHit: true, vResult: true),
                CreateSelectTestData(vIsSelected: true, vIsHit: false, vResult: true),
                CreateSelectTestData(vIsSelected: vIsSelected, vIsHit: true, vResult: !vIsSelected),
            });
        }

        [Test]
        public void 選択_複数選択状態で選択状態の図形を選択した場合は他の図形の選択状態が維持されること() {
            this.SelectTest(vIsMultiple: false, vTestDatas: new List<(IShape Mock, bool Result)> {
                CreateSelectTestData(vIsSelected: false, vIsHit: true, vResult: false),
                CreateSelectTestData(vIsSelected: true, vIsHit: true, vResult: true),
                CreateSelectTestData(vIsSelected: true, vIsHit: true, vResult: true),
            });
        }

        [Test]
        public void 選択_複数選択状態で未選択状態の図形を選択した場合は他の図形の選択状態が解除されること() {
            this.SelectTest(vIsMultiple: false, vTestDatas: new List<(IShape Mock, bool Result)> {
                CreateSelectTestData(vIsSelected: true, vIsHit: true, vResult: false),
                CreateSelectTestData(vIsSelected: true, vIsHit: true, vResult: false),
                CreateSelectTestData(vIsSelected: false, vIsHit: true, vResult: true),
            });
        }

        #endregion 選択

    }
}
