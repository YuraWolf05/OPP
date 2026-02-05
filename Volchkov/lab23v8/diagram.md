```mermaid
classDiagram

    class BadPayrollSystem
    BadPayrollSystem --> SalaryCalculator
    BadPayrollSystem --> PdfExporter
    BadPayrollSystem --> SqlDatabase

    class GoodPayrollSystem
    GoodPayrollSystem --> ISalaryCalculator
    GoodPayrollSystem --> IReportExporter
    GoodPayrollSystem --> IDataStorage

    ISalaryCalculator <|.. BasicSalaryCalculator
    IReportExporter <|.. PdfReportExporter
    IDataStorage <|.. SqlDatabaseStorage
