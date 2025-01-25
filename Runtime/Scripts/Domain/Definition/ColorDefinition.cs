using System.Collections.Generic;
using JABARACdesign.Base.Domain.Entity.Helper;
using UnityEngine;

namespace JABARACdesign.Base.Domain.Definition
{
    /// <summary>
    /// 色の定義を管理するクラス。
    /// </summary>
    public static class ColorDefinition
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
        
        /// <summary>
        /// 色の定義。
        /// </summary>
        public enum MyColor
        {
            // Primary colors
            Primary,
            OnPrimary,
            PrimaryContainer,
            OnPrimaryContainer,

            // Secondary colors
            Secondary,
            OnSecondary,
            SecondaryContainer,
            OnSecondaryContainer,

            // Tertiary colors
            Tertiary,
            OnTertiary,
            TertiaryContainer,
            OnTertiaryContainer,

            // Error colors
            Error,
            OnError,
            ErrorContainer,
            OnErrorContainer,

            // Background and surface colors
            Background,
            OnBackground,
            Surface,
            OnSurface,
            SurfaceVariant,
            OnSurfaceVariant,
            SurfaceContainer,
            OnSurfaceContainer,

            // Outline colors
            Outline,
            OutlineVariant,

            // Other
            Scrim,
            Clear,
        }
        
        private const float NONE = 1.0f;
        private const float ALPHA_006 = 0.06f;
        private const float ALPHA_012 = 0.12f;
        private const float ALPHA_024 = 0.24f;
        private const float ALPHA_060 = 0.60f;
        private const float ALPHA_080 = 0.80f;
        private const float ALPHA_090 = 0.90f;
        public const float DISABLED_ALPHA = ALPHA_024;
        private static Color NOT_DEFINED_COLOR = new(r: 0, g: 0, b: 0, a: 0);
        
        /// <summary>
        /// 色の定義から色コードの辞書。
        /// </summary>
        private static readonly Dictionary<MyColor, string> MyColorCodeDictionary =
            new()
            {
                // Primary colors
                { MyColor.Primary, "#21B4B9" },
                { MyColor.OnPrimary, "#DDF3F5" },
                { MyColor.PrimaryContainer, "#67DEDF" },
                { MyColor.OnPrimaryContainer, "#195F64" },

                // Secondary colors
                { MyColor.Secondary, "#96C838" },
                { MyColor.OnSecondary, "#F8FAED" },
                { MyColor.SecondaryContainer, "#D2E457" },
                { MyColor.OnSecondaryContainer, "#555E13" },

                // Tertiary colors
                { MyColor.Tertiary, "#EFA842" },
                { MyColor.OnTertiary, "#FAF6ED" },
                { MyColor.TertiaryContainer, "#F8C756" },
                { MyColor.OnTertiaryContainer, "#754F18" },

                // Error colors
                { MyColor.Error, "#E45F6F" },
                { MyColor.OnError, "#FFF2F4" },
                { MyColor.ErrorContainer, "#FE7787" },
                { MyColor.OnErrorContainer, "#7E1C28" },

                // Background and surface colors
                { MyColor.Background, "#3E3E3E" },
                { MyColor.OnBackground, "#E9EDEF" },
                { MyColor.Surface, "#4D4D4D" },
                { MyColor.OnSurface, "#FFFFFF" },
                { MyColor.SurfaceVariant, "#677378" },
                { MyColor.OnSurfaceVariant, "#CAD4D9" },
                { MyColor.SurfaceContainer, "#ECECEC" },
                { MyColor.OnSurfaceContainer, "#676767" },

                // Outline colors
                { MyColor.Outline, "#B7BDC0" },
                { MyColor.OutlineVariant, "#BDC8CC" },

                // Other
                { MyColor.Scrim, "#000000" },
                { MyColor.Clear, "#00000000" },
            };
        
        /// <summary>
        /// 色の定義から色を取得する。
        /// </summary>
        /// <param name="myColor">VoickColor</param>
        /// <param name="alphaType">アルファタイプ</param>
        /// <returns>Color</returns>
        public static Color GetColor(MyColor myColor, AlphaType alphaType = AlphaType.None)
        {
            if (!MyColorCodeDictionary.TryGetValue(key: myColor, value: out var colorCode))
            {
                LogHelper.Error(message: $"定義されていない色が指定されました: {myColor}");
                return NOT_DEFINED_COLOR;
            }
            
            if (!ColorUtility.TryParseHtmlString(htmlString: colorCode, color: out var color))
            {
                LogHelper.Error(message: $"色コードの解析に失敗しました: {myColor} - {colorCode}");
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
        private static float GetAlphaValue(AlphaType alphaType)
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