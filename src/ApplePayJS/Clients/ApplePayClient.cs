// Copyright (c) Just Eat, 2016. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JustEat.ApplePayJS.Clients;

public class ApplePayClient(HttpClient httpClient)
{
    public async Task<JsonDocument> GetMerchantSessionAsync(
        Uri requestUri,
        MerchantSessionRequest request,
        CancellationToken cancellationToken = default)
    {
        // POST the data to create a valid Apple Pay merchant session.
        using var response = await httpClient.PostAsJsonAsync(requestUri, request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

        // Read the opaque merchant session JSON from the response body.
        //var merchantSession = await response.Content.ReadFromJsonAsync<JsonDocument>(cancellationToken);

        return JsonSerializer.Deserialize<JsonDocument>(jsonResponse);
    }
}
