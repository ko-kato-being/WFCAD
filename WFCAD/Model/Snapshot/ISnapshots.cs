namespace WFCAD.Model.Snapshot {
    /// <summary>
    /// スナップショット群を表すインターフェース
    /// </summary>
    public interface ISnapshots {

        #region メソッド

        /// <summary>
        /// スナップショットを追加します
        /// </summary>
        void Add(ISnapshot vSnapshot);

        /// <summary>
        /// 最新のスナップショットを取得します
        /// </summary>
        ISnapshot GetLatest();

        /// <summary>
        /// 一つ前のスナップショットを取得します
        /// </summary>
        ISnapshot GetBefore();

        /// <summary>
        /// 一つ後のスナップショットを取得します
        /// </summary>
        ISnapshot GetAfter();

        #endregion メソッド

    }
}
