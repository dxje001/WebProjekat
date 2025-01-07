# Healthcare Information System

## Overview
The **Healthcare Information System** is a web application simulating a healthcare management platform. It facilitates patient scheduling, doctor-patient interactions, and administrative operations in a medical center. The system supports multiple user roles with distinct functionalities and ensures efficient handling of healthcare processes.

## Key Technologies
- **Frontend:** HTML, CSS, JavaScript
- **Backend:** .NET (C#)  
- **Data Storage:** JSON, XML, CSV, TSV, or custom delimiter-separated files
- **Version Control:** Git (GitLab repository required)
- **Design:** Customizable CSS-based UI

## Features and Roles
### Roles
1. **Unregistered User (NK):** 
   - Log in to the system.

2. **Patient (P):**
   - Schedule appointments with doctors.
   - View scheduled appointments.

3. **Doctor (L):**
   - Manage appointment schedules (view, create, and update).
   - Prescribe and manage patient therapies.

4. **Administrator (A):**
   - Manage patient data (add, update, delete, view).
   - Oversee the system's user and data integrity.

### Core Features
- **Appointment Scheduling:** Patients can book available slots with doctors.
- **Appointment Management:** Doctors and patients can view and manage appointments.
- **Therapy Management:** Doctors can prescribe therapies for their patients.
- **Patient Management:** Administrators can add, edit, delete, and view patient records.

### Advanced Features (Optional)
- Sorting and filtering for:
  - Appointments by doctor, status, date, and time.
  - Patients by name, JMBG (ID), birthdate, and email.

## How It Works
1. **Data Management:** All data is stored in text files using supported formats (e.g., JSON, XML).
2. **User Interface:** A dynamic web interface styled with CSS ensures user-friendly interaction.
3. **Authentication:** Role-based access controls ensure security and integrity.
4. **Version Control:** Code is stored in a private GitLab repository for collaboration and maintenance.

## Getting Started
1. Clone the GitLab repository to your local environment.
2. Install the required dependencies listed in the project documentation.
3. Run the backend server and open the application in a browser.

---

For detailed implementation instructions and FAQs, refer to the [official documentation](https://docs.google.com/document/d/118ZbK0rXse7t60hXkEpp_y4PawS0TSoVQdHcSy4M7nE/edit?usp=sharing).
