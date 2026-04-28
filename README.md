# 🏆 Sistema de Gestión de Complejo Deportivo (Nexus Sports Manager)

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

Este proyecto es una aplicación web MVC desarrollada en **ASP.NET Core 8** para la gestión integral de un complejo deportivo. Permite registrar usuarios, administrar canchas y controlar reservaciones evitando el cruce de horarios, cumpliendo con todos los requerimientos de la prueba de desempeño.

---

## 👤 Datos del Estudiante
* **Nombre:** [ESCRIBE TU NOMBRE AQUÍ]
* **Documento/Código:** [ESCRIBE TU CÓDIGO AQUÍ]
* **Curso/Módulo:** [ESCRIBE TU CURSO AQUÍ]

---

## ✨ Características Principales
* **CRUD Completo:** Gestión total de Usuarios y Espacios Deportivos (Canchas).
* **Control de Reservas Inteligente:** Lógica de negocio rigurosa que impide realizar dobles reservas en la misma cancha y cruce de horarios para un mismo usuario.
* **Filtros de Búsqueda:** Capacidad de filtrar canchas por tipo (ej. "Fútbol", "Baloncesto").
* **Diseño Ultra-Premium:** Interfaz de usuario "Glassmorphism" moderna, responsiva y orientada a la experiencia del usuario (UX) con validaciones preventivas.
* **Notificaciones Reales por Correo:** Integración con **MailKit** y SMTP para el envío automático de correos electrónicos al confirmar una reserva.

---

## 🛠️ Tecnologías Utilizadas
* **Backend:** C#, ASP.NET Core MVC (.NET 8)
* **ORM:** Entity Framework Core (Pomelo MySQL)
* **Base de Datos:** MySQL
* **Frontend:** HTML5, CSS3 Avanzado (Variables y Glassmorphism), Bootstrap 5.
* **Servicios de Terceros:** MailKit (Envío de Emails SMTP).

---

## 🚀 Guía de Instalación y Configuración

Para ejecutar este proyecto en tu máquina local, sigue estos pasos:

### 1. Clonar el repositorio
```bash
git clone https://github.com/monterrosag18/PruebaDesempenoCsharp270326.git
cd PruebaDesempenoCsharp270326
```

### 2. Configurar la Base de Datos y el Correo
Abre el archivo `appsettings.json` en la raíz del proyecto y reemplaza los datos con tus credenciales reales:

```json
{
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "Nexus Sports System",
    "SenderEmail": "tu.correo@gmail.com",
    "Username": "tu.correo@gmail.com",
    "Password": "tu-contraseña-de-aplicacion-de-google"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR_MYSQL;Database=TU_BASE_DE_DATOS;User Id=TU_USUARIO;Password=TU_CONTRASEÑA"
  }
}
```

### 3. Compilar y Ejecutar
Abre una terminal en la carpeta del proyecto y ejecuta:
```bash
dotnet restore
dotnet build
dotnet run
```
*También puedes simplemente abrir la solución en Visual Studio y presionar `F5` (Play).*

---

## 📖 Instrucciones de Uso
1. **Paso 1:** Al iniciar la aplicación, dirígete a la sección de **Usuarios** y registra al menos un cliente.
2. **Paso 2:** Dirígete a la sección de **Canchas (Spaces)** y registra al menos un espacio deportivo.
3. **Paso 3:** Ve a la pantalla principal o a la sección de **Reservaciones** e intenta agendar una hora. El sistema validará automáticamente la disponibilidad y te enviará un correo de confirmación.
