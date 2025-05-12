# 🏥 Práctica 3 - Sistema de Gestión de Pacientes con Códigos Únicos

## 📌 Descripción
Sistema compuesto por:
- **ClinicAPI**: API principal para gestión de pacientes
- **PatientCodeService**: Microservicio para generación de códigos únicos

## 🔗 Enlaces
- [Repositorio GitHub](https://github.com/tu-usuario/repo)
- [Documentación técnica (Wiki)](https://github.com/tu-usuario/repo/wiki)
- [Video demostración](https://youtu.be/ejemplo) (opcional)

## 🚀 Instalación Local
```bash
# 1. Clonar repositorio
git clone https://github.com/tu-usuario/repo.git

# 2. Ejecutar servicios (terminales separadas)
cd PatientCodeService && dotnet run
cd ClinicAPI && dotnet run
```

## 🛠️ Estructura del Código
```plaintext
/ClinicAPI
  /Controllers    # Endpoints API
  /Services       # Lógica de negocio
/PatientCodeService
  /Controllers    # Generación de códigos
```

## ⚠️ Problemas Conocidos
- Error al desplegar en Azure (ID: 07951fd3...)
- Advertencias CS8618 en modelos (manejo de nulables)

## 📝 Checklist de Implementación
- [x] Servicio de generación de códigos
- [x] Integración HTTP entre servicios
- [x] Persistencia en archivo
- [ ] Despliegue en Azure (pendiente)
