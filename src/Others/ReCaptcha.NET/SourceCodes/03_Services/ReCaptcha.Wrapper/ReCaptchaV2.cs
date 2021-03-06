﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aliencube.ReCaptcha.Wrapper.Extensions;
using Aliencube.ReCaptcha.Wrapper.Interfaces;
using Newtonsoft.Json;

namespace Aliencube.ReCaptcha.Wrapper
{
    /// <summary>
    /// This represents the wrapper entity for ReCaptcha V2 API (https://google.com/recaptcha).
    /// </summary>
    public class ReCaptchaV2 : IReCaptchaV2
    {
        private readonly IReCaptchaV2Settings _settings;

        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <c>ReCaptchaV2</c> class.
        /// </summary>
        /// <param name="settings"><c>ReCaptchaV2Settings</c> instance.</param>
        public ReCaptchaV2(IReCaptchaV2Settings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            this._settings = settings;
        }

        /// <summary>
        /// Gets the <c>ReCaptchaV2Request</c> instance.
        /// </summary>
        /// <param name="form">Form values collection.</param>
        /// <param name="serverVariables">Server variables collection.</param>
        /// <returns>Returns the <c>ReCaptchaV2Request</c> instance.</returns>
        public ReCaptchaV2Request GetReCaptchaV2Request(NameValueCollection form, NameValueCollection serverVariables = null)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            if (!form.AllKeys.Contains("g-recaptcha-response"))
            {
                throw new KeyNotFoundException();
            }

            var response = form["g-recaptcha-response"];
            if (String.IsNullOrWhiteSpace(response))
            {
                throw new InvalidOperationException("Response value not found");
            }

            string remoteAddr = null;
            if (serverVariables != null && serverVariables.AllKeys.Contains("REMOTE_ADDR"))
            {
                remoteAddr = serverVariables["REMOTE_ADDR"];
            }

            var request = new ReCaptchaV2Request()
                          {
                              Secret = this._settings.SecretKey,
                              Response = response,
                              RemoteIp = remoteAddr,
                          };
            return request;
        }

        /// <summary>
        /// Verifies the request asynchronously.
        /// </summary>
        /// <param name="form">Form values collection.</param>
        /// <param name="serverVariables">Server variables collection.</param>
        /// <returns>Returns <c>ReCaptchaV2Response</c> object.</returns>
        public async Task<ReCaptchaV2Response> SiteVerifyAsync(NameValueCollection form, NameValueCollection serverVariables = null)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            if (!form.AllKeys.Contains("g-recaptcha-response"))
            {
                throw new KeyNotFoundException();
            }

            var request = this.GetReCaptchaV2Request(form, serverVariables);
            var response = await this.SiteVerifyAsync(request);
            return response;
        }

        /// <summary>
        /// Verifies the request asynchronously.
        /// </summary>
        /// <param name="request"><c>ReCaptchaV2Request</c> object.</param>
        /// <returns>Returns <c>ReCaptchaV2Response</c> object.</returns>
        public async Task<ReCaptchaV2Response> SiteVerifyAsync(ReCaptchaV2Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var responseBody = await this.GetResponseBodyAsync(request);
            var response = JsonConvert.DeserializeObject<ReCaptchaV2Response>(responseBody);
            return response;
        }

        /// <summary>
        /// Gets the response body as a JSON format asynchronously.
        /// </summary>
        /// <param name="request"><c>ReCaptchaV2Request</c> object.</param>
        /// <returns>Returns the response body as a JSON format.</returns>
        public virtual async Task<string> GetResponseBodyAsync(ReCaptchaV2Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            string responseBody;
            using (var client = new HttpClient())
            {
                var requestBody = this.GetRequestBody(request);
                var content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

                var httpResponse = await client.PostAsync(this._settings.RequestUrl, content);
                responseBody = await httpResponse.Content.ReadAsStringAsync();
            }

            return responseBody;
        }

        /// <summary>
        /// Gets the request body.
        /// </summary>
        /// <param name="request"><c>ReCaptchaV2Request</c> object.</param>
        /// <returns>Returns the request body.</returns>
        public string GetRequestBody(ReCaptchaV2Request request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var requestBody = request.GetType()
                                     .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                     .ToDictionary(p => p.Name.ToLower(), p => Convert.ToString(p.GetValue(request, null)))
                                     .Flatten();
            return requestBody;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}