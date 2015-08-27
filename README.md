Za pokretanje projekta "TripPartner" potrebno je da imate instalirano sljedeće:

1. Visual Studio 2013 Ultimate (Nisam sigurna da li je kompatibilno sa drugim verzijama)

2. SQL Server Express 2014 sa sljedećeg linka: 
http://www.microsoft.com/en-us/download/details.aspx?id=42299

Zatim pratite sljedeće korake: 

1. Napravite instancu  SQL servera sa određenim nazivom 
2. Otvorite TripPartner solution u Visual Studiu
	2.1 Tools > Add SQL Server (Odaberite instancu servera koju ste napravili)
	2.2 View > SQL Server Object Explorer
		
                2.2.1 Odaberite instancu servera koju ste napravili u koraku 1.
		2.2.2. Desni click na Databeses > Add New Database
			2.2.2.1. Napravite bazu "TripPartner"
		2.2.3 Desni click na TripPartner bazu > Properties
                        2.2.3.1 Kopirajte ConnectionString baze
3. Otvorite projekat TripPartner.WebAPI i file web.config
4. Pronađite tag "connectionStrings" i u connectionString unesite kopirani connectionString baze
5. Otvorite Package Manager Console 
	5.1. Odaberite Target Project TripPartner.WebAPI
	5.2. Ukucajte sljedeće komande: 
	      enable-migrations
              add-migration new
              update-database
6. Pokrenite projekat

Sumejja Porča