using UnityEngine;
using System;

namespace Unity.Logging
{
    /// <summary>
    /// ロガークラス
    /// </summary>
    public class AppLogger
    {
        #region 定数

        /// <summary>Infoタグ</summary>
        private readonly static string TagInfo = "[Info]";

        /// <summary>Warningタグ</summary>
        private readonly static string TagWarning = "[Warning]";

        /// <summary>Errorタグ</summary>
        private readonly static string TagError = "[Error]";

        #endregion

        #region メンバ変数

        /// <summary>ロガーインスタンス</summary>
        private ILogger logger = new Logger(AppLogHandler.Instance);

        /// <summary>クラス名称</summary>
        private string className;

        #endregion

        #region コンストラクタ

        public AppLogger(Type type, LogType logType)
        {
            // 名前空間.クラス名
            className = type.Namespace + "." + type.Name;

            logger.filterLogType = logType;
        }

        #endregion

        #region 公開メソッド

        /// <summary>
        /// ロガー生成
        /// </summary>
        /// <param name="type">対象クラス</param>
        /// <returns>ロガー</returns>
        public static AppLogger CreateLogger(Type type, LogType logType)
        {
            return new AppLogger(type, logType);
        }

        /// <summary>
        /// Infoログ出力
        /// </summary>
        /// <param name="message">出力メッセージ</param>
        public void LogInfo(string message)
        {
            logger.Log(TagInfo, CreateMessage(className, message));
        }

        /// <summary>
        /// Warningログ出力
        /// </summary>
        /// <param name="message">出力メッセージ</param>
        public void LogWarning(string message)
        {
            logger.LogWarning(TagWarning, CreateMessage(className, message));
        }

        /// <summary>
        /// Errorログ出力
        /// </summary>
        /// <param name="message">出力メッセージ</param>
        public void LogError(string message)
        {
            logger.LogError(TagError, CreateMessage(className, message));
        }

        /// <summary>
        /// Exceptionログ出力
        /// </summary>
        /// <typeparam name="T">Exceptrionを継承するクラス</typeparam>
        /// <param name="message">出力メッセージ</param>
        /// <param name="context">例外発生インスタンス</param>
        public void LogException<T>(T exception, UnityEngine.Object context) where T : Exception
        {
            LogError(exception.Message);
            LogError(exception.Source);
            LogError(exception.StackTrace);
            logger.LogException(exception, context);
        }

        #endregion

        #region 非公開メソッド

        /// <summary>
        /// メッセージ生成
        /// </summary>
        /// <param name="className"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private string CreateMessage(string className, string message)
        {
            var now = DateTime.Now.ToString("yyyy/mm/dd hh:mm:ss:fff");
            return
                className
                + "," + now
                + "," + message;
        }

        #endregion
    }
}

