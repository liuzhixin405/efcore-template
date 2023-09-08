﻿namespace Serialization.SystemTextJson;

using System.Text.Json;
using System.Text.Json.Serialization;
using global::Serialization;

/// <summary>
/// Implementation of <see cref="IMessageSerializer"/> using <see cref="JsonSerializer"/>.
/// </summary>
public class JsonMessageSerializer : IMessageSerializer
{
    /// <summary>
    /// <see cref="JsonSerializerOptions"/> options for the JSON serializer. By default adds <see cref="ObjectToInferredTypesConverter"/> converter.
    /// </summary>
    public JsonSerializerOptions Options { get; set; }

    public JsonMessageSerializer(JsonSerializerOptions options = null)
    {
        Options = options ?? new(JsonSerializerDefaults.Web)
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            AllowTrailingCommas = true
        };
        Options.Converters.Add(new ObjectToInferredTypesConverter());
    }

    public byte[] Serialize(Type t, object message) =>
        JsonSerializer.SerializeToUtf8Bytes(message, t, Options);

    public object Deserialize(Type t, byte[] payload) =>
        JsonSerializer.Deserialize(payload, t, Options)!;
}