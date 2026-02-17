namespace Library.Web.Constants
{
    public static class Constants
    {
        public static class Setting
        {
            public const string HttpClientApiKey = "LibraryApi";
        }

        public static class Book
        {
            public static class EndPoints
            {
                public const string GetAll = "/api/book/GetAll";
                public const string Delete = "/api/book/Delete/{0}";
                public const string Create = "/api/book/Create";
            }
            
            public static class Messages
            {
                public const string ExceptionApiError = "Ocurrió un error consultando el servicio de libros";
            }
        }

        public static class Author
        {
            public static class EndPoints
            {
                public const string GetAll = "/api/author/GetAll";
                public const string GetAllMinified = "/api/author/GetAllMinified";
                public const string Delete = "/api/author/Delete/{0}";
                public const string Create = "/api/author/Create";
            }

            public static class Messages
            {
                public const string ExceptionApiError = "Ocurrió un error consultando el servicio de autores";
            }
        }
    }
}
