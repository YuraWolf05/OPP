```mermaid
classDiagram
    class NumericProcessor {
        -INumericOperationStrategy strategy
        +NumericProcessor(strategy)
        +SetStrategy(strategy)
        +Process(input) double
    }

    class INumericOperationStrategy {
        <<interface>>
        +Execute(value) double
        +Name string
    }

    class SquareOperationStrategy {
        +Execute(value) double
        +Name string
    }

    class CubeOperationStrategy {
        +Execute(value) double
        +Name string
    }

    class SquareRootOperationStrategy {
        +Execute(value) double
        +Name string
    }

    NumericProcessor --> INumericOperationStrategy
    INumericOperationStrategy <|.. SquareOperationStrategy
    INumericOperationStrategy <|.. CubeOperationStrategy
    INumericOperationStrategy <|.. SquareRootOperationStrategy

    class ResultPublisher {
        +ResultCalculated event
        +PublishResult(result, operationName)
    }

    class ConsoleLoggerObserver {
        +OnResultCalculated(result, operationName)
    }

    class HistoryLoggerObserver {
        -history List~string~
        +History IReadOnlyList~string~
        +OnResultCalculated(result, operationName)
    }

    class ThresholdNotifierObserver {
        -threshold double
        +ThresholdNotifierObserver(threshold)
        +OnResultCalculated(result, operationName)
    }

    ResultPublisher --> ConsoleLoggerObserver : notifies
    ResultPublisher --> HistoryLoggerObserver : notifies
    ResultPublisher --> ThresholdNotifierObserver : notifies
