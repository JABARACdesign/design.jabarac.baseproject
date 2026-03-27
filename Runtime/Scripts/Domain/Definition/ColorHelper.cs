using JABARACdesign.Base.Domain.Entity.Helper;
using UnityEngine;

namespace JABARACdesign.Base.Domain.Definition
{
    /// <summary>
    /// 色に関するユーティリティクラス。
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// 不透明度の種類。
        /// </summary>
        public enum AlphaType
        {
            None,
            Alpha006,
            Alpha012,
            Alpha024,
            Alpha060,
            Alpha080,
            Alpha090,
        }

        private const float NONE = 1.0f;
        private const float ALPHA_006 = 0.06f;
        private const float ALPHA_012 = 0.12f;
        private const float ALPHA_024 = 0.24f;
        private const float ALPHA_060 = 0.60f;
        private const float ALPHA_080 = 0.80f;
        private const float ALPHA_090 = 0.90f;
        public const float DISABLED_ALPHA = ALPHA_024;
        private static readonly Color NOT_DEFINED_COLOR = new(r: 0, g: 0, b: 0, a: 0);

        /// <summary>
        /// 色コードから色を取得する。
        /// </summary>
        /// <param name="colorCode">色コード（例: "#FFFFFF"）</param>
        /// <param name="alphaType">アルファタイプ</param>
        /// <returns>Color</returns>
        public static Color GetColorFromCode(string colorCode, AlphaType alphaType = AlphaType.None)
        {
            if (!ColorUtility.TryParseHtmlString(htmlString: colorCode, color: out var color))
            {
                LogHelper.Error(message: $"色コードの解析に失敗しました: {colorCode}");
                return NOT_DEFINED_COLOR;
            }

            if (color.a >= 1)
            {
                color.a = GetAlphaValue(alphaType: alphaType);
            }

            return color;
        }

        /// <summary>
        /// アルファタイプに基づいてアルファ値を取得する。
        /// </summary>
        /// <param name="alphaType">アルファタイプ</param>
        /// <returns>アルファ値</returns>
        public static float GetAlphaValue(AlphaType alphaType)
        {
            return alphaType switch
            {
                AlphaType.Alpha006 => ALPHA_006,
                AlphaType.Alpha012 => ALPHA_012,
                AlphaType.Alpha024 => ALPHA_024,
                AlphaType.Alpha060 => ALPHA_060,
                AlphaType.Alpha080 => ALPHA_080,
                AlphaType.Alpha090 => ALPHA_090,
                _ => NONE
            };
        }
    }
}
