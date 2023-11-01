using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace SAPAvisosPM.Helper
{
    public static class NetLogger
    {
        #region Members
        private static readonly ILog logger = LogManager.GetLogger(typeof(NetLogger));
        #endregion
        #region Constructors
        static NetLogger()
        {
            XmlConfigurator.Configure();
        }
        #endregion
        #region Methods
        public static void WriteLog(ELogLevel logLevel, String log)
        {
            if (logLevel.Equals(ELogLevel.DEBUG))
            {
                logger.Debug(log);
            }
            else if (logLevel.Equals(ELogLevel.ERROR))
            {
                logger.Error(log);
            }

            else if (logLevel.Equals(ELogLevel.FATAL))
            {
                logger.Fatal(log);
            }

            else if (logLevel.Equals(ELogLevel.INFO))
            {
                logger.Info(log);
            }

            else if (logLevel.Equals(ELogLevel.WARNING))
            {
                logger.Warn(log);
            }
        }
        #endregion

    }

    public enum ELogLevel
    {

        DEBUG = 1,

        ERROR = 2,

        FATAL = 3,

        INFO = 4,

        WARNING = 5

    }
}
