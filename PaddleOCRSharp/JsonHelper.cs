// Copyright (c) 2021 raoyutian   All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace PaddleOCRSharp
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);
#if NETCOREAPP2_1_OR_GREATER
            return (T)   System.Text.Json.JsonSerializer.Deserialize(json, typeof(T),new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive=false} );
#else
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            };
            return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(T), settings);
#endif
          
        }
    }
}
