using log4net;
using log4net.Config;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Wen.Helpers.Log
{
    /// <summary>
    /// 超级日志管理器
    /// </summary>
    public static class SuperLogger
    {
        /// <summary>
        /// 日志队列
        /// </summary>
        private static readonly ConcurrentQueue<KeyValuePair<SuperLogLevel, string>> LogQueue =
            new ConcurrentQueue<KeyValuePair<SuperLogLevel, string>>();

        /// <summary>
        /// 信号
        /// </summary>
        private static readonly ManualResetEvent Semaphore = new ManualResetEvent(false);

        /// <summary>
        /// 日志
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static SuperLogger()
        {
            try
            {
                Init();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// 另一个线程记录日志，只在程序初始化时调用一次
        /// </summary>
        public static void Register()
        {
            Task.Factory.StartNew(WriteLog);

            //Thread t = new Thread(new ThreadStart(WriteLog))
            //{
            //    IsBackground = false
            //};
            //t.Start();
        }

        /// <summary>
        /// 从队列中写日志至磁盘
        /// </summary>
        private static void WriteLog()
        {
            while (true)
            {
                // 等待信号通知
                Semaphore.WaitOne();

                // 判断是否有内容需要如磁盘 从列队中获取内容，并删除列队中的内容
                while (LogQueue.Count > 0 && LogQueue.TryDequeue(out KeyValuePair<SuperLogLevel, string> keyValuePairs))
                {
                    // 判断日志等级，然后写日志
                    switch (keyValuePairs.Key)
                    {
                        case SuperLogLevel.Debug:
                            Log.Debug(keyValuePairs.Value);
                            break;
                        case SuperLogLevel.Info:
                            Log.Info(keyValuePairs.Value);
                            break;
                        case SuperLogLevel.Error:
                            Log.Error(keyValuePairs.Value);
                            break;
                        case SuperLogLevel.Warn:
                            Log.Warn(keyValuePairs.Value);
                            break;
                        case SuperLogLevel.Fatal:
                            Log.Fatal(keyValuePairs.Value);
                            break;
                    }
                }

                // 重新设置信号
                Semaphore.Reset();
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Debug(string message)
        {
            EnqueueMessage(message, SuperLogLevel.Debug);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            EnqueueMessage(message, SuperLogLevel.Error);
        }

        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            EnqueueMessage(message, SuperLogLevel.Fatal);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            EnqueueMessage(message, SuperLogLevel.Info);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            EnqueueMessage(message, SuperLogLevel.Warn);
        }

        /// <summary>
        /// 将消息入列
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        private static void EnqueueMessage(string message, SuperLogLevel level)
        {
            switch (level)
            {
                case SuperLogLevel.Debug:
                    if (!Log.IsDebugEnabled) return;
                    break;
                case SuperLogLevel.Info:
                    if (!Log.IsInfoEnabled) return;
                    break;
                case SuperLogLevel.Error:
                    if (!Log.IsErrorEnabled) return;
                    break;
                case SuperLogLevel.Warn:
                    if (!Log.IsWarnEnabled) return;
                    break;
                case SuperLogLevel.Fatal:
                    if (!Log.IsFatalEnabled) return;
                    break;
                default:
                    return;
            }

            LogQueue.Enqueue(new KeyValuePair<SuperLogLevel, string>(level, message));

            // 通知线程往磁盘中写日志
            Semaphore.Set();
        }

        private static void Init()
        {
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            if (!configFile.Exists)
            {
                throw new Exception("未配置log4net配置文件！");
            }

            // 设置日志配置文件路径
            XmlConfigurator.Configure(configFile);
        }
    }
}
