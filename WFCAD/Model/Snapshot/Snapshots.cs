using System.Collections.Generic;
using System.Linq;

namespace WFCAD {
    /// <summary>
    /// スナップショット群クラス
    /// </summary>
    public class Snapshots : ISnapshots{
        private List<ISnapshot> FSnapshots = new List<ISnapshot>();
        private int FCurrentIndex = -1;

        #region メソッド

        /// <summary>
        /// スナップショットを追加します
        /// </summary>
        public void Add(ISnapshot vSnapshot) {
            FSnapshots = FSnapshots.Take(FCurrentIndex + 1).ToList();
            FSnapshots.Add(vSnapshot);
            FCurrentIndex++;
        }

        /// <summary>
        /// 一つ前のスナップショットを取得します
        /// </summary>
        public ISnapshot GetBefore() => FCurrentIndex > 0 ? FSnapshots[--FCurrentIndex] : null;

        /// <summary>
        /// 一つ後のスナップショットを取得します
        /// </summary>
        public ISnapshot GetAfter() => FCurrentIndex < FSnapshots.Count - 1 ? FSnapshots[++FCurrentIndex] : null;

        #endregion メソッド

    }
}
