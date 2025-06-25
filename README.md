# FastMed API

This is the backend RESTful API for the [Telegram Medical Appointment Bot](https://github.com/andreysiniy/tg-med-bot). It is a .NET 8 application responsible for managing all data related to medical clinics, doctors, working schedules, and patient appointments. It provides a set of endpoints that the Telegram bot consumes to perform its functions.

## Features

-   **RESTful Endpoints:** Provides a complete set of CRUD (Create, Read, Update, Delete) endpoints for managing core entities.
-   **Entity Management:**
    -   **Clinics:** Manages clinic information, including name, location, and working hours.
    -   **Doctors:** Manages doctor profiles, including specialty, experience, and the clinic they belong to.
    -   **Appointments:** Handles the booking, retrieval, updating, and cancellation of appointments.
-   **Advanced Filtering & Search:**
    -   Filter doctors by clinic, specialty, name, or a combination of these.
    -   Search for clinics and doctors by name.
    -   Retrieve a list of unique specializations available based on filters.
-   **Availability Logic:**
    -   Calculates and returns available time slots for a specific doctor on a given date, taking into account the clinic's working hours and existing appointments.
    -   Filters doctors who are available at a specific date and time.
-   **Database:** Uses SQLite for simple, file-based data persistence.
-   **Containerization:** Full Docker support for easy deployment and environment consistency.
-   **API Documentation:** Integrated Swagger/OpenAPI documentation for easy exploration and testing of endpoints.

## Technology Stack

-   **Framework:** ASP.NET Core 8
-   **Language:** C#
-   **Database:** SQLite with Entity Framework Core 8
-   **Mapping:** AutoMapper
-   **API Documentation:** Swashbuckle (Swagger)
-   **Containerization:** Docker

## API Endpoints

The API is structured around several main resources. The base URL is `/api`.

---

### `api/ClinicCards`

Manages clinic information.

| Method | Endpoint              | Description                               |
| :----- | :-------------------- | :---------------------------------------- |
| `GET`  | `/`                   | Retrieves all clinic cards.               |
| `GET`  | `/{id}`               | Retrieves a specific clinic card by its ID. |
| `GET`  | `/name/{name}`        | Retrieves clinic cards by name (partial match). |
| `POST` | `/`                   | Creates a new clinic card.                |
| `PUT`  | `/{id}`               | Updates an existing clinic card.          |
| `DELETE`| `/{id}`              | Deletes a clinic card.                    |

---

### `api/DoctorCards`

Manages doctor profiles and their availability.

| Method | Endpoint                         | Description                                                                                             |
| :----- | :------------------------------- | :------------------------------------------------------------------------------------------------------ |
| `GET`  | `/`                              | Retrieves a list of doctors with optional filters: `clinicId`, `speciality`, `name`, `appointmentDate`. |
| `GET`  | `/{id}`                          | Retrieves a specific doctor by ID.                                                                      |
| `GET`  | `/speciality/{speciality}`       | Retrieves doctors by specialty (partial match).                                                         |
| `GET`  | `/name/{name}`                   | Retrieves doctors by name (partial match).                                                              |
| `GET`  | `/speciality/`                   | Retrieves a list of all unique doctor specialities, with optional filters.                              |
| `GET`  | `/{id}/timeslots/{date}`         | Retrieves available 30-minute time slots for a doctor on a specific date.                               |
| `POST` | `/`                              | Creates a new doctor card.                                                                              |
| `PUT`  | `/{id}`                          | Updates an existing doctor card.                                                                        |
| `DELETE`| `/{id}`                         | Deletes a doctor card.                                                                                  |

---

### `api/Appointment`

Manages patient appointments.

| Method | Endpoint              | Description                               |
| :----- | :-------------------- | :---------------------------------------- |
| `GET`  | `/`                   | Retrieves all appointments.               |
| `GET`  | `/{id}`               | Retrieves a specific appointment by ID.   |
| `GET`  | `/user/{uuid}`        | Retrieves all appointments for a specific user UUID. |
| `POST` | `/`                   | Creates a new appointment.                |
| `PUT`  | `/{id}`               | Updates (reschedules) an existing appointment. |
| `DELETE`| `/{id}`              | Deletes (cancels) an appointment.         |

---

## Database Models

The data is structured using the following Entity Framework Core models:

-   **`ClinicCard`**: Represents a medical clinic. It has a one-to-many relationship with `DoctorCard` and `WorkingHour`.
-   **`DoctorCard`**: Represents a doctor. It belongs to one `ClinicCard` and has a one-to-many relationship with `Appointment`.
-   **`Appointment`**: Represents a scheduled appointment. It belongs to one `DoctorCard` and contains patient details.
-   **`WorkingHour`**: Defines the opening and closing times for a `ClinicCard` on a specific day of the week.

## Setup and Installation

### Prerequisites

-   .NET 8 SDK
-   A code editor like Visual Studio, JetBrains Rider, or VS Code.

### Running Locally

1.  **Clone the Repository**
    ```bash
    git clone https://github.com/andreysiniy/fastmed-api.git
    cd fastmed-api
    ```

2.  **Restore Dependencies**
    Open the solution in your IDE or run the following command in the root directory:
    ```bash
    dotnet restore
    ```

3.  **Database Setup**
    The project is configured to use a local SQLite database file named `fastmed.db`. This file is included in the repository and will be used automatically. The connection string is defined in `fastmed-api/appsettings.json`.

4.  **Run the Application**
    ```bash
    cd fastmed-api
    dotnet run
    ```
    The API will start and listen on the ports defined in `Properties/launchSettings.json`. By default, this is `http://localhost:5136`.

5.  **Explore the API**
    Once the application is running, you can access the interactive Swagger UI documentation in your browser to test the endpoints:
    [http://localhost:5136/swagger](http://localhost:5136/swagger)

## Running with Docker

The project includes a `Dockerfile` and a `compose.yaml` for easy containerization.

### Build and Run with Docker Compose

This is the simplest method. From the root directory:

```bash
# Build the image
docker-compose build

# Start the container
docker-compose up
```

### Build and Run with Docker CLI

1.  **Build the Docker Image**
    From the root directory, run:
    ```bash
    docker build -t fastmed-api -f fastmed-api/Dockerfile .
    ```

2.  **Run the Docker Container**
    ```bash
    docker run -d -p 5136:8080 --name fastmed-api-container fastmed-api
    ```
    The API will be accessible at `http://localhost:5136`. Note that the container's internal port `8080` is mapped to the host's port `5136`.
