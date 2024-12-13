using System;
using JABARACdesign.Base.Application.UI;

namespace JABARACdesign.Base.Presentation.UI.Screen
{
    /// <summary>
    /// スクリーンの基底データクラス。
    /// </summary>
    [Serializable]
    public class ScreenBaseData : BaseUIData
    {
        private string _screenTitle;
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="screenTitle">スクリーンのタイトル</param>
        public ScreenBaseData(string screenTitle)
        {
            _screenTitle = screenTitle;
        }
        
        public string ScreenTitle => _screenTitle;
    }
}