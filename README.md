# 🧮 StudentMath

**StudentMath** is a modular exam management and student evaluation system built with **.NET 9** and **Blazor Server**.  
It demonstrates clean architecture principles — separating logic into Core, Data, Processor, API, and UI layers — and provides a Blazor-based interface for teachers and students.

---

## Authentication

The application uses an **in-memory user list** for demonstration purposes. You can log in as:

- **Teacher**: to upload and process exam XML files and view all students' exam results.  
- **Student**: to view your own exams and results.

> Sample users are already included in the database. After starting the app, access it at [http://localhost:5276](http://localhost:5276).

---

## Running the Application

1. Open the solution in **Visual Studio** or your preferred IDE.
2. Start first **API** project then, **UI** project.
3. The **UI** will be available at `http://localhost:5276`.

---

## Features

- 👨‍🏫 **Teacher login**: Upload and process exam XML files  
- 🎓 **Student login**: View individual exam results  
- 📊 **XML-based exam parsing** and score calculation  
- 💾 **SQLite for exams & student's task** support  
- 🧩 **Clean Architecture layers** for easy maintenance and testing  
- 🖥️ **Blazor Server UI** for smooth interactive experience  

---

## Sample Users

| Username   | Password  | Role     |
|-----------|----------|---------|
| teacher1  | teacher  | Teacher |
| S001      | student  | Student |
| S010      | student  | Student |
| S003      | student  | Student |
| S004      | student  | Student |
| S005      | student  | Student |

> Use the teacher account to upload XML exam files and check students’ evaluations. Students can log in to see their own exam results.

---

## Notes

- Ensure **API** and **UI** projects run simultaneously.  
- The **UI** communicates with the **API** at `localhost:5276`.  
- XML-based exam processing will parse uploaded files and calculate student scores automatically.

