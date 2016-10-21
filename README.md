# log4net testing
log4net walk through
1. acquire a logger
log4net.ILog log =  log4net.LogManager.GetLogger(typeof(System.Action));
typeof(System.Action) will be the name of the logger

2. direct log4net to its config
a) add this to start up code
      XmlConfigurator.ConfigureAndWatch( new FileInfo(logFilePath + "/Configuration/Log4Net.config"));
b) or add this to assemblyinfo.cs
       [assembly:log4net.Config.XmlConfigurator(Watch = true)]

3. log4net configuration
a) need a section named "log4net"
 <configSections>
        <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net"/>
    </configSections>
    <log4net>    </log4net>
    
b) need a root in <log4net> section
        <log4net> <root></root>   </log4net>

c) detailed log4net configuration is in the root
        <root>
            <level value="ALL" />
            <appender-ref ref="MyAppender" />
            <appender-ref ref="MyFileAppender" />
            <appender-ref ref="RollingFileAppender" />
        </root>

d) appenders are like log sinks. There can be multiple of them. They work independently in parallel. Appenders referenced in <root> need to defined as well
e.g. the follow code defines 3 appenders
MyAppender sends all log entries to console
MyFileAppender sends all log entries to application.log
RollingFileAppender sends all log entries to rolling.log file. In addition, it keeps the size of the file under 1MB and the number of files under 5.
        <appender name="MyAppender" type="log4net.Appender.ConsoleAppender">
            <layout type="log4net.Layout.PatternLayout">
                <conversionPattern value="%date %level %logger - %message%newline" />
            </layout>
        </appender>
        <appender name="MyFileAppender" type="log4net.Appender.FileAppender">
            <file value="application.log" />
            <appendToFile value="true" />
            <lockingModel type="log4net.Appender.FileAppender+MinimalLock" />
            <layout type="log4net.Layout.PatternLayout">
                <conversionPattern value="%date %level %logger - %message%newline" />
            </layout>
        </appender>
        <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender">
            <file value="rolling.log" />
            <appendToFile value="true" />
            <rollingStyle value="Size" />
            <maxSizeRollBackups value="5" />
            <maximumFileSize value="1MB" />
            <staticLogFileName value="true" />
            <layout type="log4net.Layout.PatternLayout">
                <conversionPattern value="%date [%thread] %level %logger - %message%newline" />
            </layout>
        </appender>