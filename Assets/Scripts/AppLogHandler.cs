using UnityEngine;
using System.IO;
using System;

namespace Unity.Logging
{
    /// <summary>
    /// 独自ログハンドラクラス
    /// 簡易シングルトンとしている
    /// </summary>
    public class AppLogHandler : ILogHandler
    {
        /// <summary>ストリーム</summary>
        private StreamWriter streamWriter;

        /// <summary>インスタンス</summary>
        private static AppLogHandler instance;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private AppLogHandler()
        {
            // ストリーム生成
            streamWriter = new StreamWriter(this.GetLogFilePath(), true);
        }

        /// <summary>
        /// インスタンスプロパティ
        /// </summary>
        public static AppLogHandler Instance
        {
            get
            {
                if (instance == null) instance = new AppLogHandler();
                return instance;
            }
        }

        /// <summary>
        /// LogException
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        public void LogException(System.Exception exception, UnityEngine.Object context)
        {
            streamWriter.WriteLine(StackTraceUtility.ExtractStringFromException(exception));
            streamWriter.Flush();
            Debug.logger.LogException(exception, context);
        }

        /// <summary>
        /// LogFormat
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            streamWriter.WriteLine(String.Format(format, args));
            streamWriter.Flush();
            Debug.logger.logHandler.LogFormat(logType, context, format, args);
        }

        /// <summary>
        /// ログファイルパス生成
        /// </summary>
        /// <returns>ログファイルパス</returns>
        private string GetLogFilePath()
        {
            // 起動日時をファイル名とし、月ごとのフォルダに格納する
            var now = DateTime.Now;
            var logFolder = Path.Combine(Application.dataPath, "log");
            var folderName = now.ToString("yyyyMM");
            var fileName = now.ToString("yyyyMMdd_hhmmss") + ".log";

            var folderPath = Path.Combine(logFolder, folderName);

            // フォルダ生成
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, fileName);
        }
    }

}