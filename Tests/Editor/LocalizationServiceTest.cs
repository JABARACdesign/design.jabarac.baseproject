using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JABARACdesign.Base.Application.Localization;
using JABARACdesign.Base.Domain.Localization;
using JABARACdesign.Base.Infrastructure.Localization;
using NSubstitute;
using NUnit.Framework;

namespace JABARACdesign.Base.Tests.Editor
{
    /// <summary>
    /// LocalizationServiceのテスト。
    /// </summary>
    [TestFixture]
    public class LocalizationServiceTest
    {
        private ILocalizationTableLoader _mockLoader;
        private LocalizationService _service;

        [SetUp]
        public void SetUp()
        {
            _mockLoader = Substitute.For<ILocalizationTableLoader>();

            _mockLoader.LoadTableAsync(languageCode: LanguageCode.Ja).Returns(
                UniTask.FromResult(result: new Dictionary<string, string>
                {
                    { "COMMON_OK", "OK" },
                    { "COMMON_CANCEL", "キャンセル" },
                    { "SCREEN_HOME_TITLE", "ホーム" },
                    { "SCREEN_HOME_GREETING", "こんにちは、#{userName}さん" },
                    { "SCREEN_HOME_STATS", "#{totalSongs}曲中#{completedSongs}曲完了" },
                }));

            _mockLoader.LoadTableAsync(languageCode: LanguageCode.En).Returns(
                UniTask.FromResult(result: new Dictionary<string, string>
                {
                    { "COMMON_OK", "OK" },
                    { "COMMON_CANCEL", "Cancel" },
                    { "SCREEN_HOME_TITLE", "Home" },
                    { "SCREEN_HOME_GREETING", "Hello, #{userName}" },
                    { "SCREEN_HOME_STATS", "#{completedSongs} of #{totalSongs} songs completed" },
                }));

            _mockLoader.LoadTableAsync(languageCode: LanguageCode.Ko).Returns(
                UniTask.FromResult(result: new Dictionary<string, string>
                {
                    { "COMMON_OK", "확인" },
                    { "COMMON_CANCEL", "취소" },
                    { "SCREEN_HOME_TITLE", "홈" },
                    { "SCREEN_HOME_GREETING", "안녕하세요, #{userName}님" },
                }));

            _service = new LocalizationService(tableLoader: _mockLoader);
        }

        [Test]
        public void InitializeAsync_日本語で初期化できる()
        {
            // Act
            _service.InitializeAsync(languageCode: LanguageCode.Ja).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(expected: LanguageCode.Ja, actual: _service.CurrentLanguage);
        }

        [Test]
        public void GetText_登録済みキーのテキストを取得できる()
        {
            // Arrange
            _service.InitializeAsync(languageCode: LanguageCode.Ja).GetAwaiter().GetResult();

            // Act
            var result = _service.GetText(key: "SCREEN_HOME_TITLE");

            // Assert
            Assert.AreEqual(expected: "ホーム", actual: result);
        }

        [Test]
        public void GetText_未登録キーの場合はキー自体が返る()
        {
            // Arrange
            _service.InitializeAsync(languageCode: LanguageCode.Ja).GetAwaiter().GetResult();

            // Act
            var result = _service.GetText(key: "NONEXISTENT_KEY");

            // Assert
            Assert.AreEqual(expected: "NONEXISTENT_KEY", actual: result);
        }

        [Test]
        public void GetText_名前付き引数でフォーマットが機能する()
        {
            // Arrange
            _service.InitializeAsync(languageCode: LanguageCode.Ja).GetAwaiter().GetResult();

            // Act
            var result = _service.GetText(
                key: "SCREEN_HOME_GREETING",
                args: new Dictionary<string, object> { { "userName", "太郎" } });

            // Assert
            Assert.AreEqual(expected: "こんにちは、太郎さん", actual: result);
        }

        [Test]
        public void GetText_複数の名前付き引数でフォーマットが機能する()
        {
            // Arrange
            _service.InitializeAsync(languageCode: LanguageCode.Ja).GetAwaiter().GetResult();

            // Act
            var result = _service.GetText(
                key: "SCREEN_HOME_STATS",
                args: new Dictionary<string, object>
                {
                    { "totalSongs", 10 },
                    { "completedSongs", 7 },
                });

            // Assert
            Assert.AreEqual(expected: "10曲中7曲完了", actual: result);
        }

        [Test]
        public void GetText_英語に切り替えて取得できる()
        {
            // Arrange
            _service.InitializeAsync(languageCode: LanguageCode.En).GetAwaiter().GetResult();

            // Act
            var result = _service.GetText(key: "COMMON_CANCEL");

            // Assert
            Assert.AreEqual(expected: "Cancel", actual: result);
        }

        [Test]
        public void GetText_英語の名前付き引数の並び順が言語ごとに異なっても正しく動作する()
        {
            // Arrange — 英語は "#{completedSongs} of #{totalSongs}" の順序
            _service.InitializeAsync(languageCode: LanguageCode.En).GetAwaiter().GetResult();

            // Act
            var result = _service.GetText(
                key: "SCREEN_HOME_STATS",
                args: new Dictionary<string, object>
                {
                    { "totalSongs", 10 },
                    { "completedSongs", 7 },
                });

            // Assert
            Assert.AreEqual(expected: "7 of 10 songs completed", actual: result);
        }

        [Test]
        public void GetText_韓国語で取得できる()
        {
            // Arrange
            _service.InitializeAsync(languageCode: LanguageCode.Ko).GetAwaiter().GetResult();

            // Act
            var result = _service.GetText(
                key: "SCREEN_HOME_GREETING",
                args: new Dictionary<string, object> { { "userName", "민수" } });

            // Assert
            Assert.AreEqual(expected: "안녕하세요, 민수님", actual: result);
        }

        [Test]
        public void SetLanguage_言語変更イベントが発火する()
        {
            // Arrange
            _service.InitializeAsync(languageCode: LanguageCode.Ja).GetAwaiter().GetResult();
            LanguageCode? changedTo = null;
            _service.OnLanguageChanged += lang => changedTo = lang;

            // Act
            _service.SetLanguage(languageCode: LanguageCode.En);

            // Assert
            Assert.AreEqual(expected: LanguageCode.En, actual: changedTo);
            Assert.AreEqual(expected: LanguageCode.En, actual: _service.CurrentLanguage);
        }

        [Test]
        public void SetLanguage_同一言語への変更ではイベントが発火しない()
        {
            // Arrange
            _service.InitializeAsync(languageCode: LanguageCode.Ja).GetAwaiter().GetResult();
            var eventCount = 0;
            _service.OnLanguageChanged += _ => eventCount++;

            // Act
            _service.SetLanguage(languageCode: LanguageCode.Ja);

            // Assert
            Assert.AreEqual(expected: 0, actual: eventCount);
        }
    }
}
