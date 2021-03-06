﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kyrio.Services.Shared
{
    /// <summary>
    /// Abstract implementation of REST clients to call Kyrio services.
    /// </summary>
    public abstract class KyrioRestClient
    {
        /// <summary>
        /// Kyrio account associated with this client
        /// </summary>
        protected KyrioAccount _account;

        /// <summary>
        /// Constructs this client and sets initial values.
        /// </summary>
        /// <param name="account">A Kyrio account associated with his client.</param>
        public KyrioRestClient(KyrioAccount account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            _account = account;
        }

        /// <summary>
        /// Creates HTTP client and sets default headers for all REST calls.
        /// </summary>
        /// <returns>Created HTTP client</returns>
        private HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.DefaultRequestHeaders.Add("client-id", _account.ClientId);
            client.DefaultRequestHeaders.Add("enable-test-mock", _account.EnableTestMock ? "true" : "false");
            client.DefaultRequestHeaders.Add("enable-test-error", _account.EnableTestError ? "true" : "false");
            return client;
        }

        /// <summary>
        /// Composes service URL based on server URL, operation route and parameters.
        /// </summary>
        /// <param name="route">Base operation route</param>
        /// <param name="parameters">Operation query parameters</param>
        /// <returns>Operation URI</returns>
        private Uri ComposeRequestUri(string route, IDictionary<string, object> parameters)
        {
            var builder = new StringBuilder(_account.ServerUrl);

            if (!route.StartsWith("/"))
                builder.Append("/");

            builder.Append(route);

            var queryParams = ComposeQueryParams(parameters);
            builder.Append(queryParams);

            var uri = builder.ToString();
            return new Uri(uri, UriKind.Absolute);
        }

        /// <summary>
        /// Composes query parameters into encoded string.
        /// </summary>
        /// <param name="parameters">Operation query parameters</param>
        /// <returns>Encoded query parameter string</returns>
        private string ComposeQueryParams(IDictionary<string, object> parameters)
        {
            if (parameters == null) return "";

            var builder = new StringBuilder();

            foreach (var key in parameters.Keys)
            {
                if (builder.Length == 0)
                    builder.Append('?');
                else builder.Append('&');

                builder.Append(key);
                builder.Append('=');

                var value = WebUtility.UrlEncode("" + parameters[key]);
                builder.Append(value);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates request content object with JSON representation of send data.
        /// </summary>
        /// <param name="value">Value to be passed in request body.</param>
        /// <returns>JSON representation of value wrapped intp HttpContent</returns>
        private HttpContent CreateRequestContent(object value)
        {
            if (value == null) return null;

            var content = JsonConvert.SerializeObject(
                value,
                Formatting.None,
                new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    NullValueHandling = NullValueHandling.Ignore
                }
            );

            var result = new StringContent(content, Encoding.UTF8, "application/json");

            return result;
        }

        /// <summary>
        /// Parses response body into specified object type
        /// </summary>
        /// <typeparam name="T">Expected type of response object</typeparam>
        /// <param name="response">Http response message</param>
        /// <returns>Parsed response object of specified type or <code>null</code></returns>
        private async Task<T> ParseResponseContentAsync<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            T result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(responseContent);
            }
            catch (Exception ex)
            {
                throw new KyrioException(ErrorCode.UNKNOWN, (int)response.StatusCode, ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Parses error sent in response body
        /// </summary>
        /// <param name="response">Http response message</param>
        /// <returns>Parsed error message or <code>null</code></returns>
        private async Task<string> ParseResponseErrorAsync(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            string result = null;
            try
            {
                result = JsonConvert.DeserializeObject<string>(responseContent);
            }
            catch (Exception ex)
            {
                throw new KyrioException(ErrorCode.UNKNOWN, (int)response.StatusCode, ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Invokes REST operation on the server and handles the response.
        /// </summary>
        /// <typeparam name="T">Expected type of response object.</typeparam>
        /// <param name="method">Operation method: GET, POST, PUT or DELETE</param>
        /// <param name="route">Operation base route</param>
        /// <param name="parameters">Operation query parameters</param>
        /// <param name="body">Value to be sent in request body</param>
        /// <returns>Value returned by the server of expected type.</returns>
        protected async Task<T> InvokeAsync<T>(string method, string route, IDictionary<string, object> parameters, object body)
        {
            if (string.IsNullOrEmpty(_account.ClientId))
                throw new ArgumentException("ClientId is not set");
            if (string.IsNullOrEmpty(_account.ServerUrl))
                throw new ArgumentException("ServerUrl is not set");
            if (string.IsNullOrEmpty(method))
                throw new ArgumentNullException("method");
            if (string.IsNullOrEmpty(route))
                throw new ArgumentNullException("route");

            HttpClient client = CreateClient();
            Uri requestUri = ComposeRequestUri(route, parameters);

            using (HttpContent requestContent = CreateRequestContent(body))
            {
                HttpResponseMessage response;

                try
                {
                    method = method.ToUpperInvariant();
                    if (method == "GET")
                        response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
                    else if (method == "POST")
                        response = await client.PostAsync(requestUri, requestContent);
                    else if (method == "PUT")
                        response = await client.PutAsync(requestUri, requestContent);
                    else if (method == "DELETE")
                        response = await client.DeleteAsync(requestUri);
                    else
                        throw new ArgumentException("Unsupported method");
                }
                catch (HttpRequestException ex)
                {
                    throw new KyrioException(ErrorCode.NO_CONNECTION, 0, ex.Message, ex);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                        return await ParseResponseContentAsync<T>(response);
                    case HttpStatusCode.NoContent:
                        return default(T);
                    case HttpStatusCode.BadRequest:
                        throw new KyrioException(ErrorCode.BAD_REQUEST, (int)response.StatusCode, await ParseResponseErrorAsync(response));
                    case HttpStatusCode.Unauthorized:
                        throw new KyrioException(ErrorCode.UNAUTHORIZED, (int)response.StatusCode, await ParseResponseErrorAsync(response));
                    case HttpStatusCode.InternalServerError:
                        throw new KyrioException(ErrorCode.INTERNAL, (int)response.StatusCode, await ParseResponseErrorAsync(response));
                    case HttpStatusCode.GatewayTimeout:
                        throw new KyrioException(ErrorCode.TIMEOUT, (int)response.StatusCode, await ParseResponseErrorAsync(response));
                    default:
                        throw new KyrioException(ErrorCode.UNKNOWN, (int)response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
        }

    }
}
