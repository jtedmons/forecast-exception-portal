\# Forecast Exception Portal



A full-stack data application for managing forecast exceptions from detection through investigation and resolution.



The goal of this project is to model a practical workflow where forecast exceptions can be reviewed, filtered, investigated, assigned, and eventually resolved.



\## Current Status



Initial ASP.NET Core API created using hardcoded in-memory sample data. Database persistence and frontend functionality will be added in later iterations.



\## Implemented



\- Health check endpoint

\- Exception list endpoint

\- Exception detail endpoint

\- Status filter endpoint

\- Swagger API testing



\## Current API Endpoints



| Method | Endpoint | Description |

|---|---|---|

| GET | `/api/health` | Returns API health/status information |

| GET | `/api/exceptions` | Returns all sample forecast exception records |

| GET | `/api/exceptions/{id}` | Returns one exception record by ID |

| GET | `/api/exceptions/status/{status}` | Returns exceptions filtered by status |



\## Run Locally



From the project root:



```bash

dotnet run --project src/ForecastExceptionPortal.Api --urls http://localhost:5050

```



Then open Swagger:



```text

http://localhost:5050/swagger

```



\## Next Steps



\- Add database persistence

\- Add Entity Framework Core

\- Seed sample data

\- Add API tests

\- Build React TypeScript frontend



