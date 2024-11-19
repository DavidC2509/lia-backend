using Lia.SharedKernel.Exceptions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Xml.Linq;

namespace Lia.SharedKernel.Utils
{
    public static class DataUtils
    {
        public static HttpRequestMessage CreateHtpRequest<TData>(this HttpMethod httpMethod, TData data, string url)
        {
            return new HttpRequestMessage(httpMethod, url)
            {
                Content = JsonContent.Create(data)
            };
        }

        public static HttpRequestMessage CreateHtpRequest(this HttpMethod httpMethod, string url)
        {
            return new HttpRequestMessage(httpMethod, url);
        }

        public static HttpRequestMessage CreateHttpFormEncode(this HttpMethod httpMethod, Dictionary<string, string> data, string url)
        {
            return new HttpRequestMessage(httpMethod, url)
            {
                Content = new FormUrlEncodedContent(data)
            };
        }

        public static HttpClient AddAuthentication(this HttpClient client, string userName, string password, string scheme)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{userName}:{password}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }

        public static HttpClient AddTokenTsoPromise(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public static HttpClient AddCustomHeader(this HttpClient client, string headerName, string headerValue)
        {
            client.DefaultRequestHeaders.Add(headerName, headerValue);
            return client;
        }

        // Método para agregar contenido MultipartFormDataContent
        public static HttpRequestMessage AddFormData(this HttpRequestMessage request, Dictionary<string, string> fields, StreamContent fileContent, string fileName, string fileFieldName = "file")
        {
            var formData = new MultipartFormDataContent();

            // Agrega los campos adicionales al formulario
            foreach (var field in fields)
            {
                formData.Add(new StringContent(field.Value), field.Key);
            }

            // Agrega el archivo al formulario
            formData.Add(fileContent, fileFieldName, fileName);

            // Asigna el contenido al request
            request.Content = formData;

            return request;
        }

        public static async Task<Result> SendConvertData<Result>(this HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await client.SendAsync(request, cancellationToken);

            var status_ = (int)response.StatusCode;
            if (status_ == (int)HttpStatusCode.OK)
            {
                var responseText = await response.Content.ReadAsStringAsync();

                try
                {
                    // Verificar si la respuesta es un XML que contiene un JSON
                    if (responseText.TrimStart().StartsWith("<?xml"))
                    {

                        var soapResponse = XDocument.Parse(responseText);
                        responseText = soapResponse.Descendants(XName.Get("string", "http://www.tt.com.bo/")).FirstOrDefault()?.Value;

                        if (string.IsNullOrEmpty(responseText))
                        {
                            throw new Exception("Failed to register payment in SAP: Empty response");
                        }
                    }


                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var typedBody = JsonSerializer.Deserialize<Result>(responseText, options);

                    return typedBody!;
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + exception.Message + ".";
                    throw new CustomException((int)HttpStatusCode.BadRequest, "Error Json", message);
                }
            }
            else
            {
                var responseData_ = response.Content == null ? null : await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                if (responseData_ == null) throw new CustomException((int)HttpStatusCode.BadRequest, "Error Microservices", "Error Interno");

                if (request.RequestUri!.Host.Contains("tsoonpremiseapi") && request.RequestUri!.AbsoluteUri.Contains("tsopremise"))
                {
                    var typedBody = JsonSerializer.Deserialize<ErrorData>(responseData_, options);

                    throw new CustomException((int)response.StatusCode, "Error Microservices", typedBody!.Errors);
                }
                else
                {
                    var typedBody = JsonSerializer.Deserialize<Result>(responseData_, options);
                    return typedBody!;
                }

            }
        }


        // Dentro de tu clase o método
        public static string BuildQueryString<T>(T request)
        {
            var queryParameters = HttpUtility.ParseQueryString(string.Empty);

            // Obtener todas las propiedades públicas del objeto request
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                // Obtener el valor de cada propiedad
                object? value = property.GetValue(request);

                // Ignorar propiedades nulas o vacías
                if (value == null || value is string str && string.IsNullOrEmpty(str))
                    continue;

                // Formatear fechas
                if (value is DateTime dateTimeValue)
                {
                    value = dateTimeValue.ToString("yyyy-MM-ddTHH:mm:ss");
                }

                // Convertir valores booleanos a minúsculas
                if (value is bool boolValue)
                {
                    value = boolValue.ToString().ToLower();
                }

                // Agregar la propiedad y su valor a la cadena de consulta
                queryParameters[property.Name] = value.ToString();
            }

            return "?" + queryParameters.ToString();
        }

    }
}