SELECT "ClientName", Count(*)
	FROM public."Clients"
	JOIN public."ClientContacts" on public."ClientContacts"."ClientId" = public."Clients"."Id"
	GROUP BY "ClientName"