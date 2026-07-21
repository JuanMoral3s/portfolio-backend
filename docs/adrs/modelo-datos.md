# Modelo de Datos - Portfolio Backend

Este documento describe la estructura relacional de la base de datos utilizada para el portafolio.

## Diagrama Entidad-Relación (ERD)

    PROJECT {
        int Id PK
        string Title
        string Description
        string DynamicImpact
        datetime CompletedDate
        string Type
    }
    TECHNOLOGY {
        int Id PK
        string Name
        string Category
    }
    PROJECT_TECHNOLOGY {
        int ProjectId FK
        int TechnologyId FK
    }
    EXPERIENCE {
        int Id PK
        string CompanyOrLab
        string Role
        string AchievementsSummary
        datetime StartDate
        datetime EndDate
    }
    PROJECT ||--|{ PROJECT_TECHNOLOGY : "contains"}
    TECHNOLOGY ||--|{ PROJECT_TECHNOLOGY : "utilised_by"}