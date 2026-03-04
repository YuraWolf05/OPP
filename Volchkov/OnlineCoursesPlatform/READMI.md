classDiagram
direction TB

class User {
  +int id
  +str full_name
  +str email
  +str password_hash
  +str role  // "student" | "teacher"
  +datetime created_at
}

class Course {
  +int id
  +str title
  +str description
  +int teacher_id
  +datetime created_at
  +bool is_published
}

class Module {
  +int id
  +int course_id
  +str title
  +int order_index
}

class Lesson {
  +int id
  +int module_id
  +str title
  +str content
  +int order_index
  +int duration_minutes
}

class Enrollment {
  +int id
  +int student_id
  +int course_id
  +datetime enrolled_at
}

class LessonProgress {
  +int id
  +int enrollment_id
  +int lesson_id
  +bool is_completed
  +datetime completed_at
}

%% Services (business logic layer)
class ProgressService {
  +ensure_progress_rows(enrollment_id, course_id)
  +mark_completed(student_id, lesson_id)
  +course_percent(student_id, course_id)
  +is_course_completed(student_id, course_id)
}

class CertificateService {
  +generate_certificate_html(student, course, issued_at)
}

class CourseService {
  +create_course(teacher_id, title, description)
  +update_course(course_id, teacher_id, data)
  +delete_course(course_id, teacher_id)
}

%% Relationships
User "1" --> "0..*" Course : creates (teacher_id)
Course "1" --> "1..*" Module
Module "1" --> "1..*" Lesson

User "1" --> "0..*" Enrollment : enrolls (student_id)
Course "1" --> "0..*" Enrollment

Enrollment "1" --> "0..*" LessonProgress
Lesson "1" --> "0..*" LessonProgress

%% Services depend on models
ProgressService ..> Enrollment
ProgressService ..> LessonProgress
ProgressService ..> Lesson
ProgressService ..> Module
CertificateService ..> User
CertificateService ..> Course
CourseService ..> Course
CourseService ..> Module
CourseService ..> Lesson
CourseService ..> Enrollment
CourseService ..> LessonProgress
