
![logo](cb_data_white.png)
# cbData

Tento projekt vzniknul za účelem prezentace zkušeností.

## Authors

- [@M-STR](https://github.com/M-STR15)


## License

[MIT](https://choosealicense.com/licenses/mit/)


## Features

- Zobrazení rozcestníku
- zobrazovač REST API
- ukládání dat do bufferu/JSON
- zobrazení EventLogu


## Installation
1.úprava connectstringů v souboru appsettings.json

- umístění souboru "cbData.appsettings.json"
    - cbDataDB: připojení na databázi v RELEASE
    - cbDataDB-local: připojení na databázi v DEBUG

```bash
 {
	"Logging": {
		"LogLevel": {
			"Default": "Information",
			"Microsoft.AspNetCore": "Warning"
		}
	},
	"ConnectionStrings": {
		"cbDataDb": "server=<nameDBserver>; database=cbData;Trusted_Connection=True;TrustServerCertificate=True;",
		"cbDataDb-local": "server=<nameDBserver>; database=cbData;Trusted_Connection=True;TrustServerCertificate=True;"
	},
	"AllowedHosts": "*"
}
}
```

2.Spustit příkaz v Package Manager Console

```bash
  update-Database -StartupProject cbData -Project cbData.BE.DB
```

3. Spustit aplikaci


    
## Release

### 1.0.0   (2025-02-03)

Uvolněné základní funkcionality

## 🔗 Links
[![portfolio](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/M-STR15/Shutdown-PC/)

