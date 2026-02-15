```mermaid
classDiagram

    class ILogger {
        <<interface>>
        +Log(message)
    }

    class ConsoleLogger {
        +Log(message)
    }
    class FileLogger {
        -filePath
        +Log(message)
    }

    ILogger <|.. ConsoleLogger
    ILogger <|.. FileLogger

    class LoggerFactory {
        <<abstract>>
        +CreateLogger() ILogger
    }

    class ConsoleLoggerFactory {
        +CreateLogger() ILogger
    }
    class FileLoggerFactory {
        -filePath
        +CreateLogger() ILogger
    }

    LoggerFactory <|-- ConsoleLoggerFactory
    LoggerFactory <|-- FileLoggerFactory

    class LoggerManager {
        <<singleton>>
        -factory
        -logger
        +Instance
        +SetFactory(factory)
        +GetLogger() ILogger
    }

    LoggerManager --> LoggerFactory
    LoggerManager --> ILogger

    class IDataProcessorStrategy {
        <<interface>>
        +Name
        +Process(data) string
    }

    class EncryptDataStrategy {
        +Name
        +Process(data) string
    }

    class CompressDataStrategy {
        +Name
        +Process(data) string
    }

    IDataProcessorStrategy <|.. EncryptDataStrategy
    IDataProcessorStrategy <|.. CompressDataStrategy

    class DataContext {
        -strategy
        +SetStrategy(strategy)
        +Process(data) string
        +CurrentStrategyName
    }

    DataContext --> IDataProcessorStrategy

    class DataPublisher {
        +DataProcessed event
        +PublishDataProcessed(data, strategyName)
    }

    class ProcessingLoggerObserver {
        -publisher
        +ProcessingLoggerObserver(publisher)
        +Unsubscribe()
    }

    ProcessingLoggerObserver --> DataPublisher
    ProcessingLoggerObserver --> LoggerManager
