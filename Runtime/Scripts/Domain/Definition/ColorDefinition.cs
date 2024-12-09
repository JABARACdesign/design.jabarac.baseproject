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
        /// Voickにおける色の定義。
        /// </summary>
        public enum VoickColor
        {
            PrimaryContainer,
            OnPrimaryContainer,
            OnPrimary,
            Primary,
            SecondaryContainer,
            OnSecondaryContainer,
            Secondary,
            OnSecondary,
            TertiaryContainer,
            OnTertiaryContainer,
            ErrorContainer,
            OnErrorContainer,
            Error,
            OnError,
            Tertiary,
            OnTertiary,
            Background,
            OnBackground,
            Surface,
            OnSurface,
            Outline,
            SurfaceVariant,
            OnSurfaceVariant,
            Scrim,
            SurfaceContainer,
            OnSurfaceContainer,
            OutlineVariant,
            Quaternary,
            OnQuaternary,
            OnQuaternaryContainer,
            QuaternaryContainer,
            Quinary,
            OnQuinary,
            QuinaryContainer,
            OnQuinaryContainer,
            Clear,
            ScoreE,
            ScoreC,
            ScoreB,
            ScoreA,
            ScoreS,
            ScoreSS,
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
        /// Voickにおける色の定義から色コードの辞書。
        /// </summary>
        private static readonly Dictionary<VoickColor, string> VoickColorCodeDictionary =
            new()
            {
                { VoickColor.PrimaryContainer, "#67DEDF" },
                { VoickColor.OnPrimaryContainer, "#195F64" },
                { VoickColor.OnPrimary, "#DDF3F5" },
                { VoickColor.Primary, "#21B4B9" },
                { VoickColor.SecondaryContainer, "#D2E457" },
                { VoickColor.OnSecondaryContainer, "#555E13" },
                { VoickColor.Secondary, "#96C838" },
                { VoickColor.OnSecondary, "#F8FAED" },
                { VoickColor.TertiaryContainer, "#F8C756" },
                { VoickColor.OnTertiaryContainer, "#754F18" },
                { VoickColor.ErrorContainer, "#FE7787" },
                { VoickColor.OnErrorContainer, "#7E1C28" },
                { VoickColor.Error, "#E45F6F" },
                { VoickColor.OnError, "#FFF2F4" },
                { VoickColor.Tertiary, "#EFA842" },
                { VoickColor.OnTertiary, "#FAF6ED" },
                { VoickColor.Background, "#3E3E3E" },
                { VoickColor.OnBackground, "#E9EDEF" },
                { VoickColor.Surface, "#4D4D4D" },
                { VoickColor.OnSurface, "#FFFFFF" }, //
                { VoickColor.Outline, "#B7BDC0" },
                { VoickColor.SurfaceVariant, "#677378" },
                { VoickColor.OnSurfaceVariant, "#CAD4D9" },
                { VoickColor.Scrim, "#000000" },
                { VoickColor.SurfaceContainer, "#ECECEC" },
                { VoickColor.OnSurfaceContainer, "#676767" },
                { VoickColor.OutlineVariant, "#BDC8CC" },
                { VoickColor.Quaternary, "#D76BB2" },
                { VoickColor.OnQuaternary, "#F9EAF3" },
                { VoickColor.OnQuaternaryContainer, "#76275B" },
                { VoickColor.QuaternaryContainer, "#EB85C8" },
                { VoickColor.Quinary, "#E96D46" },
                { VoickColor.OnQuinary, "#FBEEEA" },
                { VoickColor.QuinaryContainer, "#F5835F" },
                { VoickColor.OnQuinaryContainer, "#752B13" },
                { VoickColor.Clear, "#00000000" },
                { VoickColor.ScoreSS, "#EFB1FF" },
                { VoickColor.ScoreS, "#97DAFF" },
                { VoickColor.ScoreA, "#FBD164" },
                { VoickColor.ScoreE, "#ECECEC" },
                { VoickColor.ScoreB, "#BFD2F7" },
                { VoickColor.ScoreC, "#CDB3A1" },
            };
        
        /// <summary>
        /// Voickにおける色の定義から色を取得する。
        /// </summary>
        /// <param name="voickColor">VoickColor</param>
        /// <param name="alphaType">アルファタイプ</param>
        /// <returns>Color</returns>
        public static Color GetColor(VoickColor voickColor, AlphaType alphaType = AlphaType.None)
        {
            if (!VoickColorCodeDictionary.TryGetValue(key: voickColor, value: out var colorCode))
            {
                LogHelper.Error(message: $"定義されていない色が指定されました: {voickColor}");
                return NOT_DEFINED_COLOR;
            }
            
            if (!ColorUtility.TryParseHtmlString(htmlString: colorCode, color: out var color))
            {
                LogHelper.Error(message: $"色コードの解析に失敗しました: {voickColor} - {colorCode}");
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