```mermaid
classDiagram
    class CustomList {
        +Add(item)
    }

    class ReadOnlyList {
        +Add(item)
    }

    CustomList <|-- ReadOnlyList

    class IReadOnlyList {
        +Count
    }

    class IWritableList {
        +Add(item)
    }

    IReadOnlyList <|-- IWritableList
    IWritableList <|.. WritableList
    IReadOnlyList <|.. SafeReadOnlyList
