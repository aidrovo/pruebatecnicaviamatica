🚀 Sistema de Gestión de Usuarios

Aplicación full stack desarrollada con Angular (frontend) y .NET (backend), que permite la gestión completa de usuarios con autenticación JWT y carga masiva mediante archivos Excel.

---

📌 Características principales

- 🔐 Autenticación con JWT (Login / Logout)
- 🛡️ Protección de rutas con Guards
- 👥 CRUD completo de usuarios
  - Crear usuario
  - Listar usuarios
  - Buscar usuarios
  - Editar usuario
  - Eliminación lógica (soft delete)
- 🔍 Búsqueda dinámica por nombre o username
- 📂 Carga masiva de usuarios mediante Excel
- ✅ Validaciones en backend

---

🧱 Tecnologías utilizadas

Frontend

- Angular
- TypeScript
- HttpClient
- Angular Router

Backend

- .NET (ASP.NET Core Web API)
- Entity Framework Core
- JWT Authentication
- EPPlus (manejo de Excel)

---

🔐 Autenticación

El sistema utiliza JWT (JSON Web Tokens) para la autenticación.

- El usuario inicia sesión y recibe un token
- El token se almacena en "localStorage"
- Se envía en cada petición protegida mediante el header:

Authorization: Bearer {token}

---

👥 Gestión de Usuarios

✔ Crear usuario

Se valida:

- Username (mayúsculas, números, longitud)
- Password (seguridad básica)
- Identificación (10 dígitos únicos)

✔ Listar usuarios

Solo se muestran usuarios no eliminados ("IsDeleted = false")

✔ Editar usuario

Permite actualizar nombre y apellido

✔ Eliminar usuario

Eliminación lógica (soft delete)

---

🔍 Búsqueda de usuarios

Permite buscar por:

- Nombre
- Username

Endpoint:

GET /api/users/search?term=valor

---

📂 Carga masiva de usuarios (Excel)

Se implementó carga de usuarios desde archivo Excel.

📥 Formato requerido:

Name| LastName| Identification| Username| Password

⚙️ Funcionamiento:

- El archivo se envía desde Angular usando "FormData"
- El backend procesa el archivo con EPPlus
- Se recorren las filas y se insertan los usuarios en base de datos

---

⚠️ Nota técnica importante

Durante la implementación se identificó que:

- Angular espera respuestas en formato JSON
- Si el backend devuelve texto plano ("string"), puede generar un error aunque el status sea 200

✅ Solución recomendada:

return Ok(new { message = "Usuarios cargados" });

---

📁 Estructura del proyecto

Frontend (Angular)

/components
  /login
  /dashboard
  /users
  /create-user
/services
/guards

Backend (.NET)

/Controllers
/Models
/Data
/Services

---

🚀 Posibles mejoras

- 🔄 Interceptor HTTP para manejo automático del token
- 📊 Paginación en listado de usuarios
- 📈 Validación de errores por fila en Excel
- 🎨 Mejora de interfaz (UI/UX)
- ⚠️ Manejo global de errores
- 👤 Implementación de roles (Admin / User)

---

▶️ Cómo ejecutar el proyecto

Backend

dotnet restore
dotnet run

Frontend

npm install
ng serve

---

💼 Autor

Desarrollado por Andrés Idrovo
Full Stack Developer

---

🧠 Nota final

Este proyecto fue desarrollado como prueba técnica, enfocado en demostrar:

- Buenas prácticas
- Separación de responsabilidades
- Manejo de autenticación
- Integración frontend-backend
- Resolución de problemas reales
