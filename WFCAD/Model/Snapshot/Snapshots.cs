using System.Collections.Generic;
using System.Linq;

namespace WFCAD {
    /// <summary>
    /// スナップショット群クラス
    /// </summary>
    public class Snapshots : ISnapshots {
        private List<ISnapshot> FSnapshots = new List<ISnapshot> {
            new Snapshot(null, new Shapes()) // 先頭に空のスナップショットを入れておきます
        };
        private int FCurrentIndex;

        #region メソッド

        /// <summary>
        /// スナップショットを追加します
        /// </summary>
        public void Add(ISnapshot vSnapshot) {
            foreach (ISnapshot wSnapshot in FSnapshots.Skip(FCurrentIndex + 1)) {
                wSnapshot.Bitmap.Dispose();
            }
            FSnapshots = FSnapshots.Take(FCurrentIndex + 1).ToList();
            FSnapshots.Add(vSnapshot);
            FCurrentIndex++;
        }

        /// <summary>
        /// 最新のスナップショットを取得します
        /// </summary>
        public ISnapshot GetLatest() => FSnapshots.Count > 0 ? FSnapshots[FCurrentIndex] : null;

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
