using System.Collections.Generic;

namespace Htec.Poc.API.FunctionalTests.Models;

public class SearchResponse
{
    public int size { get; set; }
    public int offset { get; set; }
    public List<SearchResponseItem> results { get; set; }
}