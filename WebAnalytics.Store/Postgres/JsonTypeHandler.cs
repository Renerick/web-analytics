using System;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper;

namespace WebAnalytics.Store.Postgres
{
    public class JsonTypeHandler : SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.Value = (value == null)
                ? (object) DBNull.Value
                : JsonSerializer.Serialize(value,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true
                    });
            parameter.DbType = DbType.String;
        }

        public object Parse(Type destinationType, object value)
        {
            return JsonSerializer.Deserialize(value.ToString(), destinationType,
                new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, IgnoreNullValues = true,
                });
        }
    }
}