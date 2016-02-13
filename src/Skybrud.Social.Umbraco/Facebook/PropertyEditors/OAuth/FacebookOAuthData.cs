﻿using System;
using Newtonsoft.Json;
using Skybrud.Social.Facebook;

namespace Skybrud.Social.Umbraco.Facebook.PropertyEditors.OAuth {

    public class FacebookOAuthData {

        #region Private fields

        private FacebookService _service;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ID of the authenticated user.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// Gets the name of the authenticated user.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        /// <summary>
        /// Gets the expiry date of the access token. Facebook access tokens typically have a
        /// lifetime of two months.
        /// </summary>
        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
        
        /// <summary>
        /// Gets an array of the scope (permissions).
        /// </summary>
        [JsonProperty("scope")]
        public string[] Scope { get; set; }

        /// <summary>
        /// Gets an array of business accounts (pages).
        /// Requires "manage_pages" permission
        /// </summary>
        [JsonProperty("business_pages")]
        public FacebookBusinessPageData[] BusinessPages { get; set; }

        /// <summary>
        /// Stores a Business Page data selected by the user
        /// </summary>
        [JsonProperty("selected_business_page")]
        public FacebookBusinessPageData SelectedBusinessPage { get; set; }

        /// <summary>
        /// Gets whether the OAuth data is valid - that is whether the OAuth data has a valid
        /// access token and the expiration timestamp hasn't been passed. Calling this property
        /// will not check the validate the access token against the API.
        /// </summary>
        [JsonIgnore]
        public bool IsValid {
            get { return !String.IsNullOrWhiteSpace(AccessToken) && ExpiresAt > DateTime.Now; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the FacebookService class. Invoking this method will not
        /// result in any calls to the Facebook API.
        /// </summary>
        public FacebookService GetService() {
            return _service ?? (_service = FacebookService.CreateFromAccessToken(AccessToken));
        }

        /// <summary>
        /// Serializes the OAuth data into a JSON string.
        /// </summary>
        public string Serialize() {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Deserializes the specified JSON string into an OAuth data object.
        /// </summary>
        /// <param name="str">The JSON string to be deserialized.</param>
        public static FacebookOAuthData Deserialize(string str) {
            return JsonConvert.DeserializeObject<FacebookOAuthData>(str);
        }

        #endregion

    }

    public class FacebookBusinessPageData {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

}