@startuml
skinparam classAttributeIconSize 0

' ====== Users ======
class User {
  +int Id
  +string FullName
  +string Email
  +string PasswordHash
  +DateTime CreatedAt
}

class Student {
  +ICollection<Enrollment> Enrollments
}

class Teacher {
  +ICollection<Course> CreatedCourses
}

User <|-- Student
User <|-- Teacher

' ====== Courses structure ======
class Course {
  +int Id
  +string Title
  +string Description
  +int TeacherId
  +DateTime CreatedAt
  +bool IsPublished
}

class Module {
  +int Id
  +string Title
  +int CourseId
  +int OrderIndex
}

class Lesson {
  +int Id
  +string Title
  +string Content
  +int ModuleId
  +int OrderIndex
  +int DurationMinutes
}

Teacher "1" -- "0..*" Course : creates >
Course "1" -- "1..*" Module
Module "1" -- "1..*" Lesson

' ====== Enrollment and progress ======
class Enrollment {
  +int Id
  +int StudentId
  +int CourseId
  +DateTime EnrolledAt
}

class LessonProgress {
  +int Id
  +int EnrollmentId
  +int LessonId
  +bool IsCompleted
  +DateTime? CompletedAt
}

Student "1" -- "0..*" Enrollment
Course "1" -- "0..*" Enrollment
Enrollment "1" -- "0..*" LessonProgress
Lesson "1" -- "0..*" LessonProgress

@enduml
